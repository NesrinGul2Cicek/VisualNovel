using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ChapterTrigger : MonoBehaviour
{
    [Header("Chapter Info")]
    public string chapterID = "Chapter_01";
    public string chapterTitle = "Bölüm 1";
    public string sceneToLoad = "NovelScene";

    [Header("Interaction")]
    public KeyCode interactKey = KeyCode.E;

    [Header("UI")]
    public GameObject promptPanel;
    public TMP_Text promptText;

    private bool playerInRange = false;

    private void Start()
    {
        if (promptPanel != null)
            promptPanel.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            EnterChapter();
        }
    }

    private void EnterChapter()
    {
        Debug.Log("Bölüm seçildi: " + chapterID);
        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (promptPanel != null)
                promptPanel.SetActive(true);

            if (promptText != null)
                promptText.text = chapterTitle + "\nE ile baþlat";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (promptPanel != null)
                promptPanel.SetActive(false);
        }
    }
}