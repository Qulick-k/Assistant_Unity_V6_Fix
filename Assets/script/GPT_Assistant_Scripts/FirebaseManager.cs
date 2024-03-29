using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System.Threading.Tasks; //引用firebase的DB
using TMPro;

public class FirebaseManager : MonoBehaviour
{
    //創建新用戶
    public Firebase.Auth.FirebaseAuth auth;
    public Firebase.Auth.FirebaseUser user;

    ////方便顯示用的inputField
    //[SerializeField] private TMP_InputField inputNote;

    ////測試用email和password
    //private string email = "test@test.com";
    //private static string password ="123456";

    // Start is called before the first frame update
    void Start()
    {
        //取得auth
        //auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        //檢查身分，訂閱StateChanged
        //auth.StateChanged += AuthStateChanged;
        //Register(email, password);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //註冊的接口
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
                print("註冊成功");
            }            
        });
    }

    //新增登入
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
                print("登入成功");
            }           
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }            
        });
    }

    //新增登出
    public void Logout()
    {
        auth.SignOut();
    }

    private void AuthStateChanged(object sender, EventArgs e)
    {
        //檢查當前是不是user
        if (auth.CurrentUser != user)
        {
            user = auth.CurrentUser;  //如果是的話，更新user

            //如果有角色登入
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

    // 儲存 email或是data
    public void SaveData(string data)
    {
        //如果有角色登入
        if(user != null)
        {
            //取得樹狀節點，當作DatabaseReference參考
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
            //每個使用者都創造節點，再設定要存取的資料名字叫email，之後設置數值也就是玩家的email
            //reference.Child(user.UserId).Child("email").SetValueAsync(user.Email).ContinueWith(task =>
            //{
            //    if (task.IsCompletedSuccessfully)
            //    {
            //        print("儲存成功");
            //    };

            //});
            reference.Child(user.UserId).Child("data").SetValueAsync(data).ContinueWith(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    print("儲存成功");
                };

            });
        }
        else
        {
            print("請先登入");
        }            
    }

    // 讀取 玩家資料
    //目前這個方法停用中
    public void LoadData()
    {
        //取得樹狀節點，當作DatabaseReference參考
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        //取得玩家的ID，再取得玩家的資料
        reference.Child(user.UserId).Child("data").GetValueAsync().ContinueWith(task =>
        {
            //如果有角色登入
            if(user != null)
            {
                if (task.IsCompletedSuccessfully)
                {
                    //如果task成功，把task的Result賦予給DataSnapshot
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
                print("請先登入"); 
            }
        });
    }

    //將"取得樹狀節點，當作DatabaseReference參考"的部分提出來，作為方法
    public DatabaseReference GetUserReference()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        return reference.Child(user.UserId);       
    }

    //取得API和ORG密碼的參考
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

    //取得AWSKey和AWSSecretKey
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

    ////物件銷毀，就取消訂閱StateChanged
    //private void OnDestroy()
    //{
    //    auth.StateChanged -= AuthStateChanged;
    //}
}
