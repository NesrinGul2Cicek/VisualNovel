using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultUIManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text endingTitleText;
    public TMP_Text endingDescriptionText;
    public TMP_Text badgeText;

    [Header("Scene Settings")]
    public string mainSceneName = "MainScene";


    public StoryController storyController;
    public SaveData saveData;
    private void Start()
    {
        LoadResultData();
    }


    private void LoadResultData()
    {
        if (storyController == null || saveData == null)
        {
            Debug.LogError("Referanslar eksik!");
            return;
        }

        // Ending data
        if (endingTitleText != null)
            endingTitleText.text = storyController.currentEnding?.speakerName ?? "";

        if (endingDescriptionText != null)
            endingDescriptionText.text = storyController.currentEnding?.dialogueText ?? "";

        // Badge data
        if (badgeText != null)
        {
            int chapterID = GameManager.Instance.SelectedChapterID;

            if (saveData.HasBadge(chapterID))
                badgeText.text = "Kazanýlan Rozet";
            else
                badgeText.text = "Rozet kazanýlmadý";
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainSceneName);
    }
}