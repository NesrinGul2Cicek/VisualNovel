using UnityEngine;

public class StoryController : MonoBehaviour
{
    public static StoryController Instance;

    public EndingData currentEnding;

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

    public void SetEnding(string speaker, string text, int nodeID)
    {
        currentEnding = new EndingData
        {
            speakerName = speaker,
            dialogueText = text,
            nodeID = nodeID
        };

        Debug.Log("Ending set: " + speaker + " / nodeID: " + nodeID);
    }
}