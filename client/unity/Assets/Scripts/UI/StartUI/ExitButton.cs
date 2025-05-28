using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{

    public Button exit;
    void Start()
    {
        exit.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        // �ڿ���̨���������Ϣ����ѡ��
        Debug.Log("Exit button clicked");

        // ��ʽ�����˳�
#if UNITY_EDITOR
        // �����Unity�༭�������У�ֹͣ����ģʽ
        EditorApplication.isPlaying = false;
#else
        // �ڷ����İ汾���˳���Ϸ
        Application.Quit();
#endif
    }
}