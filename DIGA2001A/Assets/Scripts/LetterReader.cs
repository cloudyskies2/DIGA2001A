using UnityEngine;
using UnityEngine.InputSystem;

public class LetterReader : MonoBehaviour
{
    public KeyCode readKey = KeyCode.R;

    [Header("UI Elements")]
    public GameObject readPromptUI;   // "Press R to Read" text
    public GameObject letterPanelUI;  // Panel with the letter image

    [Header("Player References")]
    public FirstPersonController playerController;   // Drag your FirstPersonController component here
    public PlayerInput playerInput;                  // Drag your PlayerInput component here

    private bool isPlayerInRange = false;
    private bool isReading = false;

    void Start()
    {
        readPromptUI.SetActive(false);
        letterPanelUI.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInRange && !isReading)
        {
            readPromptUI.SetActive(true);

            if (Input.GetKeyDown(readKey))
            {
                OpenLetter();
            }
        }
        else if (isReading)
        {
            if (Input.GetKeyDown(readKey))
            {
                CloseLetter();
            }
        }
    }

    void OpenLetter()
    {
        isReading = true;
        readPromptUI.SetActive(false);
        letterPanelUI.SetActive(true);

        // Disable movement + camera look
        playerController.enabled = false;
        playerInput.enabled = false;

        // Unlock cursor so user can read
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void CloseLetter()
    {
        isReading = false;
        letterPanelUI.SetActive(false);

        // Re-enable movement + camera look
        playerController.enabled = true;
        playerInput.enabled = true;

        // Lock cursor back for FPS control
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            readPromptUI.SetActive(false);

            if (isReading)
                CloseLetter();
        }
    }
}
