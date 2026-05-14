using UnityEngine;

[CreateAssetMenu(fileName = "NewChapterData", menuName = "VisualNovel/Chapter Data")]
public class ChapterData : ScriptableObject
{
    public int chapterID;
    public string chapterTitle;

    [TextArea(2, 5)]
    public string chapterDescription;

    public int startNodeID = 0;
    public StoryNode[] storyNodes;
}