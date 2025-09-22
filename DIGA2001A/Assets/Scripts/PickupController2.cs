using System.Numerics;
using UnityEngine;

//Title:PICKUP & DROP Physics Objects in Unity (Like PORTAL)
//Author: Speed Tutor
//Date: 26 march 2022
//Code version;n/a
//Availability;https://youtu.be/6bFCQqabfzo?si=6U0QiA-0ua9y_g-0


public class PlayerupController2 : MonoBehaviour
{
    [SerializeField] Transform holdArea;
    private GameObject heldObj;
    private Rigidbody heldObjRB;

    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickupForce = 150.0f;

    [System.Obsolete]
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(UnityEngine.Vector3.forward), out hit, pickupRange))
                {
                    PickupObject(hit.transform.gameObject);
                }
            }
            else
            {
                 DropObject();
            }
        }
        if (heldObj != null)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        if(UnityEngine.Vector3.Distance(heldObj.transform.position, holdArea.position) < 0.1f)
        {
            UnityEngine.Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }

    [System.Obsolete]
    void PickupObject(GameObject pickObj)
    {
       if (pickObj.GetComponent<Rigidbody>())
        {
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            heldObjRB.useGravity = false;
            heldObjRB.drag = 10;
            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

            heldObjRB.transform.parent = holdArea;
            heldObj = pickObj;
        }
    }

    [System.Obsolete]
    void DropObject()
    {
        heldObjRB.useGravity = true;
        heldObjRB.drag = 1;
        heldObjRB.constraints = RigidbodyConstraints.None;

        heldObj.transform.parent = null;
        heldObj = null;
    }
}
