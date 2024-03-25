using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEditor;

public class ChangeScene : MonoBehaviour
{
    public GameObject[] scenes;
    public int index = 0;

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
        
    }

    public void ChangeToChapterOne()
    {
    SceneManager.LoadScene("lesson2");
    }
    public void ChangeToChapterTwo()
    {
        SceneManager.LoadScene("2-1");
    }
    public void ChangeToChapterThree()
    {
        SceneManager.LoadScene("Chapter3_ChooseArea_Scene");
    }

    public void ChangeToChapterFour()
    {
        SceneManager.LoadScene("scenes4");
    }
    public void ChangeToChapterFive()
    {
        SceneManager.LoadScene("Lesson5-1");
    }
    public void ChangeToChapterSix()
    {
        SceneManager.LoadScene("lesson6_1");
    }
    public void ClickRight()
    {
        scenes[index].SetActive(false);
        index++;
        scenes[index].SetActive(true);

    }
    public void ClickLeft()
    {
        scenes[index].SetActive(false);
        index--;
        scenes[index].SetActive(true);

    }
}
