using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitRecord : MonoBehaviour
{

    public Button start;
    public Button exit;
    public GameObject targetCanvas;
    public GameObject FileManager;
    void Start()
    {
        start.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        // ���ص�ǰ��ť
        start.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);

        if (targetCanvas != null)
        {
            targetCanvas.SetActive(true);
            FileManager.SetActive(true);
        }
        else
        {
            Debug.LogError("�Ҳ���Ŀ��Canvas������·���Ƿ���ȷ");
        }
    }
}