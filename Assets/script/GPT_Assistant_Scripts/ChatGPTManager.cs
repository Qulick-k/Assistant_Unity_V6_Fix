using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;                              
///�b��GPT�ɡA�O�o�ϥ�OpenAI��namespace
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
    //�M�w�O�_�ϥκ�X�����ܵ������L�ȡATrue�N�O�ϥκ�X�����ܵ��AFalse�N�O�ϥΤ@�봣�ܵ�
    [SerializeField] private bool useComprehensivePrompt;
    //�s��Assistant��ID�A���AI�U��A�@�ӬO�`�ǫ��U�⪺���X�A�t�@�ӬO��X���U�⪺���X
    [SerializeField] private string assistantID;
    //�s��OpenAI��API
    private OpenAIClient api = null;
    //�s��thread    
    private ThreadResponse threads = null;

    //�s��assistant�U�⪺�T��
    private string messages;

    //�s��qFireBase��U�Ӫ�Openai���_
    private string Open_APIKey;
    private string Open_ORGKey;

    //�s�W�H�]�B�����B�^�Цr�ƤW��
    //��r�ϰ�̤�5��A�̦h20��
    [TextArea(5, 20)]
    public string personality;   //�A���W�r�O����H���p�p���A�ӥB�A���w�Y�B�N�O�C
    [TextArea(5, 20)]
    public string scene;           //�ڭ̤H���b�C�ֶ�̭����B�A���񦳤@���u�c�C
    public int maxResponseWordLimit = 15;

    //��MainScene��APIKey�MORGKey
    [SerializeField] private MainScene mainScene;


    //�]�@�ӵ��cNPCAction�A�]�m�ʧ@����r�M�ʧ@�y�z
    //���ᵹ�t�ΧǦC�ơA��NPCAction��bUnity�W�ݨ�
    public List<NPCAction> actions;

    //�]�@��ť�g�ݩ�
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


    ///�ǦC��OnResponseEvent
    [System.Serializable]
    public class OnResponseEvent : UnityEvent<string> { }

    //���Ѵ��ܵ���kGetInstructions()
    public string GetInstructions()
    {
        if (useComprehensivePrompt)
        {
            return "�A�O�@����v�U�бM���B��Ĭ��ԩ�������ä覡�Ӥ޾ɾǥͲ`�J��Ҿ��v���D�A�H�U�O�ǥͪ����D:";
        }
        else
        {
            return "�A�O�@����v�U�СA�M���ϥδ`�Ǻ��i�����C�����D�Ӧ^���ǥͪ�����";
        }
        //string instructions = "�A�O�@����v�U�бM���B��Ĭ��ԩ�������ä覡�Ӥ޾ɾǥͲ`�J��Ҿ��v���D�A�H�U�O�ǥͪ����D:";
        //return instructions;
    }

    //�p�G�n��X���N�^�Ǻ�X�����ܵ��A�_�h�^�Ǥ@�봣�ܵ�
    string GetPrompt()
    {
        if (useComprehensivePrompt)
        {
            return "�C�мҥ�Ĭ��ԩ�����ݥ�60�r���޾ɾǥͪ����D";
        }
        else
        {
            return "�C�Х�60�r�������^���ǥͪ����D";
        }
    }


    //�s�WAskChatGPT()��k�A��J�ѼƬ�string
    public async void AskChatGPT(string newText)
    {          
        ////����API���v
        api = new OpenAIClient(new OpenAIAuthentication(Open_APIKey, Open_ORGKey));
        //var api = new OpenAIClient(new OpenAIAuthentication(Open_APIKey, Open_ORGKey));        

        //���^Assistant
        if (assistantID == null)
        {
            Debug.LogError("Assistant ID is null�A�Цb�s�边��W�U��ID");
        }
        var assistant = await api.AssistantsEndpoint.RetrieveAssistantAsync(assistantID); //asst_9qKD9VPJa2YtwQ3qsbOsQWbd

        ////�إ�Thread
        if (threads == null)
        {
            var thread = await api.ThreadsEndpoint.CreateThreadAsync();
            threads = thread;
        }
        //var threads = await api.ThreadsEndpoint.CreateThreadAsync();
        ////���^Thread
        //var threads = await api.ThreadsEndpoint.RetrieveThreadAsync("thread_QzfPiSz0nsRvl7A8MU6tt63t");

        //�إ�message
        var request = new CreateMessageRequest(GetInstructions() + newText + GetPrompt());
        var message = await api.ThreadsEndpoint.CreateMessageAsync(threads.Id, request);
        Debug.Log($"{message.Id}: {message.Role}: {message.PrintContent()}");

        //����run
        var run = await threads.CreateRunAsync(assistant);

        //����run����
        var runStatus = await threads.RetrieveRunAsync(run);
        Debug.Log($"{runStatus.Status} | [{runStatus.Id}]");
        int Count = 0;

        /// �ɤWRUN FAILED�AFailedc�άOCompleted�N�_�j��
        while (runStatus.Status != RunStatus.Completed )
        {
            Count++;
            //Thread.Sleep(1000);
            StartCoroutine(Wait1Sec(Count));
            runStatus = await threads.RetrieveRunAsync(run.Id);
            runStatus = await runStatus.UpdateAsync();


            Debug.Log($"[{runStatus.Id}] {runStatus.Status} | {runStatus.CreatedAt} | ����s�F{Count}��");
            if(runStatus.Status == RunStatus.Failed)
            {                
                Debug.LogError("Run Failed�A�����^����U�ͦ�Messages");
                break;
            }
        }

        //���^�̷s�@�q���T��
        GetMessage(threads);        
    }

    private void Start()
    {   
        //�s�W�q�\��AskChatGPT()��voiceToText��event�̭�
        voiceToText.DictationEvents.OnFullTranscription.AddListener(AskChatGPT);
    }

    private void Update()
    {
        //�p�G����n��VR�A�A��input action manager�����ѦҡC��Ӫ�������IBUTTON�ӻ��ܡC
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            voiceToText.Activate();
        }
    }

    //�s�W�@�Ӥ�k�A��MainScene�I�s�o�Ӥ�k�AopenAI�����A����APIKey�MORGKey
    public void GetAPI_ORGManager()
    {
        Open_APIKey = mainScene.getAPI();
        Open_ORGKey = mainScene.getORG();
        //var openAI = new OpenAIClient(new OpenAIAuthentication(mainScene.getAPI(), mainScene.getORG()));
        //print(mainScene.getAPI() + "�M" + mainScene.getORG());
    }

    async void GetMessage(ThreadResponse thread)
    {
        var messageList = await thread.ListMessagesAsync();
        var getresponse = messageList.Items[0].PrintContent();
        messages = getresponse;
        Debug.Log($"{messages}");

        ///��r�d�ݭn��令�i�H�ϥΨ��b��ܦh���r
        //�o�X�n��+��s��r�b��r��
        OnResponse.Invoke(messages); //�o�̬O�q���C�����������^���汵��OnResponse���q���A�޼ƨϥ�chatResponse.Content�C���OTTS�άO��r�檺��
    }

    IEnumerator Wait1Sec(int Count)
    {
        messages = "���b���ݦ^��:��" + Count + "��";
        WaitintResponse.Invoke(messages);
        yield return new WaitForSeconds(1);       
    }

    public async void CreateThread()
    {
        //�s�ؤ@��Thread
        var api = new OpenAIClient(new OpenAIAuthentication(Open_APIKey, Open_ORGKey));
        var thread = await api.ThreadsEndpoint.CreateThreadAsync();
        threads = thread;

        print(threads.Id + "�s�Wthread");
    }

    public async void DeleteThread()
    {
        if (threads != null)
        {
            //�R���ª�Thread
            var api = new OpenAIClient(new OpenAIAuthentication(Open_APIKey, Open_ORGKey));
            var isDeleted = await api.ThreadsEndpoint.DeleteThreadAsync(threads.Id);
            Assert.IsTrue(isDeleted);
            print(isDeleted + "�R��thread");
        }
        else
        {
            print("threads��null�A�L�k�R��thread");
        }
        
    }
}

