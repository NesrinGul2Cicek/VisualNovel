using UnityEngine;

public class StoryController : MonoBehaviour
{
    public EndingData currentEnding;

    public void SetEnding(string speaker, string text, int nodeID)
    {
        currentEnding = new EndingData
        {
            speakerName = speaker,
            dialogueText = text,
            nodeID = nodeID
        };
    }
}