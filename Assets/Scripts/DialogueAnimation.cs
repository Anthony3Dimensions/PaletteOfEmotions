using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueAnimation : MonoBehaviour
{
    
    public Text dialogueText;
    public float delay = 0.5f;
    public string[] dialogueLines;
    public int dialogueIndex;
    private bool imTalking;
    public TMP_Text textBox;
    //private string[] dialogueLines;
    private int currentLine;
    private bool playerInRange = false;

    void Start()
    {
        Invoke("StartDialogue", 1.5f);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            StopAllCoroutines();
            dialogueText.text = dialogueLines[dialogueIndex];
            dialogueIndex++;
            ShowNextLine();
            
        }
    }

    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            ShowDialogue();
        }
    }


    void  StartDialogue()
    {
       StartCoroutine(ShowText(dialogueLines[dialogueIndex]));
    }

    void ShowNextLine()
    {
     
        if(dialogueIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowText(dialogueLines[dialogueIndex]));
        }
        
    }

    IEnumerator ShowText(string dialogueLine)
    {
        imTalking = true;
        foreach(char letter in dialogueLine)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(delay);
        }
        dialogueText.text = null; 
        dialogueIndex++;
        ShowNextLine();
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

      void ShowDialogue()
    {
        if(currentLine < dialogueLines.Length)
        {
            textBox.text = dialogueLines[currentLine];
            currentLine++;
        }
        else
        {
            textBox.text = "Fin del dialogo";
        }
    }

    public void CambiarNivel()
    {
        SceneManager.LoadScene(2);
    }
}