#region Sarge����_�w�o��
////����"�ʧ@"���ܵ���kBuildActionInstructions()�A��NPC�����`�ʧ@�y�z�A�åB�b�^�Ф����w���X�S�w�ʧ@��r
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
///// �ŧiopenAI�A�ᤩOpenAIApi���O�����A�N��l��
///// </summary>
/////�ŧiList�A�x���ϥ�ChatMessage�o��struct
//private OpenAIClient openAI = new OpenAIClient();
//private List<ChatMessage> messages = new List<ChatMessage>();
/////�ŧi�@��Struct ChatMessage���ϰ��ܼ�newMessage�AnewMessage����Content�MRole��]�wstring�r��C
//ChatMessage newMessage = new ChatMessage();
//newMessage.Content = "#zh-tw" + GetInstructions() + newText; //"#zh-tw"+ "#en-us"  	en-us //�n��GPT����^�����ܡA�O�o�]��wit.ai�令����
//newMessage.Role = "user";

/////��ChatMessage���O��newMessage�r��ƾڡA�ᵹ�u����ChatMessage���Omessages��C
//messages.Add(newMessage);

/////�ŧi�ϰ��ܼ�request�A�ᤩCreateChatCompletionRequest�o�����O����
/////request��Messages��C�MModel�r��i�H�~��Ū��+�~���g�J�A�Nmessages��C�ᤩ��Messages�A�åB�]�wModel����
//CreateChatCompletionRequest request = new CreateChatCompletionRequest();
//request.Messages = messages;
//request.Model = "gpt-3.5-turbo"; //gpt-3.5-turbo   gpt-4-1106-preview

