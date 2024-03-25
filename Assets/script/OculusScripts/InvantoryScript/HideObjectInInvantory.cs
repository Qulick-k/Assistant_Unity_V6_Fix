using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HideObjectInInvantory : MonoBehaviour
{
    //Show出來的欄位
    public Image artStuff;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    static public int indexObjectsAudio;

    //佔存的欄位
    private Sprite artStuff_Cache;
    private string titleText_Cache;
    private string descriptionText_Cache;
    private int indexObjectsAudio_Cache;

    public GameObject ParentObject;
    private GameObject setGameObject;

    private bool DetectObjectInCollider;
    private bool DetectObject_ForImage_StatueInCollider;
    private bool DetectObject_ForBrand_Haishen_InCollider;

    // Start is called before the first frame update
    void Start()
    {
        setGameObject = null;

        //暫存欄位一開始都設為空值
        artStuff_Cache = null;
        titleText_Cache = null;
        descriptionText_Cache = null;

        InventoryCanvas callOpen = ParentObject.GetComponent<InventoryCanvas>();
        InventoryCanvas callClose = ParentObject.GetComponent<InventoryCanvas>();
        callOpen.callOpenMeshRenderer += CallOpen_callOpenMeshRenderer;
        callClose.callCloseMeshRenderer += CallClose_callCloseMeshRenderer;
    }

    private void CallClose_callCloseMeshRenderer(object sender, System.EventArgs e)
    {
        if (setGameObject != null)
        {
            setGameObject.GetComponentInChildren<MeshRenderer>().enabled = false;

            List<Transform> children = GetChildren(setGameObject.transform, true);
            foreach (Transform child in children)
            {
                if (child.GetComponent<MeshRenderer>() == true)
                {
                    child.GetComponent<MeshRenderer>().enabled = false;
                }
                else
                {
                    continue;
                }
            }
        }
        else
        {
            Debug.Log("關閉時setGameObject是空值");
            return;
        }
    }
    

    private void CallOpen_callOpenMeshRenderer(object sender, System.EventArgs e)
    {
        //Debug.Log(setGameObject.GetComponent<DetectObject>().inCollider);
        //如果掛載物件符合以下條件
        //1. null
        //2. DetectObject腳本的inCollider為true
        //3. DetectObject_ForImage_Statue腳本的inCollider為true
        //4. DetectObject_ForBrand_Haishen_腳本的inCollider為true
        getDetectObjectbool(setGameObject);                          //傳入setGameObject給方法做判斷，看bool值是true還是false
        getDetectObject_ForImage_Statuebool(setGameObject);
        getDetectObject_ForBrand_Haishen_bool(setGameObject);
        if (setGameObject == null || DetectObjectInCollider == true || DetectObject_ForImage_StatueInCollider == true || DetectObject_ForBrand_Haishen_InCollider == true)
        {           
            //Debug.Log("開啟時setGameObject是空值");
            return;
        }
        else
        {
            Debug.Log(setGameObject.GetComponent<DetectObject>());   //會回報NULL
            //GetComponentInChildren
            setGameObject.GetComponentInChildren<MeshRenderer>().enabled = true;

            List<Transform> children = GetChildren(setGameObject.transform, true);
            foreach (Transform child in children)
            {
                if (child.GetComponent<MeshRenderer>() == true)
                {
                    child.GetComponent<MeshRenderer>().enabled = true;
                }
                else
                {
                    continue;
                }
            }
            Debug.Log("渲染開起來了");
        }
    }
    private void getDetectObjectbool(GameObject setGameObject)                        //判斷各個物件有沒有在碰撞體裡面，在裡面的話就回傳true
    {
        if (setGameObject != null && setGameObject.GetComponent<DetectObject>())
        {
            if (setGameObject.GetComponent<DetectObject>().inCollider == true)
            {
                DetectObjectInCollider = true;
            }
            else
            {
                DetectObjectInCollider = false;
            }
        }
        else
        {
            DetectObjectInCollider = false;
        }
    }
    private void getDetectObject_ForImage_Statuebool(GameObject setGameObject)         //判斷各個物件有沒有在碰撞體裡面，在裡面的話就回傳true
    {
        if (setGameObject != null && setGameObject.GetComponent<DetectObject_ForImage_Statue>())
        {
            if (setGameObject.GetComponent<DetectObject_ForImage_Statue>().inCollider == true)
            {
                DetectObject_ForImage_StatueInCollider = true;
            }
            else
            {
                DetectObject_ForImage_StatueInCollider = false;
            }
        }
        else
        {
            DetectObject_ForImage_StatueInCollider = false;
        }
    }
    private void getDetectObject_ForBrand_Haishen_bool(GameObject setGameObject)        //判斷各個物件有沒有在碰撞體裡面，在裡面的話就回傳true
    {
        if (setGameObject != null && setGameObject.GetComponent<DetectObject_ForBrand_Haishen_>())
        {
            if (setGameObject.GetComponent<DetectObject_ForBrand_Haishen_>().inCollider == true)
            {
                DetectObject_ForBrand_Haishen_InCollider = true;
            }
            else
            {
                DetectObject_ForBrand_Haishen_InCollider = false;
            }
        }
        else
        {
            DetectObject_ForBrand_Haishen_InCollider = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Sugar" || other.gameObject.tag == "huaiPaper")
        {
            setGameObject = null;
            artStuff_Cache = null;
            titleText_Cache = null;
            descriptionText_Cache = null;
            indexObjectsAudio_Cache = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sugar" || other.gameObject.tag == "huaiPaper")
        {
            Debug.Log("背包系統碰到甘蔗或文件了");
            setGameObject = other.gameObject;

            //從甘蔗或文件上的scriptable Object抓取圖、標題和描述
            //甘蔗的SO放在PickUP腳本，郭懷一文件的SO
            LessonTwoSO localSOinfo = setGameObject.GetComponent<_SOHolder>().SOinfo;

            Sprite artSprite = localSOinfo.artWork; //setGameObject.GetComponent<Image>().sprite;
            string artTitle = localSOinfo.title;    //setGameObject.GetComponent<TMP_Text>().text;
            string artDescription = localSOinfo.description;  //setGameObject.GetComponent<TMP_Text>().text;
            int indexObjectLocal = localSOinfo.indexObject;
            getSpriteDescription(artSprite, artTitle, artDescription, indexObjectLocal);
        }
    }

    public void getSpriteDescription(Sprite image, string title, string description, int index) //再加個int index
    {
        //抓取到的甘蔗或是郭懷一文件的圖片(sprite)、標題(text)和描述(text)
        //再放到暫存區Cache
        artStuff_Cache = image;
        titleText_Cache = title;
        descriptionText_Cache = description;
        indexObjectsAudio_Cache = index;
    }


    //當背包系統被hover的時候，就更新顯示當前物件圖片、標題和描述
    //由ItemUI底下的Border物件掛載的XR Socket Interactor中的Hover Event呼叫
    public void showSpriteDescription()
    {
        Debug.Log("被hover了");
        artStuff.sprite = artStuff_Cache;
        titleText.text = titleText_Cache;
        descriptionText.text = descriptionText_Cache;
        indexObjectsAudio = indexObjectsAudio_Cache;
    }
    List<Transform> GetChildren(Transform parent, bool recursive)
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in parent)
        {
            children.Add(child);
            if (recursive)
            {
                children.AddRange(GetChildren(child, true));
            }
        }
        return children;
    }
}
