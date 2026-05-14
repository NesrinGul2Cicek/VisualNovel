using System;
using UnityEngine;

[Serializable]
public class ChoiceData
{
    [TextArea(1, 2)]
    public string choiceText;

    public int nextNodeID = -1;

    public string badgeName;
}
