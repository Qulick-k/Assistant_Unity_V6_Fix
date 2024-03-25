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
    //���FirebaseManager
    [SerializeField] private FirebaseManager firebaseManager;
    //���ChatGPTManager
    [SerializeField] private ChatGPTManager chatGPTManager;
    private string APIKey;
    private string ORGKey;

    //���TexrToSpeechAWS
    [SerializeField] private TextToSpeechAWS textToSpeechAWS;
    private string AWSKey;
    private string AWSSecretKey;

    [SerializeField] private TMP_InputField inputEmail;
    [SerializeField] private TMP_InputField inputPassword;

    [SerializeField] private GameObject panelLogin;
    [SerializeField] private GameObject panelInfo;

    //��s��remail
    [SerializeField] private TextMeshProUGUI textEmail;
    //��s���O
    [SerializeField] private TMP_InputField inputNote;

    //APIManger����r�BORGManager����r
    [SerializeField] private TextMeshProUGUI inputAPI;
    [SerializeField] private TextMeshProUGUI inputORG;

    private void Awake()
    {
        //�}���@�}�l�N�I�sLoadAPI_ORGManager()�A����_
        LoadAPI_ORGManager();
        LoadAWS_Key();
    }

   

    // Start is called before the first frame update
    void Start()
    {      

        //�q�\StateChanged
        //firebaseManager.auth.StateChanged += AuthStateChanged;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //���US�x�s���
        if(Input.GetKeyDown(KeyCode.S))
        {
            firebaseManager.SaveData(inputNote.text);
        }
        //���UL�x�s���
        if (Input.GetKeyDown(KeyCode.L))
        {
            //firebaseManager.LoadData();
            StartCoroutine(LoadNoteTask());
        }
        */
    }

    //�I���s���U
    public void Register()
    {
        firebaseManager.Register(inputEmail.text, inputPassword.text);
    }

    //�I���s�n�J�C�s�W���f�A�I�sFirebaseManager��Login
    public void Login()
    {
        firebaseManager.Login(inputEmail.text, inputPassword.text);
    }

    //�I���s�n�X�C�s�W���f�A�I�sFirebaseManager��Logout
    public void Logout()
    {
        firebaseManager.Logout();
        //���a���r�M��
        inputNote.text = "";
    }

    //�I���s�x�s�C�s�W���f�A�I�sFirebaseManager��SaveData
    public void SaveData()
    {
        firebaseManager.SaveData(inputNote.text);
    }
    /*
    private void AuthStateChanged(object sender, EventArgs e)
    {
        //�ˬd��e���S��user�n�X�A���N����panelInfo�B���panelLogin�B�M��email���F�S���N�ۤϡC
        if (firebaseManager.user == null)
        {           
            textEmail.text = "";
            panelLogin.SetActive(true);
            panelInfo.SetActive(false);
        }
        else
        {
            textEmail.text = firebaseManager.user.Email;
            //���������A�N�i��Ū�����
            StartCoroutine(LoadNoteTask());
            panelLogin.SetActive(false);
            panelInfo.SetActive(true);
        }
    }
    */

    //�����q�\StateChanged
    //private void OnDestroy()
    //{
    //    firebaseManager.auth.StateChanged -= AuthStateChanged;
    //}

    /*
    //Ū�� ��ƨϥΨ�{
    IEnumerator LoadNoteTask()
    {
        //���o��ڸ`�I
        var task = firebaseManager.GetUserReference().Child("data").GetValueAsync();

        //���ݨ�task�����A�AŪ��task��Result�A�åB��s���O
        yield return new WaitUntil(() => task.IsCompleted);
        DataSnapshot snapshot = task.Result;
        //�p�Gsnapshot���ƭȡA�N��s���O
        if (snapshot.Value != null)
        {
            string note = snapshot.Value.ToString();
            print(note);
            inputNote.text = note;
        }
        else
        {
            print("�S�����");
            inputNote.text = "";
        }        
    }
    */

    //�}���}�l�N�I�sLoadAPI_ORGManager()�A����_�C ��Button�I�sLoadAPI_ORGManager()
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

    //�]��LoadAPITask()��{��A�A�]LoadORGTask()��{
    IEnumerator LoadAPITask()
    {
        //���o��ڸ`�I
        var task = firebaseManager.GetApiManagerReference().GetValueAsync();

        //���ݨ�task�����A�AŪ��task��Result�A�åB��s���O
        yield return new WaitUntil(() => task.IsCompleted);
        DataSnapshot snapshot = task.Result;
        //�p�Gsnapshot���ƭȡA�N��s���O
        if (snapshot.Value != null)
        {
            string note = snapshot.Value.ToString();
            //print(note);
            APIKey = note;
        }
        else
        {
            print("�S�����");
            inputAPI.text = "";
        }

        //�]��LoadAPITask()��{��A�A�]LoadORGTask()��{
        StartCoroutine(LoadORGTask());
    }

    IEnumerator LoadORGTask()
    {
        //���o��ڸ`�I
        var task = firebaseManager.GetORGManagerReference().GetValueAsync();

        //���ݨ�task�����A�AŪ��task��Result�A�åB��s���O
        yield return new WaitUntil(() => task.IsCompleted);
        DataSnapshot snapshot = task.Result;
        //�p�Gsnapshot���ƭȡA�N��s���O
        if (snapshot.Value != null)
        {
            string note = snapshot.Value.ToString();
            //print(note);
            ORGKey = note;
        }
        else
        {
            print("�S�����");
            inputORG.text = "";
        }

        //���]������I�sChatGPTManager�A�i�H����APIKey�MORGKey�F
        chatGPTManager.GetAPI_ORGManager();
    }

    //�]��LoadAWS_Key_Task()��{��ALoadAWS_Secret_Key_Task()��{
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
            print("�S�����");
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
            print("�S�����");
        }

        textToSpeechAWS.GetAWS_KeyManager();
    }
}
