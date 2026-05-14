using UnityEngine;

public class SaveData : MonoBehaviour
{
    private const string SAVE_KEY = "PROGRESS";

    public ChapterProgress[] chapters;

    private void Awake()
    {
        Load();
    }

    // Bölüm tamamlanınca çağır
    public void CompleteChapter(int index)
    {
        if (chapters[index].completed)
            return; // zaten yapılmışsa tekrar işleme

        chapters[index].completed = true;
        chapters[index].badgeEarned = true;

        Save();
    }

    // Sadece oynandı olarak işaretle (rozet yok)
    public void MarkPlayed(int index)
    {
        if (!chapters[index].completed)
        {
            // sadece oynandı ama bitmedi → kayıt alma
            return;
        }
    }

    public bool HasBadge(int index)
    {
        return chapters[index].badgeEarned;
    }

    // --- SAVE / LOAD ---

    public void Save()
    {
        string json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    public void EarnBadge(int chapterID)
    {
        if (!chapters[chapterID].badgeEarned)
        {
            chapters[chapterID].badgeEarned = true;
            Save();
        }
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string json = PlayerPrefs.GetString(SAVE_KEY);
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }
}