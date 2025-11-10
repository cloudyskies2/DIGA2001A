using UnityEngine;
using TMPro;

/// Title: How to Make a Door System in Unity - Unity C# Tutorial
/// Date:   15 Jun 2023
/// Availabiliy: https://youtu.be/1M1pMkKt6uo?si=i7dQrqC_mx9b4YJ_

public class MazeGate : MonoBehaviour
{
    public Animator animator;                       
    public KeyCode interactKey = KeyCode.E;        
    public TextMeshProUGUI interactionPrompt;      

    private bool isPlayerInRange = false;
    private bool isGateOpened = false;             

    void Start()
    {
        if (interactionPrompt != null)
            interactionPrompt.gameObject.SetActive(false); 
    }

    void Update()
    {
        if (isPlayerInRange && !isGateOpened)
        {
            
            if (interactionPrompt != null)
            {
                interactionPrompt.text = "Press E to Open Gate";
                interactionPrompt.gameObject.SetActive(true);
            }

            
            if (Input.GetKeyDown(interactKey))
            {
                animator.SetTrigger("OpenWall");   
                isGateOpened = true;

                
                if (interactionPrompt != null)
                    interactionPrompt.gameObject.SetActive(false);
            }
        }
        else
        {
            
            if (interactionPrompt != null)
                interactionPrompt.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerInRange = false;
    }
}