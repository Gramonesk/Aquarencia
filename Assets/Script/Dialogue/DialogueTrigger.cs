using System;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Dialogue
{
    public string name;
    public Sprite character_sprite;
    [TextArea(3, 10)] public List<String> text;
}

public class DialogueTrigger : MonoBehaviour
{
    public List<Dialogue> dialogue;
    private int index = 0;
    private void Start()
    {
        DialogueManager.instance.OnNext = NextDialogue;
    }
    public void StartNewDialogue()
    {
        index = 0;
        TriggerDialogue();
    }
    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogue[index]);
    }
    public void NextDialogue()
    {
        if (index + 1 == dialogue.Count) return;
        index++;
        TriggerDialogue();
    }
}