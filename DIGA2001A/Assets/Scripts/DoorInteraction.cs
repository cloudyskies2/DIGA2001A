using UnityEngine;
using TMPro;

/// Title: How to Make a Door System in Unity - Unity C# Tutorial
/// Date:   15 Jun 2023
/// Availabiliy: https://youtu.be/1M1pMkKt6uo?si=i7dQrqC_mx9b4YJ_



public class DoorInteraction : MonoBehaviour
{
    public Animator animator;
    public KeyCode interactKey = KeyCode.E;

    [Header("UI Elements")]
    public GameObject doorPromptUI;
    public TextMeshProUGUI doorPromptText;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip openDoorSound;
    public AudioClip closeDoorSound;

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
                {
                    animator.SetTrigger("OpenDoor");
                    if (audioSource != null && openDoorSound != null)
                        audioSource.PlayOneShot(openDoorSound);
                }
                else
                {
                    animator.SetTrigger("CloseDoor");
                    if (audioSource != null && closeDoorSound != null)
                        audioSource.PlayOneShot(closeDoorSound);
                }

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
