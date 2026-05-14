using UnityEngine;

[CreateAssetMenu(fileName = "NewBadge", menuName = "Game/Badge")]
public class BadgeData : ScriptableObject
{
    public string id;          // benzersiz (örn: "chapter1_badge")
    public string title;       // rozet adý
    public string description; // açýklama
    public Sprite icon;        // görsel

    public int chapterIndex;   // hangi bölümden kazanýlýyor
}
