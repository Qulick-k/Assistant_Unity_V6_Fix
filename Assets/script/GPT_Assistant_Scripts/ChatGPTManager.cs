using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;                              
///在串GPT時，記得使用OpenAI的namespace
using UnityEngine.Events;
using Oculus.Voice.Dictation;
using OpenAI.Threads;
using System.Threading;
using System;
using OpenAI.Chat;
using Utilities.WebRequestRest;
using NUnit.Framework;

public class ChatGPTManager : MonoBehaviour
{
    //決定是否使用綜合型提示詞的布林值，True就是使用綜合型提示詞，False就是使用一般提示詞
    [SerializeField] private bool useComprehensivePrompt;
    //存放Assistant的ID，兩個AI助手，一個是循序型助手的號碼，另一個是綜合型助手的號碼
    [SerializeField] private string assistantID;
    //存放OpenAI的API
    private OpenAIClient api = null;
    //存放thread    
    private ThreadResponse threads = null;

    //存放assistant助手的訊息
    private string messages;

    //存放從FireBase抓下來的Openai金鑰
    private string Open_APIKey;
    private string Open_ORGKey;

    //新增人設、場景、回覆字數上限
    //文字區域最少5行，最多20行
    [TextArea(5, 20)]
    public string personality;   //你的名字是鏈鋸人的小小紅，而且你喜歡吃冰淇淋。
    [TextArea(5, 20)]
    public string scene;           //我們人正在遊樂園裡面散步，附近有一些攤販。
    public int maxResponseWordLimit = 15;

    //抓MainScene的APIKey和ORGKey
    [SerializeField] private MainScene mainScene;


    //設一個結構NPCAction，設置動作關鍵字和動作描述
    //之後給系統序列化，讓NPCAction能在Unity上看到
    public List<NPCAction> actions;

    //設一個聽寫屬性
    public AppDictationExperience voiceToText;

    [System.Serializable]
    public struct NPCAction
    {
        public string actionKeyword;
        [TextArea(2, 5)]
        public string actionDescription;

        public UnityEvent actionEvent;
    }

    public OnResponseEvent OnResponse;
    public OnResponseEvent WaitintResponse;


    ///序列化OnResponseEvent
    [System.Serializable]
    public class OnResponseEvent : UnityEvent<string> { }

    //提供提示詞方法GetInstructions()
    public string GetInstructions()
    {
        if (useComprehensivePrompt)
        {
            return "你是一位歷史助教專門運用蘇格拉底式的質疑方式來引導學生深入思考歷史問題，以下是學生的問題:";
        }
        else
        {
            return "你是一位歷史助教，專門使用循序漸進的條列式問題來回答學生的提問";
        }
        //string instructions = "你是一位歷史助教專門運用蘇格拉底式的質疑方式來引導學生深入思考歷史問題，以下是學生的問題:";
        //return instructions;
    }

    //如果要綜合型就回傳綜合型提示詞，否則回傳一般提示詞
    string GetPrompt()
    {
        if (useComprehensivePrompt)
        {
            return "。請模仿蘇格拉底的質問用60字內引導學生的問題";
        }
        else
        {
            return "。請用60字內直接回答學生的問題";
        }
    }


    //新增AskChatGPT()方法，輸入參數為string
    public async void AskChatGPT(string newText)
    {          
        ////給予API授權
        api = new OpenAIClient(new OpenAIAuthentication(Open_APIKey, Open_ORGKey));
        //var api = new OpenAIClient(new OpenAIAuthentication(Open_APIKey, Open_ORGKey));        

        //取回Assistant
        if (assistantID == null)
        {
            Debug.LogError("Assistant ID is null，請在編輯器填上助手ID");
        }
        var assistant = await api.AssistantsEndpoint.RetrieveAssistantAsync(assistantID); //asst_9qKD9VPJa2YtwQ3qsbOsQWbd

        ////建立Thread
        if (threads == null)
        {
            var thread = await api.ThreadsEndpoint.CreateThreadAsync();
            threads = thread;
        }
        //var threads = await api.ThreadsEndpoint.CreateThreadAsync();
        ////取回Thread
        //var threads = await api.ThreadsEndpoint.RetrieveThreadAsync("thread_QzfPiSz0nsRvl7A8MU6tt63t");

        //建立message
        var request = new CreateMessageRequest(GetInstructions() + newText + GetPrompt());
        var message = await api.ThreadsEndpoint.CreateMessageAsync(threads.Id, request);
        Debug.Log($"{message.Id}: {message.Role}: {message.PrintContent()}");

        //執行run
        var run = await threads.CreateRunAsync(assistant);

        //等待run完成
        var runStatus = await threads.RetrieveRunAsync(run);
        Debug.Log($"{runStatus.Status} | [{runStatus.Id}]");
        int Count = 0;

        /// 補上RUN FAILED，Failedc或是Completed就斷迴圈
        while (runStatus.Status != RunStatus.Completed )
        {
            Count++;
            //Thread.Sleep(1000);
            StartCoroutine(Wait1Sec(Count));
            runStatus = await threads.RetrieveRunAsync(run.Id);
            runStatus = await runStatus.UpdateAsync();


            Debug.Log($"[{runStatus.Id}] {runStatus.Status} | {runStatus.CreatedAt} | 重更新了{Count}次");
            if(runStatus.Status == RunStatus.Failed)
            {                
                Debug.LogError("Run Failed，直接擷取當下生成Messages");
                break;
            }
        }

        //取回最新一段的訊息
        GetMessage(threads);        
    }

    private void Start()
    {   
        //新增訂閱者AskChatGPT()到voiceToText的event裡面
        voiceToText.DictationEvents.OnFullTranscription.AddListener(AskChatGPT);
    }

