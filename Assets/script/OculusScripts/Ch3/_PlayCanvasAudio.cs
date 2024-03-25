using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class _PlayCanvasAudio : MonoBehaviour
{
    [SerializeField] private AudioSource[] EndSound;   //設置單元總結用的語音串列
    private int SoundIndex;                            //設置判斷用的index

    private void Start()
    {
        SoundIndex = 0;                                //一開始index設為0
    }

    public void PlaySound()                            //被Unityevent通知的時候，執行PlaySound
    {
        SoundIndex = 0;       //index重新設置為0
        if (EndSound != null) //如果串列內部有東西的話
        {
            EndSound[SoundIndex].Play();  //就從0開始播放
            float _time = EndSound[SoundIndex].clip.length;  //設置區域變數，把當前index中的list的語音提取clip然後判斷出語音長度，再把語音長度賦予給_time
            StartCoroutine(AudioPlay(_time));         //執行協程，並且把_time當作參數丟進去
        }
    }

    private IEnumerator AudioPlay(float time)   //協程收到_time的參數
    {
        yield return new WaitForSeconds(time);   //等待該語音的時間長度
        PlayNextSound();       //等完之後，執行下一個語音
    }

    private void PlayNextSound()     //被協程呼叫後，執行PlayNextSound()
    {
        SoundIndex++;         //index 加1
        if (SoundIndex < EndSound.Count())  //如果當前的index還小於語音串列的長度
        {
            EndSound[SoundIndex].Play();   //那就執行被+1後的index的list中的語音
            float _time = EndSound[SoundIndex].clip.length;  //並且抓取"被+1後的index的list中的語音"的語音長度，再把時間長度賦予給_time
            StartCoroutine(AudioPlay(_time));  //再重新呼叫協程，並且把_time再作為參數丟進去
        }
        else
        {
            print("執行完所有語音了");    //如果index已經"等於"語音串列的長度，就代表播放完成所有語音
        }
    }
}
