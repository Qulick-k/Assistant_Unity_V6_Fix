using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Extensions;

public class SwitchAssistantType : MonoBehaviour
{
    [SerializeField] private Transform comprehensiveAssistant;  //��X���U��
    [SerializeField] private Transform sequentialAssistant;    //�`�ǫ��U��
    //[SerializeField] private bool useComprehensivePrompt;       //�M�w�O�_�ϥκ�X�����ܵ������L�ȡATrue�N�O�ϥκ�X�����ܵ��AFalse�N�O�ϥΤ@�봣�ܵ�

    //�I�����s�I�sOpenComprehensiveAssistant()�A�}�Һ�X���U��A�������`�ǫ��U��A�åB�]�museComprehensivePrompt���ܵ���True
    public void OpenComprehensiveAssistant()
    {
        sequentialAssistant.SetActive(false);
        comprehensiveAssistant.SetActive(true);        
        //useComprehensivePrompt = true;
    }

    //�I�����s�I�sOpenComprehensiveAssistant()�A�}�Ҵ`�ǫ��U��A��������X���U��A�åB�]�museComprehensivePrompt���ܵ���false
    public void OpenSequentialAssistant()
    {
        comprehensiveAssistant.SetActive(false);
        sequentialAssistant.SetActive(true);
        //useComprehensivePrompt = false;
    }
}
