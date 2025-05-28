using BattleCity;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FileManager : MonoBehaviour
{
    private Button AddFile;
    private Button Remove;
    private Button Exit;
    public GameObject UIPlane;
    public GameObject LoadingPlane;
    public GameObject GameCanvas;
    public GameObject StartCanvas;
    // public GameObject RecordPlay;
    public static List<string> SelectedFilePaths { get; private set; } = new List<string>();

    public Transform contentParent; // ScrollView��Content����
    public GameObject fileButtonPrefab; // �ļ���ťԤ����
    private bool isRemovingMode = false; // ����ɾ��ģʽ��־
    private Image removeButtonImage; // ����Image�������
    private Color originalColor = Color.white; // ��ʼ��ɫ
    public CameraController cameraController;


    private string GetStreamingAssetPath(string relativePath)
    {
        return Path.Combine(Application.streamingAssetsPath, relativePath);
    }


    void Start()
    {
        SceneData.GameStage = "Start";
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCamera != null)
        {
            cameraController = mainCamera.GetComponent<CameraController>();
        }
        StartCanvas = GameObject.Find("StartCanvas");
        AddFile = GameObject.Find("StartCanvas/Window/UIPlane/AddFile").GetComponent<Button>();
        Remove = GameObject.Find("StartCanvas/Window/UIPlane/Remove").GetComponent<Button>();
        Exit = GameObject.Find("StartCanvas/Window/UIPlane/Exit").GetComponent<Button>();
        contentParent = GameObject.Find("StartCanvas/Window/UIPlane/Scroll View/Viewport/Content").GetComponentInParent<Transform>();
        fileButtonPrefab = Resources.Load<GameObject>("UI/Buttons/recordButton");
        Transform backgroundChild = Remove.transform.Find("Background");

        removeButtonImage = backgroundChild.GetComponent<Image>();

        originalColor = removeButtonImage.color;
        Remove.targetGraphic = removeButtonImage;

        SelectedFilePaths.Add(GetStreamingAssetPath("Test.json"));
        UpdateFileListUI();

        AddFile.onClick.AddListener(() =>
        {
            StartCoroutine(SelectFileAndUpdate());
        });
        Remove.onClick.AddListener(() =>
        {
            isRemovingMode = !isRemovingMode;
            UpdateRemoveButtonColor();
        });
        Exit.onClick.AddListener(() => ExitFileManager());
        TypeEventSystem.Global.Register<LoadingEvent>(e =>
        {
            HidePlane();
        });
        TypeEventSystem.Global.Register<BattleEndEvent>(e =>
        {
            ShowPlane();
        });
    }

    void HidePlane()
    {
        UIPlane.SetActive(false);
        LoadingPlane.SetActive(true);
    }

    void ShowPlane()
    {
        UIPlane.SetActive(true);
        LoadingPlane.SetActive(false);
    }

    IEnumerator SelectFileAndUpdate()
    {
        yield return FileSelect.SelectFile(SelectedFilePaths);
        UpdateFileListUI();
    }

    void UpdateFileListUI()
    {
        // ��վ��б�
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
        Debug.Log($"the length of FilePaths: {SelectedFilePaths.Count}");
        // �������б�
        foreach (string filePath in SelectedFilePaths)
        {
            GameObject buttonObj = Instantiate(fileButtonPrefab, contentParent);
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            Transform textChild = buttonObj.transform.Find("Background/Test");
            string displayName = fileName.Length > 6 ? fileName.Substring(0, 6) : fileName;
            textChild.GetComponentInChildren<TMP_Text>().text = displayName;

            // ��ӵ���¼�
            buttonObj.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (isRemovingMode)
                {
                    // ɾ������
                    SelectedFilePaths.Remove(filePath);
                    UpdateFileListUI(); // ����ˢ��UI
                }
                else
                {
                    // ѡ�����
                    OnFileSelected(filePath);
                }
            });
        }
    }
    void UpdateRemoveButtonColor()
    {
        removeButtonImage.color = isRemovingMode ? Color.red : originalColor;
        
        Debug.Log(removeButtonImage.color);
        Remove.OnPointerExit(null); // ����״̬����
        if (removeButtonImage.canvasRenderer != null)
        {
            removeButtonImage.canvasRenderer.SetColor(removeButtonImage.color);
        }
    }
    void ExitFileManager()
    {
#if UNITY_EDITOR
        // �����Unity�༭�������У�ֹͣ����ģʽ
        EditorApplication.isPlaying = false;
#else
        // �ڷ����İ汾���˳���Ϸ
        Application.Quit();
#endif
    }
    void OnFileSelected(string filePath)
    {
        SceneData.FilePath = filePath;
        SceneData.GameStage = "Loading";
        TypeEventSystem.Global.Send( new LoadingEvent());
        // RecordPlay.SetActive(true);
        // cameraController.enabled = !cameraController.enabled;
    }
}



