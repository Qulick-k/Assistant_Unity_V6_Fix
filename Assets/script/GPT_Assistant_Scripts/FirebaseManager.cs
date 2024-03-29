using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System.Threading.Tasks; //�ޥ�firebase��DB
using TMPro;

public class FirebaseManager : MonoBehaviour
{
    //�Ыطs�Τ�
    public Firebase.Auth.FirebaseAuth auth;
    public Firebase.Auth.FirebaseUser user;

    ////��K��ܥΪ�inputField
    //[SerializeField] private TMP_InputField inputNote;

    ////���ե�email�Mpassword
    //private string email = "test@test.com";
    //private static string password ="123456";

    // Start is called before the first frame update
    void Start()
    {
        //���oauth
        //auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        //�ˬd�����A�q�\StateChanged
        //auth.StateChanged += AuthStateChanged;
        //Register(email, password);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //���U�����f
    public void Register(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception.InnerException.Message);
                return;
            }
            if (task.IsCompletedSuccessfully)
            {
                print("���U���\");
            }            
        });
    }

    //�s�W�n�J
    public async void Login(string email, string password)
    {
        await auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception.InnerException.Message);
                return;
            }
            if (task.IsCompletedSuccessfully)
            {
                print("�n�J���\");
            }           
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }            
        });
    }

    //�s�W�n�X
    public void Logout()
    {
        auth.SignOut();
    }

    private void AuthStateChanged(object sender, EventArgs e)
    {
        //�ˬd��e�O���Ouser
        if (auth.CurrentUser != user)
        {
            user = auth.CurrentUser;  //�p�G�O���ܡA��suser

            //�p�G������n�J
            if(user != null)
            {
                Debug.Log($"Login - {user.Email}");
            }
            else
            {
                Debug.Log("User has been signed out");
            }
            
        }
    }

    // �x�s email�άOdata
    public void SaveData(string data)
    {
        //�p�G������n�J
        if(user != null)
        {
            //���o�𪬸`�I�A��@DatabaseReference�Ѧ�
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
            //�C�ӨϥΪ̳��гy�`�I�A�A�]�w�n�s������ƦW�r�semail�A����]�m�ƭȤ]�N�O���a��email
            //reference.Child(user.UserId).Child("email").SetValueAsync(user.Email).ContinueWith(task =>
            //{
            //    if (task.IsCompletedSuccessfully)
            //    {
            //        print("�x�s���\");
            //    };

            //});
            reference.Child(user.UserId).Child("data").SetValueAsync(data).ContinueWith(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    print("�x�s���\");
                };

            });
        }
        else
        {
            print("�Х��n�J");
        }            
    }

    // Ū�� ���a���
    //�ثe�o�Ӥ�k���Τ�
    public void LoadData()
    {
        //���o�𪬸`�I�A��@DatabaseReference�Ѧ�
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        //���o���a��ID�A�A���o���a�����
        reference.Child(user.UserId).Child("data").GetValueAsync().ContinueWith(task =>
        {
            //�p�G������n�J
            if(user != null)
            {
                if (task.IsCompletedSuccessfully)
                {
                    //�p�Gtask���\�A��task��Result�ᤩ��DataSnapshot
                    DataSnapshot snapshot = task.Result;
                    print(snapshot.Value);
                    //inputNote.text = snapshot.Value.ToString();
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("GetValueAsync encountered an error: " + task.Exception.InnerException.Message);
                    return;
                }
            }
            else 
            {
                print("�Х��n�J"); 
            }
        });
    }

    //�N"���o�𪬸`�I�A��@DatabaseReference�Ѧ�"���������X�ӡA�@����k
    public DatabaseReference GetUserReference()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        return reference.Child(user.UserId);       
    }

    //���oAPI�MORG�K�X���Ѧ�
    public DatabaseReference GetApiManagerReference()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        return reference.Child("APIManager");
    }
    public DatabaseReference GetORGManagerReference()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        return reference.Child("ORGManager");
    }

    //���oAWSKey�MAWSSecretKey
    public DatabaseReference GetAWS_Polly_Access_KeyReference()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        return reference.Child("AWS_Polly_Access_Key");
    }
    public DatabaseReference GetAWS_Polly_Secret_Access_KeyReference()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        return reference.Child("AWS_Polly_Secret_Access_Key");
    }

    ////����P���A�N�����q�\StateChanged
    //private void OnDestroy()
    //{
    //    auth.StateChanged -= AuthStateChanged;
    //}
}
