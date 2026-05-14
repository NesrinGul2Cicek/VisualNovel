using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterSelectButton : MonoBehaviour
{
    public int chapterID = 1;
    public string novelSceneName = "NovelScene";

    public void SelectChapter()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance bulunamadý.");
            return;
        }

        GameManager.Instance.SetSelectedChapter(chapterID);
        

        SceneManager.LoadScene(novelSceneName);
    }
}