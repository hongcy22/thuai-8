using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic; // ���List���������ռ�
using SimpleFileBrowser;

public class FileSelect : MonoBehaviour
{
    // ����ʵ��
    public static FileSelect Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ��ѡ���糡������
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static IEnumerator SelectFile(List<string> SelectedFilePaths)
    {
        // �����ļ������������.dat��.json��.zip֧�֣�
        FileBrowser.SetFilters(
            true,
            new FileBrowser.Filter("Supported Files", ".dat", ".json", ".zip")
        );

        // ����Ĭ�Ϲ�����Ϊ.dat����ѡ��
        FileBrowser.SetDefaultFilter(".dat");

        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".rar", ".exe");

        // ��ӿ�����ӣ�����ԭ����
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);

        // �����ļ�ѡ��Э��
        yield return Instance.ShowLoadDialogCoroutine(SelectedFilePaths);
    }

    IEnumerator ShowLoadDialogCoroutine(List<string> SelectedFilePaths)
    {
        // ��ʾ���ļ�ѡ��Ի���
        yield return FileBrowser.WaitForLoadDialog(
            FileBrowser.PickMode.Files,
            true, // �����ѡ
            null,
            null,
            "Select Files",
            "Select"
        );

        if (FileBrowser.Success)
        {
            // ���֮ǰ�����·��
            // SelectedFilePaths.Clear();

            // ������ѡ���·��
            SelectedFilePaths.AddRange(FileBrowser.Result);

            // ����ѡ�е��ļ����ɱ������Ƴ��ļ��������룩
            foreach (string path in SelectedFilePaths)
            {
                Debug.Log("Selected file: " + path);
            }
        }
        else
        {
            Debug.Log("File selection cancelled");
        }
    }
}