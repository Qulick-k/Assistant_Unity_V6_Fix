using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

[System.Serializable]
public class CraftingSystem : MonoBehaviour
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

    public UnityEngine.UI.Text Mission4;

    private void Awake()
    {
        NextRecipe();
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
            Mission4.text = "<color=green>✓ 4.了解哪些組合材料可以合成(將材料合成)</color>";
            table.GetComponent<SaveSystemCh2>().type = "合成成功";
            table.GetComponent<SaveSystemCh2>().Save();
            if (index == 1)
            {
                SceneManager.LoadScene("2-4");
                //portal.SetActive(true);
            }
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
