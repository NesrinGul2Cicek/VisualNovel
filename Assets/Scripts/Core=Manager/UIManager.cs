using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI")]
    public GameObject panel;

    private ChapterData targetChapter;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Taþ çaðýrýr
    public void OpenPanel(ChapterData chapter)
    {
        if (!panel) return;

        panel.SetActive(true);
        targetChapter = chapter;
    }

    public void ClosePanel()
    {
        if (!panel) return;

        panel.SetActive(false);
        targetChapter = null;
    }

    // Player (Interact) çaðýrýr
    public void TryInteract()
    {
        if (!panel) return;
        if (!panel.activeSelf) return;
        if (targetChapter == null) return;

        GameData.currentChapter = targetChapter;
        SceneManager.LoadScene("NovelScene");
    }
}