using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CharacterController : MonoBehaviour
{

    [SerializeField]
    private float m_PlayerWalkSpeed = 0.5f;
    [SerializeField]
    private float m_PlayerSprintSpeed = 0.7f;
    [SerializeField]
    private float m_PlayerRotateSpeed = 3f;
    [SerializeField]
    private float m_PlayerThrusterSpeed = 1.5f;
    [SerializeField]
    private float m_JetpackForce = 1.3f;
    [SerializeField]
    private float m_DefaultJetpackTime = 4f;
    [SerializeField]
    private LayerMask m_GroundMask;
    [SerializeField]
    private float m_MovementSmoothing = .15f;
    [SerializeField]
    private S_CameraController m_CameraControllerScript = null;
    [SerializeField]
    private Transform m_Target = null;

    public S_PlaceMachine m_MachinePlacer = null;
    public S_MachineInteraction Interactor = null;
    private S_CharacterUpgrade CharacterUpgrade = null;
    private S_GravityController GC = null;

    private Animator Animate;
    private Rigidbody rb;
    private Vector3 MoveDirection;
    private Vector3 SmoothVelocity;
    private float RemainingJetpack;
    public bool interacting;
    private bool GroundAnimate = false;

    void Start()
    {
        Animate = gameObject.GetComponentInChildren<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        GC = GetComponent<S_GravityController>();
        CharacterUpgrade = GetComponent<S_CharacterUpgrade>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!interacting)
        {
            if (GC.HasPlanet())
            {
                CalculateGroundMovement();
                CalculateGroundRotation();
                Animation(true);
            }
            else
            {
                CalculateSpaceMovement();
                CalculateSpaceRotation();
            }
        }
        else
        {
            Animation(false);
        }
    }

    void FixedUpdate()
    {
        if (!interacting)
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
    }

    public void SetInteracting(bool interactingPass)
    {
        Cursor.lockState = interactingPass ? CursorLockMode.Confined : CursorLockMode.Locked;
        interacting = interactingPass;
        Cursor.visible = interactingPass;
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
        Vector3 targetMove;
        if (Input.GetAxis("Vertical") < 0)
        {
            targetMove = new Vector3(-Input.GetAxisRaw("Horizontal"), 0, Mathf.Abs(Input.GetAxisRaw("Vertical"))).normalized;
        }
        else
        {
            targetMove = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Mathf.Abs(Input.GetAxisRaw("Vertical"))).normalized;
        }
        //MoveDirection = Vector3.SmoothDamp(MoveDirection, targetMove * m_PlayerThrusterSpeed, ref SmoothVelocity, m_MovementSmoothing);
        MoveDirection = targetMove * m_PlayerThrusterSpeed;
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

            if (Input.GetAxis("SpaceRotate") != 0 && CharacterUpgrade.HasThrusterUpgrade())
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
            rb.velocity += transform.TransformDirection(MoveDirection);
            if (rb.velocity.magnitude > 0)
            {
                rb.velocity = rb.velocity * 0.95f;
            }
            GroundAnimate = true;
        }
        else
        {
            GroundAnimate = false;
        }

        if (Input.GetButton("Jump"))
        {
            if (CharacterUpgrade.HasJetpackUpgrade())
            {
                rb.velocity += transform.up * m_JetpackForce;
            }
            else if (RemainingJetpack > 0)
            {
                rb.velocity += transform.up * m_JetpackForce;
                RemainingJetpack -= Time.deltaTime;
            }
        }
        else if (RemainingJetpack < m_DefaultJetpackTime)
        {
            RemainingJetpack += Time.deltaTime * .3f;
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

        if (CharacterUpgrade.HasThrusterUpgrade())
        {
            rb.velocity += transform.TransformDirection(MoveDirection);
        }

        if (Input.GetButton("Jump") && CharacterUpgrade.HasJetpackUpgrade())
        {
            rb.velocity += transform.up * m_PlayerThrusterSpeed;
        }
        if (Input.GetButton("Descend"))
        {
            rb.velocity -= transform.up * m_PlayerThrusterSpeed;
        }
    }



    // Method to check if player is on a surface
    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, 2f, m_GroundMask))
        {
            return true;
        }
        return false;

    }

    // Runs animation after certain keypresses
    private void Animation(bool Animating)
    {
        if (Animating)
        {
            if (GroundAnimate && (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")) != 0))
            {
                Animate.SetInteger("AnimationPar", 1);
            }
            else
            {
                Animate.SetInteger("AnimationPar", 0);
            }
        }
        else
        {
            Animate.SetInteger("AnimationPar", 0);
        }
    }

}
