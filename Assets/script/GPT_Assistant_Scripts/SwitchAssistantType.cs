using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Extensions;

public class SwitchAssistantType : MonoBehaviour
{
    [SerializeField] private Transform comprehensiveAssistant;  //��X���U��
    [SerializeField] private Transform sequentialAssistant;    //�`�ǫ��U��
    //[SerializeField] private bool useComprehensivePrompt;       //�M�w�O�_�ϥκ�X�����ܵ������L�ȡATrue�N�O�ϥκ�X�����ܵ��AFalse�N�O�ϥΤ@�봣�ܵ�

    private void Start()
    {
        //�p�G�ݭn�޾ɡA�N�}�Ҵ`�ǫ����ȲM��B������X�����ȲM��F�p�G���ݭn�޾ɡA�N�}�Һ�X�����ȲM��B�����`�ǫ����ȲM��
        if (KeepData.guideSwitch == true)
        {
            sequentialAssistant.gameObject.SetActive(true);
            comprehensiveAssistant.gameObject.SetActive(false);
        }
        else
        {
            sequentialAssistant.gameObject.SetActive(false);
            comprehensiveAssistant.gameObject.SetActive(true);
        }
    }


    //�I�����s�I�sOpenComprehensiveAssistant()�A�}�Һ�X���U��A�������`�ǫ��U��A�åB�]�museComprehensivePrompt���ܵ���True
    public void OpenComprehensiveAssistant()
    {
        if (KeepData.guideSwitch != true)
        {
            sequentialAssistant.SetActive(false);
            comprehensiveAssistant.SetActive(true);
        }
        else
        {
            print("�w�}�Ҵ`�ǫ��P�_�A����}�Һ�X���U��");
        }          
        //useComprehensivePrompt = true;
    }

    //�I�����s�I�sOpenSequentialAssistant()�A�}�Ҵ`�ǫ��U��A��������X���U��A�åB�]�museComprehensivePrompt���ܵ���false
    public void OpenSequentialAssistant()
    {
        if (KeepData.guideSwitch)
        {
            comprehensiveAssistant.SetActive(false);
            sequentialAssistant.SetActive(true);
        }
        else
        {
            print("�w�}�Һ�X���P�_�A����}�Ҵ`�ǫ��U��");
        }        
        //useComprehensivePrompt = false;
    }
}
