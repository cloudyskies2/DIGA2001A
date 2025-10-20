using UnityEngine;
using TMPro;
using UnityEngine.ProBuilder.MeshOperations;

public class DoorInteraction : MonoBehaviour 
{

    public Animator animator;
    public KeyCode interactKey = KeyCode.E;
    public TextMeshProUGUI InteractionPrompt;

    private bool isPlayerInRange = false;
    private bool isDoorOpen = false;

     void Start()
    {
        if (InteractionPrompt != null)
        {
            InteractionPrompt.gameObject.SetActive(false);
        }
    }

     void Update()
    {
        if (isPlayerInRange)
        {

            if (InteractionPrompt != null)
            {
                InteractionPrompt.text = isDoorOpen ? "Press E to Close Door" : "Press E to Open Door";
                InteractionPrompt.gameObject.SetActive(true);


            }

            if (Input.GetKeyDown(interactKey))
            {
                if (!isDoorOpen)
                {

                    animator.SetTrigger("OpenDoor");


                }
                else
                {
                    animator.SetTrigger("CloseDoor");



                }

                isDoorOpen = !isDoorOpen;
            }
        }else
        {

            if (InteractionPrompt != null)
            {
                InteractionPrompt.gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            
        }
    }
}
