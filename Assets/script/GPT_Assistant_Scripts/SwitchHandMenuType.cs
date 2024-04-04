using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchHandMenuType : MonoBehaviour
{
    [SerializeField] private Transform guideOnMenu;
    [SerializeField] private Transform guideOffMenu;
    [SerializeField] private Text switchAlpha;

    private void Start()
    {
        //KeepData.guideSwitch = true;
        //�p�G�ݭn�޾ɡA�N�}�Ҵ`�ǫ����ȲM��B������X�����ȲM��F�p�G���ݭn�޾ɡA�N�}�Һ�X�����ȲM��B�����`�ǫ����ȲM��
        if (KeepData.guideSwitch == true)
        {
            guideOnMenu.gameObject.SetActive(true);
            guideOffMenu.gameObject.SetActive(false);
        }
        else
        {
            guideOnMenu.gameObject.SetActive(false);
            guideOffMenu.gameObject.SetActive(true);
        }
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        //��ګ��UA��A�N���r�զ����`�C��
    //        switchAlpha.color = new Color(switchAlpha.color.r, switchAlpha.color.g, switchAlpha.color.b, 1);

    //    }
    //}
}
