using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject startButton;

    public void TriggerDialogue()
    {
        if(startButton != null)
            startButton.SetActive(false);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
