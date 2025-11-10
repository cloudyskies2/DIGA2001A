
using UnityEngine;

/// Title: Game Over Screen Unity Tutorial
/// Date: 12 Sept 2022
/// Availabiliy: https://youtu.be/pKFtyaAPzYo?si=QL509S07fNfCCnTV




public class GoalTrigger : MonoBehaviour
{
    public TimerManager timerManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timerManager.ReachGoal();
        }
    }
}
