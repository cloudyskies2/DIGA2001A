using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private Rigidbody rb;
    private float lerpSpeed = 10f;
    private Transform heldPoint;
    private Transform from;
    private Transform to;
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
        transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, Time.deltaTime * lerpSpeed);
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
        if(heldPoint != null)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, heldPoint.position, Time.deltaTime * lerpSpeed);
        }
    }
}
