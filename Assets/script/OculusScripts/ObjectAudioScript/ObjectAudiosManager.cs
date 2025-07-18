using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectAudiosManager : MonoBehaviour
{
    ///使用者每按一次語音時，當saveAudioEvent有訂閱者的話，就呼叫訂閱者的LogMissionComplete()記錄使用了什麼語音
    public UnityEvent<string> saveAudioEvent;

    [SerializeField] private AudioSource[] OA_List;

    private int OA_index;

    private void Start()
    {
        OA_index = 0;
    }

    public void PlayObjectAudio()
    {
        OA_index = HideObjectInInvantory.indexObjectsAudio;

        switch (OA_index)
        {
            case 1: 
                OA_List[0].Play();
                saveAudioEvent.Invoke("成功播放，1.郭懷一的小人偶語音");
                break;
            case 2:
                OA_List[1].Play();
                saveAudioEvent.Invoke("成功播放，2.甘蔗語音");
                break;
            case 3:
                OA_List[2].Play();
                saveAudioEvent.Invoke("成功播放，3.郭懷一的筆記文件語音");
                break;
            case 4:
                OA_List[3].Play();
                saveAudioEvent.Invoke("成功播放，4.紅磚石語音");
                break;
            case 5:
                OA_List[4].Play();
                saveAudioEvent.Invoke("成功播放，5.糯米漿語音");
                break;
            case 6:
                OA_List[5].Play();
                saveAudioEvent.Invoke("成功播放，6.蚵殼灰語音");
                break;
            case 7:
                OA_List[6].Play();
                saveAudioEvent.Invoke("成功播放，7.糖水語音");
                break;
            case 8:
                OA_List[7].Play();
                break;
            case 9:
                OA_List[8].Play();
                break;
            case 10:
                OA_List[9].Play();
                break;
            case 11:
                OA_List[10].Play();
                break;
            case 12:
                OA_List[11].Play();
                break;
            case 13:
                OA_List[12].Play();
                break;
            case 14:
                OA_List[13].Play();
                break;
            case 15:
                OA_List[14].Play();
                break;
            case 16:
                OA_List[15].Play();
                break;
            case 17:
                OA_List[16].Play();
                break;
            case 18:
                OA_List[17].Play();
                break;
            case 19:
                OA_List[18].Play();
                break;
            case 20:
                OA_List[19].Play();
                break;
            case 21:
                OA_List[20].Play();
                break;
            case 22:
                OA_List[21].Play();
                break;
            case 23:
                OA_List[22].Play();
                break;
            case 24:
                OA_List[23].Play();
                break;
            case 25:
                OA_List[24].Play();
                break;
            case 26:
                OA_List[25].Play();
                break;
            case 27:
                OA_List[26].Play();
                break;
            case 28:
                OA_List[27].Play();
                break;
            case 29:
                OA_List[28].Play();
                break;
            case 30:
                OA_List[29].Play();
                break;
            case 31:
                OA_List[30].Play();
                break;
            case 32:
                OA_List[31].Play();
                break;
            case 33:
                OA_List[32].Play();
                break;
            case 34:
                OA_List[33].Play();
                break;
            case 35:
                OA_List[34].Play();
                break;
            case 36:
                OA_List[35].Play();
                break;
            case 37:
                OA_List[36].Play();
                break;
            case 38:
                OA_List[37].Play();
                break;
            default:
                break;
        }
    }
}
