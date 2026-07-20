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

    // NOT: Eskiden burada Inspector'dan sürüklenen StoryController ve
    // SaveData referanslarý vardý. Bu nesneler DontDestroyOnLoad ile
    // runtime'da oluþtuðu için ResultScene'in edit modunda sahnede fiziksel
    // olarak bulunmuyorlar (sürükleyecek bir þey yok / yanlýþ nesneye
    // baðlanýyordu). Artýk ikisi de kalýcý singleton üzerinden
    // (StoryController.Instance, SaveData.Instance) eriþiliyor.

    private void Start()
    {
        LoadResultData();
    }

    private void LoadResultData()
    {
        if (StoryController.Instance == null)
        {
            Debug.LogError("StoryController.Instance bulunamadý! Sahnede kalýcý bir StoryController nesnesi olduðundan emin olun.");
            return;
        }

        if (SaveData.Instance == null)
        {
            Debug.LogError("SaveData.Instance bulunamadý! Sahnede kalýcý bir SaveData nesnesi olduðundan emin olun.");
            return;
        }

        // Ending data
        if (endingTitleText != null)
            endingTitleText.text = StoryController.Instance.currentEnding?.speakerName ?? "";

        if (endingDescriptionText != null)
            endingDescriptionText.text = StoryController.Instance.currentEnding?.dialogueText ?? "";

        // Badge data
        if (badgeText != null)
        {
            int chapterID = GameManager.Instance.SelectedChapterID;
            if (SaveData.Instance.HasBadge(chapterID))
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