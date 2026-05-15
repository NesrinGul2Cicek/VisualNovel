using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int SelectedChapterID = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSelectedChapter(int id)
    {
        SelectedChapterID = id;

        Debug.Log("Selected Chapter: " + id);
    }
}