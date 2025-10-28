
using UnityEngine;

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