    private void Update()
    {
        //如果之後要用VR，再用input action manager抓按鍵參考。後來直接選擇點BUTTON來說話。
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            voiceToText.Activate();
        }
    }

    //新增一個方法，讓MainScene呼叫這個方法，openAI拿伺服器的APIKey和ORGKey
    public void GetAPI_ORGManager()
    {
        Open_APIKey = mainScene.getAPI();
        Open_ORGKey = mainScene.getORG();
        //var openAI = new OpenAIClient(new OpenAIAuthentication(mainScene.getAPI(), mainScene.getORG()));
        //print(mainScene.getAPI() + "和" + mainScene.getORG());
    }

    async void GetMessage(ThreadResponse thread)
    {
        var messageList = await thread.ListMessagesAsync();
        var getresponse = messageList.Items[0].PrintContent();
        messages = getresponse;
        Debug.Log($"{messages}");

        ///文字攔需要更改成可以使用卷軸顯示多行文字
        //發出聲音+更新文字在文字欄
        OnResponse.Invoke(messages); //這裡是通知遊戲場景中的回覆欄接收OnResponse的通知，引數使用chatResponse.Content。像是TTS或是文字欄的更換
    }

    IEnumerator Wait1Sec(int Count)
    {
        messages = "正在等待回覆:第" + Count + "次";
        WaitintResponse.Invoke(messages);
        yield return new WaitForSeconds(1);       
    }

    public async void CreateThread()
    {
        //新建一個Thread
        var api = new OpenAIClient(new OpenAIAuthentication(Open_APIKey, Open_ORGKey));
        var thread = await api.ThreadsEndpoint.CreateThreadAsync();
        threads = thread;

        print(threads.Id + "新增thread");
    }

    public async void DeleteThread()
    {
        if (threads != null)
        {
            //刪除舊的Thread
            var api = new OpenAIClient(new OpenAIAuthentication(Open_APIKey, Open_ORGKey));
            var isDeleted = await api.ThreadsEndpoint.DeleteThreadAsync(threads.Id);
            Assert.IsTrue(isDeleted);
            print(isDeleted + "刪除thread");
        }
        else
        {
            print("threads為null，無法刪除thread");
        }
        
    }
}

#region Sarge版本_已廢棄
////提供"動作"提示詞方法BuildActionInstructions()，讓NPC能夠遵循動作描述，並且在回覆中必定說出特定動作文字
//public string BuildActionInstructions()
//{
//    string instrction = "";

//    foreach(var item in actions)
//    {
//        instrction += "if i imply that I want you to do the following : " + item.actionDescription
//            + ". You must add to your answer the following keyword : " + item.actionKeyword + ". \n";
//    }

//    return instrction;
//}
//+


//            "You must answer in less than" + maxResponseWordLimit + "words. \n" +

//            "Here is the information about your Personnality : \n" +
//            personality + "\n" +

//            "Here is the information about the Scene around you : \n" +
//            scene + "\n" +

//            BuildActionInstructions() +

//            "Here is the message of the player : \n"
///// <summary>
///// 宣告openAI，賦予OpenAIApi類別給它，就初始化
///// </summary>
/////宣告List，泛型使用ChatMessage這個struct
//private OpenAIClient openAI = new OpenAIClient();
//private List<ChatMessage> messages = new List<ChatMessage>();
/////宣告一個Struct ChatMessage的區域變數newMessage，newMessage內有Content和Role能設定string字串。
//ChatMessage newMessage = new ChatMessage();
//newMessage.Content = "#zh-tw" + GetInstructions() + newText; //"#zh-tw"+ "#en-us"  	en-us //要讓GPT中文回應的話，記得也到wit.ai改成中文
//newMessage.Role = "user";

/////把ChatMessage型別的newMessage字串數據，丟給只接收ChatMessage類別messages串列
//messages.Add(newMessage);

/////宣告區域變數request，賦予CreateChatCompletionRequest這個類別給它
/////request的Messages串列和Model字串可以外部讀取+外部寫入，將messages串列賦予給Messages，並且設定Model版本
//CreateChatCompletionRequest request = new CreateChatCompletionRequest();
//request.Messages = messages;
//request.Model = "gpt-3.5-turbo"; //gpt-3.5-turbo   gpt-4-1106-preview

/////設立一個區域變數response，把request當作參數放進openAI.CreateChatCompletion()方法，透過await等待工作，工作完成後把數據賦予給response
/////這個response搭載了OpenAI的回覆
//var response = await openAI.CreateChatCompletion(request);

/////如果繼承CreateChatCompletion的區域變數response中的Choices串列並非null的狀態，並且串列中的index大於0
/////抓Choices串列第一個index的字串訊息，丟給區域變數chatResponse
/////再把chatResponse放進messages串列
/////之後通知遊戲場景中的回覆欄接收OnResponse的通知，引數使用chatResponse.Content
//if (response.Choices != null && response.Choices.Count > 0)
//{
//    var chatResponse = response.Choices[0].Message;

//    foreach (var item in actions)
//    {
//        //如果回覆內有包含動作關鍵字，就觸發動作通知，但同時也需要把裡面的動作關鍵字清除掉，變成空白
//        //這樣NPC才不會說出他正在做某個動作關鍵字
//        if (chatResponse.Content.Contains(item.actionKeyword))
//        {
//            string textNoKeyword = chatResponse.Content.Replace(item.actionKeyword, "");
//            chatResponse.Content = textNoKeyword;
//            item.actionEvent?.Invoke();
//        }
//    }

//    messages.Add(chatResponse);
//    Debug.Log(chatResponse.Content);

//    OnResponse.Invoke(chatResponse.Content); //這裡是通知遊戲場景中的回覆欄接收OnResponse的通知，引數使用chatResponse.Content。像是TTS或是文字欄的更換
//}
#endregion