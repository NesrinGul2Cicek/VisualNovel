using UnityEngine;

[CreateAssetMenu(fileName = "BadgeDatabase", menuName = "Game/Badge Database")]
public class BadgeDatabase : ScriptableObject
{
    public BadgeData[] badges;

    public BadgeData GetBadgeByChapter(int chapterIndex)
    {
        foreach (var badge in badges)
        {
            if (badge.chapterIndex == chapterIndex)
                return badge;
        }
        return null;
    }

    public BadgeData GetBadgeByID(string id)
    {
        foreach (var badge in badges)
        {
            if (badge.id == id)
                return badge;
        }
        return null;
    }
}
