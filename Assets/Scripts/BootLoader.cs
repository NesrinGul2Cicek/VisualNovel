using UnityEngine;
using UnityEngine.SceneManagement;

public class BootLoader : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string mainSceneName = "MainScene";

    [Header("Managers")]
    [SerializeField] private GameObject gameManagerPrefab;
    [SerializeField] private GameObject storyControllerPrefab;
    [SerializeField] private GameObject saveDataPrefab;

    private void Awake()
    {
        EnsureGameManagerExists();
        EnsureStoryControllerExists();
        EnsureSaveDataExists();
    }

    private void Start()
    {
        SceneManager.LoadScene(mainSceneName);
    }

    private void EnsureGameManagerExists()
    {
        if (GameManager.Instance != null)
            return;

        if (gameManagerPrefab == null)
        {
            Debug.LogError("BootLoader: GameManager prefab atanmadý.");
            return;
        }

        Instantiate(gameManagerPrefab);
    }

    private void EnsureStoryControllerExists()
    {
        if (StoryController.Instance != null)
            return;

        if (storyControllerPrefab == null)
        {
            Debug.LogError("BootLoader: StoryController prefab atanmadý.");
            return;
        }

        Instantiate(storyControllerPrefab);
    }

    private void EnsureSaveDataExists()
    {
        if (SaveData.Instance != null)
            return;

        if (saveDataPrefab == null)
        {
            Debug.LogError("BootLoader: SaveData prefab atanmadý.");
            return;
        }

        Instantiate(saveDataPrefab);
    }
}