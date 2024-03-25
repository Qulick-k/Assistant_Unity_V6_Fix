using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using OpenAI;
using System;
using Firebase.Database;
public class MainScene : MonoBehaviour
{
    //抓取FirebaseManager
    [SerializeField] private FirebaseManager firebaseManager;
    //抓取ChatGPTManager
    [SerializeField] private ChatGPTManager chatGPTManager;
    private string APIKey;
    private string ORGKey;

    //抓取TexrToSpeechAWS
    [SerializeField] private TextToSpeechAWS textToSpeechAWS;
    private string AWSKey;
    private string AWSSecretKey;

    [SerializeField] private TMP_InputField inputEmail;
    [SerializeField] private TMP_InputField inputPassword;

    [SerializeField] private GameObject panelLogin;
    [SerializeField] private GameObject panelInfo;

    //更新文字email
    [SerializeField] private TextMeshProUGUI textEmail;
    //更新筆記
    [SerializeField] private TMP_InputField inputNote;

    //APIManger的文字、ORGManager的文字
    [SerializeField] private TextMeshProUGUI inputAPI;
    [SerializeField] private TextMeshProUGUI inputORG;

    private void Awake()
    {
        //腳本一開始就呼叫LoadAPI_ORGManager()，抓金鑰
        LoadAPI_ORGManager();
        LoadAWS_Key();
    }

   

    // Start is called before the first frame update
    void Start()
    {      

        //訂閱StateChanged
        //firebaseManager.auth.StateChanged += AuthStateChanged;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //按下S儲存資料
        if(Input.GetKeyDown(KeyCode.S))
        {
            firebaseManager.SaveData(inputNote.text);
        }
        //按下L儲存資料
        if (Input.GetKeyDown(KeyCode.L))
        {
            //firebaseManager.LoadData();
            StartCoroutine(LoadNoteTask());
        }
        */
    }

    //點按鈕註冊
    public void Register()
    {
        firebaseManager.Register(inputEmail.text, inputPassword.text);
    }

    //點按鈕登入。新增接口，呼叫FirebaseManager的Login
    public void Login()
    {
        firebaseManager.Login(inputEmail.text, inputPassword.text);
    }

    //點按鈕登出。新增接口，呼叫FirebaseManager的Logout
    public void Logout()
    {
        firebaseManager.Logout();
        //順帶把文字清除
        inputNote.text = "";
    }

    //點按鈕儲存。新增接口，呼叫FirebaseManager的SaveData
    public void SaveData()
    {
        firebaseManager.SaveData(inputNote.text);
    }
    /*
    private void AuthStateChanged(object sender, EventArgs e)
    {
        //檢查當前有沒有user登出，有就關掉panelInfo、顯示panelLogin、清空email欄位；沒有就相反。
        if (firebaseManager.user == null)
        {           
            textEmail.text = "";
            panelLogin.SetActive(true);
            panelInfo.SetActive(false);
        }
        else
        {
            textEmail.text = firebaseManager.user.Email;
            //當切換角色，就進行讀取資料
            StartCoroutine(LoadNoteTask());
            panelLogin.SetActive(false);
            panelInfo.SetActive(true);
        }
    }
    */

    //取消訂閱StateChanged
    //private void OnDestroy()
    //{
    //    firebaseManager.auth.StateChanged -= AuthStateChanged;
    //}

    /*
    //讀取 資料使用協程
    IEnumerator LoadNoteTask()
    {
        //取得樹根節點
        var task = firebaseManager.GetUserReference().Child("data").GetValueAsync();

        //等待到task完成，再讀取task的Result，並且更新筆記
        yield return new WaitUntil(() => task.IsCompleted);
        DataSnapshot snapshot = task.Result;
        //如果snapshot有數值，就更新筆記
        if (snapshot.Value != null)
        {
            string note = snapshot.Value.ToString();
            print(note);
            inputNote.text = note;
        }
        else
        {
            print("沒有資料");
            inputNote.text = "";
        }        
    }
    */

    //腳本開始就呼叫LoadAPI_ORGManager()，抓金鑰。 讓Button呼叫LoadAPI_ORGManager()
    public void LoadAPI_ORGManager()
    {
        StartCoroutine(LoadAPITask());       
    }
    private void LoadAWS_Key()
    {
        StartCoroutine(LoadAWS_Key_Task());
    }
    
    public string getAPI()
    {
        string API = APIKey;       
        return API;
    }
    public string getORG()
    {
        string ORG = ORGKey;
        return ORG;
    }

    public string getAWSKey()
    {
        string AWSKEY = AWSKey;
        return AWSKEY;
    }

    public string getAWSSecretKey()
    {
        string AWSSECRETKEY = AWSSecretKey;
        return AWSSECRETKEY;
    }

    //跑完LoadAPITask()協程後，再跑LoadORGTask()協程
    IEnumerator LoadAPITask()
    {
        //取得樹根節點
        var task = firebaseManager.GetApiManagerReference().GetValueAsync();

        //等待到task完成，再讀取task的Result，並且更新筆記
        yield return new WaitUntil(() => task.IsCompleted);
        DataSnapshot snapshot = task.Result;
        //如果snapshot有數值，就更新筆記
        if (snapshot.Value != null)
        {
            string note = snapshot.Value.ToString();
            //print(note);
            APIKey = note;
        }
        else
        {
            print("沒有資料");
            inputAPI.text = "";
        }

        //跑完LoadAPITask()協程後，再跑LoadORGTask()協程
        StartCoroutine(LoadORGTask());
    }

    IEnumerator LoadORGTask()
    {
        //取得樹根節點
        var task = firebaseManager.GetORGManagerReference().GetValueAsync();

        //等待到task完成，再讀取task的Result，並且更新筆記
        yield return new WaitUntil(() => task.IsCompleted);
        DataSnapshot snapshot = task.Result;
        //如果snapshot有數值，就更新筆記
        if (snapshot.Value != null)
        {
            string note = snapshot.Value.ToString();
            //print(note);
            ORGKey = note;
        }
        else
        {
            print("沒有資料");
            inputORG.text = "";
        }

        //全跑完之後呼叫ChatGPTManager，可以拿取APIKey和ORGKey了
        chatGPTManager.GetAPI_ORGManager();
    }

    //跑完LoadAWS_Key_Task()協程後，LoadAWS_Secret_Key_Task()協程
    IEnumerator LoadAWS_Key_Task()
    {
        var task = firebaseManager.GetAWS_Polly_Access_KeyReference().GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);
        DataSnapshot snapshot = task.Result;

        if (snapshot.Value != null)
        {
            string note = snapshot.Value.ToString();
            AWSKey = note;
        }
        else
        {
            print("沒有資料");
        }

        StartCoroutine(LoadAWS_Secret_Key_Task());
    }

    IEnumerator LoadAWS_Secret_Key_Task()
    {
        var task = firebaseManager.GetAWS_Polly_Secret_Access_KeyReference().GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);
        DataSnapshot snapshot = task.Result;

        if (snapshot.Value != null)
        {
            string note = snapshot.Value.ToString();
            AWSSecretKey = note;
        }
        else
        {
            print("沒有資料");
        }

        textToSpeechAWS.GetAWS_KeyManager();
    }
}
