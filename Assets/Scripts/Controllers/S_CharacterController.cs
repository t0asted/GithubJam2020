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
    private float m_PlayerThrusterSpeed = 1f;

    [SerializeField]
    private float m_MaxJetpackVelocity = 20f;
    [SerializeField]
    private float m_JetpackForce = 2f;
    [SerializeField]
    private LayerMask m_GroundMask;
    [SerializeField]
    private float m_MovementSmoothing = .15f;
    [SerializeField]
    private S_CameraController m_CameraControllerScript;
    [SerializeField]
    private Transform m_Target;


    private Animator Animate;
    private Rigidbody rb;
    private Vector3 MoveDirection;
    private Vector3 SmoothVelocity;
    private S_GravityController GC;
    private bool GroundAnimate = false;

    // Start is called before the first frame update
    void Start()
    {
        Animate = gameObject.GetComponentInChildren<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GC = GetComponent<S_GravityController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GC.HasPlanet())
        {
            CalculateGroundMovement();
            CalculateGroundRotation();
            Animation();
        }
        else
        {
            CalculateSpaceMovement();
            CalculateSpaceRotation();
        }
    }

    void FixedUpdate()
    {
        if (GC.HasPlanet())
        {
            MovePlayerGrounded();
        }
        else
        {
            MovePlayerSpace();
        }

    }

    // Calculates player movement vector for fixed update rigidbody
    private void CalculateGroundMovement()
    {
        float movementSpeed = m_PlayerWalkSpeed;

        // Set Movement speed to sprint if shift is held
        if (Input.GetButton("Sprint"))
        {
            movementSpeed = m_PlayerSprintSpeed;
        }

        // Check if S is pressed
        // If so reverse a and d directions
        Vector3 targetMove;
 
        if (Input.GetAxis("Vertical") < 0)
        {
            targetMove = new Vector3(-Input.GetAxisRaw("Horizontal"), 0, Mathf.Abs(Input.GetAxisRaw("Vertical"))).normalized;
        }
        else
        {
            targetMove = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        }

        // Setup movedirection with movement speeds and smoothing
        MoveDirection = Vector3.SmoothDamp(MoveDirection, targetMove * movementSpeed, ref SmoothVelocity, m_MovementSmoothing);
    }


    // Calculates player movement whilst in space
    private void CalculateSpaceMovement()
    {
        Vector3 targetMove = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        MoveDirection = Vector3.SmoothDamp(MoveDirection, targetMove * m_PlayerThrusterSpeed, ref SmoothVelocity, m_MovementSmoothing);
    }


    // Calculates rotation of character from camera direction
    private void CalculateGroundRotation()
    {

        Vector3 cameraDirection = m_CameraControllerScript.GetForwardCamera();

        if (m_CameraControllerScript && m_Target)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.forward, cameraDirection) * transform.rotation, m_PlayerRotateSpeed * Time.deltaTime);
                transform.rotation = Quaternion.FromToRotation(transform.up, m_Target.up) * transform.rotation;
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.forward, -cameraDirection) * transform.rotation, m_PlayerRotateSpeed * Time.deltaTime);
                transform.rotation = Quaternion.FromToRotation(transform.up, m_Target.up) * transform.rotation;
            }
            else if (Input.GetAxis("Horizontal") != 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.forward, cameraDirection) * transform.rotation, m_PlayerRotateSpeed * Time.deltaTime);
                transform.rotation = Quaternion.FromToRotation(transform.up, m_Target.up) * transform.rotation;
            }
        }
    }

    // Calculates rotation of character from camera direction
    private void CalculateSpaceRotation()
    {

        Vector3 cameraDirection = m_CameraControllerScript.GetForwardCamera();

        if (m_CameraControllerScript && m_Target)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.forward, cameraDirection) * transform.rotation, m_PlayerRotateSpeed * Time.deltaTime);
                transform.rotation = Quaternion.FromToRotation(transform.up, m_Target.up) * transform.rotation;
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.forward, -cameraDirection) * transform.rotation, m_PlayerRotateSpeed * Time.deltaTime);
                transform.rotation = Quaternion.FromToRotation(transform.up, m_Target.up) * transform.rotation;
            }
            else if (Input.GetAxis("Horizontal") != 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.forward, cameraDirection) * transform.rotation, m_PlayerRotateSpeed * Time.deltaTime);
                transform.rotation = Quaternion.FromToRotation(transform.up, m_Target.up) * transform.rotation;
            }

            if (Input.GetAxis("SpaceRotate") != 0)
            { 
                transform.Rotate(Input.GetAxis("SpaceRotate") * m_PlayerThrusterSpeed, 0, 0);
                m_Target.rotation = Quaternion.FromToRotation(m_Target.up, transform.up) * m_Target.rotation;
            }
        }
    }


    // Moves rigidbody within fixedupdate
    private void MovePlayerGrounded()
    {
        // Check if player is on the ground
        if (IsGrounded())
        {
            rb.velocity = transform.TransformDirection(MoveDirection);
            GroundAnimate = true;
        }
        else
        {
            GroundAnimate = false;
        }

        if (Input.GetButton("Jump"))
        {
            if (rb.velocity.magnitude < m_MaxJetpackVelocity)
            {
                rb.velocity += transform.up * m_JetpackForce;
            }
        }

    }


    private void MovePlayerSpace()
    {
        if (rb.velocity.magnitude > 0)
        {
            rb.velocity = rb.velocity * 0.95f;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        rb.velocity += transform.TransformDirection(MoveDirection);

        if (Input.GetButton("Jump"))
        {
            rb.velocity += transform.up * m_JetpackForce;
        }
        if (Input.GetButton("Descend"))
        {
            rb.velocity -= transform.up * m_JetpackForce;
        }
    }



    // Method to check if player is on a surface
    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, .3f, m_GroundMask))
        {
            return true;
        }
        return false;

    }

    // Runs animation after certain keypresses
    private void Animation()
    {
        if (GroundAnimate && (Input.GetAxis("Vertical") + Input.GetAxis("Horizontal") != 0))
        {
            Animate.SetInteger("AnimationPar", 1);
        }
        else
        {
            Animate.SetInteger("AnimationPar", 0);
        }
    }

}
