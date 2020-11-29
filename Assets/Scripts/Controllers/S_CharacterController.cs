using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CharacterController : MonoBehaviour
{

    [SerializeField]
    private float m_PlayerWalkSpeed = 5f;
    [SerializeField]
    private float m_PlayerSprintSpeed = 10f;
    [SerializeField]
    private float m_PlayerRotateSpeed = 4f;
    [SerializeField]
    private bool m_UseJetpack = true;
    [SerializeField]
    private float m_MaxJetpackVelocity = 20f;
    [SerializeField]
    private float m_JumpVelocity = 2f;
    [SerializeField]
    private LayerMask m_GroundMask;
    [SerializeField]
    private float m_MovementSmoothing = .15f;
    [SerializeField]
    private S_CameraController m_CameraControllerScript;
    [SerializeField]
    private Transform m_Target;

    public S_MachineInteraction Interactor = null;


    private Animator Animate;
    private Rigidbody rb;
    private Vector3 MoveDirection;
    private Vector3 SmoothVelocity;
    private bool interacting;

    // Start is called before the first frame update
    void Start()
    {
        Animate = gameObject.GetComponentInChildren<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CalculatePlayerMovement();
        CalculateRotation();
        Animation();
    }

    void FixedUpdate()
    {
        if(!interacting)
        {
            MovePlayer();
        }
    }

    public void SetInteracting(bool interactingPass)
    {
        interacting = interactingPass;
        Cursor.visible = interactingPass;
    }

    // Calculates player movement vector for fixed update rigidbody
    private void CalculatePlayerMovement()
    {
        float movementSpeed = m_PlayerWalkSpeed;

        // Set Movement speed to sprint if shift is held
        if (Input.GetButton("Sprint"))
        {
            movementSpeed = m_PlayerSprintSpeed;
        }

        Vector3 targetMove = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        MoveDirection = Vector3.SmoothDamp(MoveDirection, targetMove * movementSpeed, ref SmoothVelocity, m_MovementSmoothing);
    }

    private void CalculateRotation()
    {
        Quaternion aim = new Quaternion(0, 0, 0, 0);
        Vector3 cameraDirection = m_CameraControllerScript.GetForwardCamera();
        //Vector3 cameraDirection = m_Target.forward;

        if (m_CameraControllerScript && m_Target) 
        {
            if (Input.GetKey("w"))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.forward, cameraDirection) * transform.rotation, m_PlayerRotateSpeed * Time.deltaTime);
                transform.rotation = Quaternion.FromToRotation(transform.up, m_Target.up) * transform.rotation;
            } 
            else if (Input.GetKey("s"))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.forward, -cameraDirection) * transform.rotation, m_PlayerRotateSpeed * Time.deltaTime);
                transform.rotation = Quaternion.FromToRotation(transform.up, m_Target.up) * transform.rotation;
            } 
            else if (Input.GetKey("a") || Input.GetKey("d")) {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.forward, cameraDirection) * transform.rotation, m_PlayerRotateSpeed * Time.deltaTime);
                transform.rotation = Quaternion.FromToRotation(transform.up, m_Target.up) * transform.rotation;
            }


        }
    }


    // Moves rigidbody within fixedupdate
    private void MovePlayer()
    {
        // Check if player is on the ground
        if (IsGrounded())
        {
            rb.velocity = transform.TransformDirection(MoveDirection);
        }

        if (Input.GetButton("Jump"))
        {
            // If using jetpack
            if (m_UseJetpack)
            {
                // If velocity is below maximum 
                if (rb.velocity.magnitude < m_MaxJetpackVelocity)
                {
                    rb.velocity += transform.up * m_JumpVelocity;
                }
            }
            // ELSE Jumping
            else
            {
                rb.AddForce(transform.up * m_JumpVelocity);
            }
        }

    }



    // Method to check if player is on a surface
    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, .3f, m_GroundMask)) {
            return true;
        }
        return false;

    }
    
    // Runs animation after certain keypresses
    private void Animation()
    {
        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            Animate.SetInteger("AnimationPar", 1);
        }
        else
        {
            Animate.SetInteger("AnimationPar", 0);
        }
    }

}
