using UnityEngine;
using UnityEngine.InputSystem;

public class LetterReader : MonoBehaviour
{
    public KeyCode readKey = KeyCode.R;

    [Header("UI Elements")]
    public GameObject readPromptUI;   
    public GameObject letterPanelUI;
    [Header("Player References")]
    public FirstPersonController playerController;   
    public PlayerInput playerInput;                  

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

        
        playerController.enabled = false;
        playerInput.enabled = false;

        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void CloseLetter()
    {
        isReading = false;
        letterPanelUI.SetActive(false);

        
        playerController.enabled = true;
        playerInput.enabled = true;

        
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
