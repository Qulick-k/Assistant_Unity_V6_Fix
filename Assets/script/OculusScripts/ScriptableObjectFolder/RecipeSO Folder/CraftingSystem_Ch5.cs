using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CraftingSystem_Ch5 : MonoBehaviour
{
    [SerializeField] private BoxCollider placeItemsAreaBoxCollider;
    [SerializeField] private List<CraftingRecipeSO> craftingRecipeSOList;
    [SerializeField] private Transform itemSpawnPoint;
    //[SerializeField] private Transform vfxSpawnItem;這個是特效

    private CraftingRecipeSO craftingRecipeSO;

    public List<string> recipeContent;
    public TMP_Text recipeContentText;

    [SerializeField] string type;
    public GameObject table, portal;
    int index;

    public UnityEngine.UI.Text Mission;
    [SerializeField] private string MissionContent;
    private int CraftCount;
    private bool First_close;
    private bool Second_close;
    [SerializeField] private bool IsIn5_1;

    private void Awake()
    {
        NextRecipe();
        CraftCount = 0;
        First_close = true;
        Second_close = true;
    }
    public void NextRecipe()
    {
        if (craftingRecipeSOList == null)
        {
            craftingRecipeSO = craftingRecipeSOList[0]; //List從頭開始
            recipeContentText.text = recipeContent[0].ToString();//將list的內容轉換為string，再傳到canvas上的recipecontentText
            Debug.Log("Restart recipe list");
        }
        else
        {
            index = craftingRecipeSOList.IndexOf(craftingRecipeSO); //取得目前list的index
            index = (index + 1) % craftingRecipeSOList.Count;
            craftingRecipeSO = craftingRecipeSOList[index];
            recipeContentText.text = recipeContent[index].ToString();
            Debug.Log("Next recipe");
        }
    }

    public void Craft()
    {
        //Debug.Log("craft");
        Collider[] colliderArray = Physics.OverlapBox(transform.position + placeItemsAreaBoxCollider.center, placeItemsAreaBoxCollider.size, placeItemsAreaBoxCollider.transform.rotation);

        List<ItemSO> inputItemList = new List<ItemSO>(craftingRecipeSO.inputItemSOList);
        List<GameObject> consumeItemGameObjectList = new List<GameObject>();

        foreach (Collider collider in colliderArray)
        {
            Debug.Log(collider);
            if (collider.TryGetComponent(out RecipeSOHolder recipeSOHolder))
            {
                //Debug.Log(recipeSOHolder);
                if (inputItemList.Contains(recipeSOHolder.SOinfo))
                {
                    //Debug.Log(inputItemList);
                    inputItemList.Remove(recipeSOHolder.SOinfo);
                    consumeItemGameObjectList.Add(collider.gameObject);
                }

            }
        }

        if (inputItemList.Count == 0)
        {
            type = "合成成功";
            Debug.Log("yes");
            Transform spawnedItemTransform = Instantiate(craftingRecipeSO.outputItemSO.prefab.transform, itemSpawnPoint.position, itemSpawnPoint.rotation);

            foreach (GameObject consumeItemGameObject in consumeItemGameObjectList)
            {
                Destroy(consumeItemGameObject);
            }

            

            if (IsIn5_1 == false)   //如果現在的場景不在5-1，就執行5-4的判斷
            {
                if (index == 0 && First_close == true)  //現在如果是合成四海龍王圖的話，就CraftCount++;
                {
                    CraftCount++;
                    First_close = false;
                }
                if (index == 1 && Second_close == true) //現在如果是合成海神廟門牌的話，就CraftCount++;
                {
                    CraftCount++;
                    Second_close = false;
                }
                if (CraftCount == 2)  //當CraftCount是2的時候，就變換任務描述的顏色
                {
                    //portal.SetActive(true);
                    Mission.text = MissionContent;
                }
            }
            else   //如果現在的場景是在5-1(在編輯器裡面勾起IsIn5_1這個布林值為true)，就直接執行5-1的程式碼，因為5-1目前只需要合成出1個物件，所以直接變換任務描述和顏色即可。
            {
                portal.SetActive(true);
                Mission.text = MissionContent;
            }
            //為了避免單元5、6，LOG到第2單元，XROrigin也需要把SaveSystemCh2腳本換掉
            table.GetComponent<SaveSystemCh2>().type = "合成成功";
            table.GetComponent<SaveSystemCh2>().Save();

        }
        else
        {
            table.GetComponent<SaveSystemCh2>().type = "合成失敗";
            table.GetComponent<SaveSystemCh2>().Save();
        }

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))//測試用
        {
            Craft();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextRecipe();
        }
    }
}
