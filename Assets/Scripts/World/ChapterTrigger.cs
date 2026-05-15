using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class ChapterTrigger : MonoBehaviour
{
    [Header("Chapter Info")]
    public string chapterID = "Chapter_01";
    public string chapterTitle = "Bölüm 1";
    public string sceneToLoad = "NovelScene";

    [Header("Interaction")]
    public InputActionReference interactAction;

    [Header("UI")]
    public GameObject promptPanel;
    public TMP_Text promptText;

    private bool playerInRange = false;

    private void OnEnable()
    {
        if (interactAction != null)
            interactAction.action.Enable();
    }

    private void OnDisable()
    {
        if (interactAction != null)
            interactAction.action.Disable();
    }

    private void Start()
    {
        if (promptPanel != null)
            promptPanel.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && interactAction != null && interactAction.action.WasPressedThisFrame())
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
                promptText.text = chapterTitle + "\nBas";
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