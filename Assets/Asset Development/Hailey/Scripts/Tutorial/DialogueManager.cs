using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public Animator animator;

    private Queue<string> sentences; //FIFO collection

    public bool isCutscene;
    public string scene;

    public GameObject dialogueBox;

    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
        dialogueBox.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //animator.SetBool("IsOpen", true);
        dialogueBox.SetActive(true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence;
        
        sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        //dialogueText.text = sentence;
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        if(isCutscene)
            SceneManager.LoadScene(scene);
        else
        {
            //animator.SetBool("IsOpen", false);
            dialogueBox.SetActive(false);
        }      
       
    }

}
