using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepData : MonoBehaviour
{
    public static string loginName;
    public static bool guideSwitch; // true: ���޾ɡA���`�ǫ� �Ffalse: �S�޾ɡA����X��

    private void Start()
    {
        guideSwitch = true;        // �w�]���`�ǫ�
    }
}
