using UnityEngine;

[CreateAssetMenu(fileName = "ChapterDatabase", menuName = "VisualNovel/Chapter Database")]
public class ChapterDatabase : ScriptableObject
{
    public ChapterData[] chapters;

    public ChapterData GetChapterByID(int id)
    {
        if (chapters == null) return null;

        foreach (var chapter in chapters)
        {
            if (chapter != null && chapter.chapterID == id)
                return chapter;
        }

        return null;
    }
}