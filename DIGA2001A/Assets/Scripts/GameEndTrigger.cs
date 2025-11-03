using UnityEngine;

public class GameEndTrigger : MonoBehaviour
{
    public GameObject endMenuUI; 
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            EndGame();
        }
    }

    void EndGame()
    {
        endMenuUI.SetActive(true);

        
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var move = player.GetComponent<FPController>();
            if (move != null) move.enabled = false;
        }

        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

