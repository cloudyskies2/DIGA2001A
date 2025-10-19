using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    public GameObject puzzle;
    public float interactionDistance = 2;
    public LayerMask layers;

     void Update()
     {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, layers))
        { 
            if (hit.collider.gameObject.GetComponent<PuzzleManager>())
            {
                puzzle.SetActive(true);
                if(Input.GetKeyDown(KeyCode.Q))
                {
                    hit.collider.gameObject.GetComponent<PuzzleManager>();
                }
            }
            else
            {
                puzzle.SetActive(false);
            }
        }
        else
        {
           puzzle.SetActive(false);
        }
    }
}
 