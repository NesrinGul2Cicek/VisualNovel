using UnityEngine;

public class NovelLoader : MonoBehaviour
{
    public ChapterDatabase database;

    private void Start()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager bulunamadý!");
            return;
        }

        int id = GameManager.Instance.SelectedChapterID;

        if (database == null)
        {
            Debug.LogError("Database atanmamýþ!");
            return;
        }

        ChapterData data = database.GetChapterByID(id);

        if (data == null)
        {
            Debug.LogError("Chapter bulunamadý! ID: " + id);
            return;
        }

        PlayChapter(data);
    }

    void PlayChapter(ChapterData data)
    {
        Debug.Log("Oynatýlan chapter: " + data.chapterTitle);

        // burada dialog sistemi baþlayacak
    }
}