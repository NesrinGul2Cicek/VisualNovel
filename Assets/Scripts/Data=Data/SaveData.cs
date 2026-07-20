using System;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    private const string SAVE_KEY = "PROGRESS";

    public ChapterProgress[] chapters = new ChapterProgress[0];
    public static SaveData Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // chapters dizisini, verilen chapterID'ye erişebilecek kadar büyütür.
    // Böylece kaç bölüm olduğunu elle ayarlamana gerek kalmaz ve
    // IndexOutOfRangeException bir daha oluşmaz.
    private void EnsureCapacity(int chapterID)
    {
        if (chapterID < 0)
        {
            Debug.LogError("Geçersiz chapterID: " + chapterID);
            return;
        }

        if (chapters == null)
        {
            chapters = new ChapterProgress[chapterID + 1];
        }
        else if (chapterID >= chapters.Length)
        {
            Array.Resize(ref chapters, chapterID + 1);
        }

        if (chapters[chapterID] == null)
        {
            chapters[chapterID] = new ChapterProgress();
        }
    }

    // Bölüm tamamlanınca çağır
    public void CompleteChapter(int chapterID)
    {
        EnsureCapacity(chapterID);

        if (chapters[chapterID].completed)
            return; // zaten yapılmışsa tekrar işleme

        chapters[chapterID].completed = true;
        chapters[chapterID].badgeEarned = true;

        Save();
    }

    // Sadece oynandı olarak işaretle (rozet yok)
    public void MarkPlayed(int chapterID)
    {
        EnsureCapacity(chapterID);

        if (!chapters[chapterID].completed)
        {
            // sadece oynandı ama bitmedi → kayıt alma
            return;
        }
    }

    public bool HasBadge(int chapterID)
    {
        EnsureCapacity(chapterID);
        return chapters[chapterID].badgeEarned;
    }

    public void EarnBadge(int chapterID)
    {
        EnsureCapacity(chapterID);

        if (!chapters[chapterID].badgeEarned)
        {
            chapters[chapterID].badgeEarned = true;
            Save();
        }
    }

    // --- SAVE / LOAD ---

    public void Save()
    {
        string json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string json = PlayerPrefs.GetString(SAVE_KEY);
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }

    public void MarkChapterCompleted(int chapterID)
    {
        EnsureCapacity(chapterID);
        if (chapters[chapterID].completed)
            return; // zaten tamamlanmışsa tekrar kaydetme
        chapters[chapterID].completed = true;
        Save();
    }
}