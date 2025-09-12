using UnityEngine;

public class Door : MonoBehaviour
{
    bool toggle;
    public Animator anim;
    public void openClose()
    {
        toggle = !toggle;
        if(toggle == false)
        {
            anim.ResetTrigger("open");
            anim.SetTrigger("close");
        }
        if(toggle == true)
        {
            anim.ResetTrigger("close");
            anim.SetTrigger("open");
        }
    }
        
}
