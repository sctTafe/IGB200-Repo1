using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogueOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<DialogueTrigger>().TriggerDialogue();
    }
}
