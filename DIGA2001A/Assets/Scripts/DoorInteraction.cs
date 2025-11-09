
using UnityEngine;
using TMPro;

public class DoorInteraction : MonoBehaviour
{
    public Animator animator;
    public KeyCode interactKey = KeyCode.E;

    [Header("UI Elements")]
    public GameObject doorPromptUI; 
    public TextMeshProUGUI doorPromptText; 

    private bool isPlayerInRange = false;
    private bool isDoorOpen = false;

    void Start()
    {
        if (doorPromptUI != null)
        {
            doorPromptUI.SetActive(false); 
        }
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            
            if (doorPromptText != null)
            {
                doorPromptText.text = isDoorOpen ? "Press E to Close Door" : "Press E to Open Door";
            }

            if (doorPromptUI != null && !doorPromptUI.activeSelf)
            {
                doorPromptUI.SetActive(true);
            }

            
            if (Input.GetKeyDown(interactKey))
            {
                if (!isDoorOpen)
                    animator.SetTrigger("OpenDoor");
                else
                    animator.SetTrigger("CloseDoor");

                isDoorOpen = !isDoorOpen;
            }
        }
        else
        {
            if (doorPromptUI != null && doorPromptUI.activeSelf)
            {
                doorPromptUI.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerInRange = false;
    }
}
