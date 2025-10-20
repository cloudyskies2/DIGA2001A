using System.Net;
using System.Threading;
using UnityEngine;


public class PickUpObject : MonoBehaviour
{
    private Rigidbody rb;
    private float lerpSpeed = 10f;
    private Transform heldPoint;
    private Transform From;
    private Transform To;
    Quaternion targetRotation;
    private GameObject heldObject;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void PickUp(Transform heldPoint)
    {
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.SetParent(heldPoint);
        transform.localPosition = Vector3.zero;
    }

    public void RotateObject()
    {
        transform.rotation = Quaternion.Lerp(From.rotation, To.rotation, Time.deltaTime * lerpSpeed);

        //heldObject.transform.rotation = targetRotation;
    }


    public void Drop()
    {
        rb.useGravity = true;
        transform.SetParent(null);
    }

    public void Throw(Vector3 impulse)
    {
        transform.SetParent(null);
        rb.useGravity = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(impulse, ForceMode.Impulse);
    }

    public void MoveToHeldPoint(Vector3 targetPosition)
    {
        rb.MovePosition(targetPosition);
    }

    private void FixedUpdate()
    {
        if (heldPoint != null)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, heldPoint.position, Time.deltaTime * lerpSpeed);
            //RotateObject();
        }
    }
}