/////�]�ߤ@�Ӱϰ��ܼ�response�A��request��@�ѼƩ�iopenAI.CreateChatCompletion()��k�A�z�Lawait���ݤu�@�A�u�@�������ƾڽᤩ��response
/////�o��response�f���FOpenAI���^��
//var response = await openAI.CreateChatCompletion(request);

/////�p�G�~��CreateChatCompletion���ϰ��ܼ�response����Choices��C�ëDnull�����A�A�åB��C����index�j��0
/////��Choices��C�Ĥ@��index���r��T���A�ᵹ�ϰ��ܼ�chatResponse
/////�A��chatResponse��imessages��C
/////����q���C�����������^���汵��OnResponse���q���A�޼ƨϥ�chatResponse.Content
//if (response.Choices != null && response.Choices.Count > 0)
//{
//    var chatResponse = response.Choices[0].Message;

//    foreach (var item in actions)
//    {
//        //�p�G�^�Ф����]�t�ʧ@����r�A�NĲ�o�ʧ@�q���A���P�ɤ]�ݭn��̭����ʧ@����r�M�����A�ܦ��ť�
//        //�o��NPC�~���|���X�L���b���Y�Ӱʧ@����r
//        if (chatResponse.Content.Contains(item.actionKeyword))
//        {
//            string textNoKeyword = chatResponse.Content.Replace(item.actionKeyword, "");
//            chatResponse.Content = textNoKeyword;
//            item.actionEvent?.Invoke();
//        }
//    }

//    messages.Add(chatResponse);
//    Debug.Log(chatResponse.Content);

//    OnResponse.Invoke(chatResponse.Content); //�o�̬O�q���C�����������^���汵��OnResponse���q���A�޼ƨϥ�chatResponse.Content�C���OTTS�άO��r�檺��
//}
#endregion