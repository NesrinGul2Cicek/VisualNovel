using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NovelUIManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public TMP_Text continueHint;

    [Header("Choice UI")]
    public GameObject choicePanel;
    public Button choiceButton1;
    public Button choiceButton2;
    public TMP_Text choiceButton1Text;
    public TMP_Text choiceButton2Text;

    [Header("Story Data")]
    public ChapterDatabase chapterDatabase;

    [Header("Scene Settings")]
    public string resultSceneName = "ResultScene";

    private ChapterData currentChapter;
    private StoryNode currentNode;
    private bool waitingForChoice = false;

    // NOT: storyController ve saveData artık sahneler arası kalıcı
    // singleton'lar üzerinden erişiliyor (StoryController.Instance,
    // SaveData.Instance). Inspector'dan sürükleme YAPMANIZA gerek yok,
    // eski dragged referanslar sahne değişince kayboluyordu.

    private void Start()
    {
        if (choicePanel != null)
            choicePanel.SetActive(false);

        SetupChoiceButtons();
        LoadSelectedChapter();
    }

    // ✔ YENİ INPUT SYSTEM
    public void OnContinue(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (waitingForChoice) return;

        ContinueStory();
    }

    private void LoadSelectedChapter()
    {
        if (chapterDatabase == null)
        {
            Debug.LogError("ChapterDatabase atanmadı.");
            return;
        }

        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance bulunamadı.");
            return;
        }

        int selectedChapterID = GameManager.Instance.SelectedChapterID;
        currentChapter = chapterDatabase.GetChapterByID(selectedChapterID);

        if (currentChapter == null)
        {
            Debug.LogError("Seçilen chapter bulunamadı. ID: " + selectedChapterID);
            return;
        }

        if (currentChapter.storyNodes == null || currentChapter.storyNodes.Length == 0)
        {
            Debug.LogError("Chapter içinde story node yok. Chapter ID: " + currentChapter.chapterID);
            return;
        }

        LoadNode(currentChapter.startNodeID);
    }

    private void LoadNode(int nodeID)
    {
        StoryNode node = FindNodeByID(nodeID);

        if (node == null)
        {
            Debug.LogError("Node bulunamadı: " + nodeID);
            return;
        }

        currentNode = node;
        ShowNode(currentNode);
    }

    private StoryNode FindNodeByID(int nodeID)
    {
        if (currentChapter == null || currentChapter.storyNodes == null)
            return null;

        for (int i = 0; i < currentChapter.storyNodes.Length; i++)
        {
            if (currentChapter.storyNodes[i].nodeID == nodeID)
                return currentChapter.storyNodes[i];
        }

        return null;
    }

    private void ShowNode(StoryNode node)
    {
        if (nameText != null)
            nameText.text = node.speakerName;

        if (dialogueText != null)
            dialogueText.text = node.dialogueText;

        if (choicePanel != null)
            choicePanel.SetActive(false);

        waitingForChoice = false;

        if (node.isChoiceNode)
        {
            ShowChoices(node);
            return;
        }

        if (continueHint != null)
            continueHint.gameObject.SetActive(!node.isEndingNode);
    }

    public void ContinueStory()
    {
        Debug.Log("ContinueStory ÇALIŞTI");

        if (currentNode == null)
        {
            Debug.LogError("currentNode NULL");
            return;
        }

        if (currentNode.isEndingNode)
        {
            if (StoryController.Instance != null)
            {
                StoryController.Instance.SetEnding(
                    currentNode.speakerName,
                    currentNode.dialogueText,
                    currentNode.nodeID
                );
            }
            else
            {
                Debug.LogError("StoryController.Instance bulunamadı! Sahnede kalıcı bir StoryController nesnesi olduğundan emin olun.");
            }

            // Bölümü "tamamlandı" olarak işaretle — rozete dokunmuyor,
            // rozet zaten SelectChoice() içinde koşullu kazandırılıyor.
            if (SaveData.Instance != null && GameManager.Instance != null)
            {
                int chapterID = GameManager.Instance.SelectedChapterID;
                SaveData.Instance.MarkChapterCompleted(chapterID);
            }
            else
            {
                Debug.LogError("SaveData.Instance veya GameManager.Instance bulunamadı!");
            }

            SceneManager.LoadScene(resultSceneName);
            return;
        }

        if (currentNode.isChoiceNode)
            return;

        if (currentNode.nextNodeID == -1)
        {
            Debug.LogWarning("Sonraki node tanımlanmamış. Node ID: " + currentNode.nodeID);
            return;
        }

        LoadNode(currentNode.nextNodeID);
    }

    private void ShowChoices(StoryNode node)
    {
        if (node.choices == null || node.choices.Length < 2)
        {
            Debug.LogError("Choice node için en az 2 seçim gerekli. Node ID: " + node.nodeID);
            return;
        }

        waitingForChoice = true;

        if (continueHint != null)
            continueHint.gameObject.SetActive(false);

        if (choicePanel != null)
            choicePanel.SetActive(true);

        if (choiceButton1Text != null)
            choiceButton1Text.text = node.choices[0].choiceText;

        if (choiceButton2Text != null)
            choiceButton2Text.text = node.choices[1].choiceText;
    }

    private void SetupChoiceButtons()
    {
        if (choiceButton1 != null)
        {
            choiceButton1.onClick.RemoveAllListeners();
            choiceButton1.onClick.AddListener(() => SelectChoice(0));
        }

        if (choiceButton2 != null)
        {
            choiceButton2.onClick.RemoveAllListeners();
            choiceButton2.onClick.AddListener(() => SelectChoice(1));
        }
    }

    private void SelectChoice(int choiceIndex)
    {
        if (currentNode == null || currentNode.choices == null)
            return;

        if (choiceIndex < 0 || choiceIndex >= currentNode.choices.Length)
            return;

        ChoiceData selectedChoice = currentNode.choices[choiceIndex];

        if (SaveData.Instance != null && !string.IsNullOrEmpty(selectedChoice.badgeName))
        {
            SaveData.Instance.EarnBadge(GameManager.Instance.SelectedChapterID);
        }

        if (choicePanel != null)
            choicePanel.SetActive(false);

        waitingForChoice = false;

        if (selectedChoice.nextNodeID == -1)
        {
            Debug.LogWarning("Seçimde nextNodeID tanımlanmamış.");
            return;
        }

        LoadNode(selectedChoice.nextNodeID);
    }

    
}