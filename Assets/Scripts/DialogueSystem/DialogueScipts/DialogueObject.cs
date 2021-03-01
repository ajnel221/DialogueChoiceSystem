using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue_Object", menuName = "NPC Dialogue Object", order = 0)]
public class DialogueObject : ScriptableObject
{
    [Header("Dialogue")]
    public List<DialogueSegment> dialogueSegments = new List<DialogueSegment>();

    [Header("Follow on dialogue - Optional")]
    public DialogueObject endDialogue;
    
}






[System.Serializable]
public struct DialogueSegment
{
    public string dialogueText;
    public float dialogueDisplayTime;
    public List<DialogueChoice> dialogueChoices;
}

[System.Serializable]
public struct DialogueChoice
{
    public string dialogueChoice;
    public DialogueObject followOnDialogue;
    public GameObject spawnPrefabTest;
}