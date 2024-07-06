using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Text Settings")]
    public float speed;

    [Header("UI Settings")]
    public TMP_Text nameText;
    public Image Character;
    public TMP_Text dialogueText;
    public static DialogueManager instance;

    public Action OnNext;
    public Queue<string> queue = new Queue<string>();
    private void Awake()
    {
        instance = this;
    }
    public void StartDialogue(Dialogue dialogue)
    {
        queue = new Queue<string>(dialogue.text);
        nameText.text = dialogue.name.ToString();
        Character.sprite = dialogue.character_sprite;
        NextDialogue();
    }
    public void NextDialogue()
    {
        if (queue.Count == 0)
        {
            EndDialogue();
            return;
        }
        StopAllCoroutines();
        StartCoroutine(Write(queue.Dequeue()));
    }
    public void EndDialogue()
    {
        //hilangin nama atau digelapin dll
        OnNext?.Invoke();
    }
    IEnumerator Write(string text)
    {
        dialogueText.text = "";
        foreach(char c in text.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(speed);
        }
    }
}