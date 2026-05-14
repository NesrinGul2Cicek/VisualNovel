using System;
using UnityEngine;

[Serializable]
public class StoryNode
{
    public int nodeID;

    [Header("Dialogue")]
    public string speakerName;

    [TextArea(2, 5)]
    public string dialogueText;

    [Header("Flow")]
    public bool isChoiceNode;
    public int nextNodeID = -1;
    public bool isEndingNode;

    [Header("Choices")]
    public ChoiceData[] choices;
}