using TMPro;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    [SerializeField] private GameObject confetti;
    [SerializeField] private GameObject confetti2;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            confetti.gameObject.SetActive(true);
            confetti2.gameObject.SetActive(true);
        }
    }
}
