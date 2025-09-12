using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    public GameObject crosshair;
    public float interactionDiastance;
    public LayerMask layers;

     void Update()
     {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, interactionDiastance, layers))
        {
            if (hit.collider.gameObject.GetComponent<Door>())
            {
                crosshair.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponent<Door>().openClose();
                }
            }
            else
            {
                crosshair.SetActive(false);
            }
        }
        else
        {
            crosshair.SetActive(false);
        }
    }
}
 