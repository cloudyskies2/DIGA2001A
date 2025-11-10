using UnityEngine;
using TMPro;

/// Title: How to make a Dialogue System in Unity
/// Date: 23 Jul 2017
/// Availabiliy: https://youtu.be/_nRzoTzeyxU?si=Cn54MrHWCMdY0Mw_



public class CatNPCDialogue : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    [Header("Player References")]
    public FPController playerMovement;
    

    private string[] lines = new string[]
    {
        "Hey! press R to continue" +
        " I'm Munchie the cat. I hope youll enjoy our game",
        "Use W, A, S and D to move all direction.",
        "Press SPACE to jump, and yes... you can double jump.",
        "Press ESC anytime to pause the game.",
        "Okay twin, off you go."
    };

    private int index = 0;
    private bool isTalking = false;

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTalking)
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        isTalking = true;

        
        playerMovement.enabled = false;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        dialoguePanel.SetActive(true);
        index = 0;
        dialogueText.text = lines[index];
    }

    void Update()
    {
        if (isTalking && Input.GetKeyDown(KeyCode.R))
        {
            NextLine();
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = lines[index];
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);

        
        playerMovement.enabled = true;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isTalking = false;
    }
}

