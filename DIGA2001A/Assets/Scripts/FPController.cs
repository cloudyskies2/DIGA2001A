using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;
public class FPController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    [Header("Jump Settings")]
    public float jumpHeight = 16f;
    //public bool doubleJump;
    public float jumpForce = 9f;
    //public float doubleJumpForce = 8f;
    //private Rigidbody rb;
    public float numOfJumps;
    public float maxNumOfJumps = 2f;

    [Header("Look Settings")]
    public Transform cameraTransform;
    public float lookSensitivity = 1f;
    public float verticalLookLimit = 90f;
    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private float verticalRotation = 0f;

    //[Header("Shooting")]
    //public GameObject bulletPrefab;
    //public Transform gunPoint;

    [Header("Crouch Settings")]
    public float crouchHeight = 1f;
    public float standHeight = 2f;
    public float crouchSpeed = 2.5f;
    private float originalMoveSpeed;

    [Header("Pickup Settings")]
    public float pickupRange = 3.5f;
    public Transform heldPoint;
    private PickUpObject heldObject;
    [SerializeField] private LayerMask pickUpLayer;

    [Header("Rotation Settings")]
    //Quaternion targetRotation;

    [Header("Throw Settings")]
    public float throwForce = 10f;
    public float throwUpwardBoost = 1f;

    [Header("Interact Settings")]
    public float interactRange = 3f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        originalMoveSpeed = moveSpeed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        HandleMovement();
        HandleLook();

        if(heldObject != null)
        {
            heldObject.MoveToHeldPoint(heldPoint.position);
        }
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && !IsGrounded()) return;
        if (!IsGrounded() && numOfJumps >= maxNumOfJumps) return;

        //if (context.performed && IsGrounded())
        //{
            if (numOfJumps == 0) StartCoroutine(routine:WaitForLanding());

            numOfJumps++;
            velocity.y = Mathf.Sqrt(jumpHeight * -5f * gravity);

            //HandleJump(jumpForce);
        //}

        /*else if(context.performed && !controller.isGrounded && doubleJump)
        {
            HandleJump(doubleJumpForce);
            doubleJump = false;
        }*/

        //if (!context.performed) return;
        //if (!IsGrounded() && numOfJumps >= maxNumOfJumps) return;
        //if(numOfJumps == 0) StartCoroutine(WaitForLanding());

        //numOfJumps++;
        //velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private bool IsGrounded() => controller.isGrounded;

    private IEnumerator WaitForLanding()
    {
        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(IsGrounded);

        numOfJumps = 0;
    }

    /*private bool GetIsGrounded()
    {
        bool hit = Physics.Raycast(transform.position, Vector3.down, jumpHeight);

        if (hit)
        {
            doubleJump = true;
        }

        return hit;
    }*/

    /*public void OnShoot(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if(bulletPrefab != null && gunPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(gunPoint.forward * 1000f); //Adjust force value as needed
                Destroy(bullet, 3); //Destroys bullet after 3 seconds
            }
        }
    }*/

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            controller.height = crouchHeight;
            moveSpeed = crouchSpeed;
        }
        else if (context.canceled)
        {
            controller.height = standHeight;
            moveSpeed = originalMoveSpeed;
        }
    }

   

    public void HandleMovement()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward *
        moveInput.y;
        controller.Move(move * moveSpeed * Time.deltaTime);
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    public void HandleLook()
    {
        float mouseX = lookInput.x * lookSensitivity;
        float mouseY = lookInput.y * lookSensitivity;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit,
        verticalLookLimit);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void HandleJump()
    {

        //rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    public void OnPickUp(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        if(heldObject == null)
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, pickupRange, pickUpLayer)) 
            {
                PickUpObject pickUp = hit.collider.GetComponent<PickUpObject>();

                if(pickUp != null)
                {
                    pickUp.PickUp(heldPoint);
                    heldObject = pickUp;
                }
            }
        }
        else
        {
            heldObject.Drop();
            heldObject = null;
        }
    }

    public void OnRotateObjectYAxis(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        heldObject.transform.Rotate(0, 45, 0);
        //targetRotation = Quaternion.Euler(heldObject.transform.eulerAngles.x, heldObject.transform.eulerAngles.y + 90, heldObject.transform.eulerAngles.z);
    }

    public void OnRotateObjectXAxis(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        heldObject.transform.Rotate(45, 0, 0);
        //targetRotation = Quaternion.Euler(heldObject.transform.eulerAngles.x, heldObject.transform.eulerAngles.y + 90, heldObject.transform.eulerAngles.z);
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if(heldObject ==null) return;

        Vector3 dir = cameraTransform.forward;
        Vector3 impulse = dir * throwForce + Vector3.up * throwUpwardBoost;

        heldObject.Throw(impulse);
        heldObject = null;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if(Physics.Raycast(ray, out RaycastHit hit, interactRange))
        {
            //Only allow objects tagged as "Switchable".
            if(hit.collider.CompareTag("Switchable"))
            {
                var switcher = hit.collider.GetComponent<MaterialSwitcher>();
                if(switcher != null)
                {
                    switcher.ToggleMaterial();
                }
            }
        }
    }
}