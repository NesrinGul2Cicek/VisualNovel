using UnityEngine;
using UnityEngine.SceneManagement;

public class BootLoader : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string mainSceneName = "MainScene";

    [Header("Managers")]
    [SerializeField] private GameObject gameManagerPrefab;

    private void Awake()
    {
        EnsureGameManagerExists();
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
}
