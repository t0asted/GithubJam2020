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
    private float m_JetpackOverchargeSpeed = 2.5f;

    [Space]
    [Header("Jetpack")]
    [SerializeField]
    private float m_JetpackForce = 1.3f;
    [SerializeField]
    private float m_DefaultJetpackTime = 3f;
    [SerializeField]
    private float m_OverchargeJetpackTime = 3f;
    [SerializeField]
    private LayerMask m_GroundMask;
    [SerializeField]
    private float m_MovementSmoothing = .15f;
    [SerializeField]
    private S_ThrusterController m_Thrust = null;
    [SerializeField]
    private float m_VerticalDeadzone = .2f;
    [SerializeField]
    private float m_HorizontalDeadzone = .2f;


    [Space]
    [Header("Camera")]
    [SerializeField]
    private S_CameraController m_CameraControllerScript = null;
    [SerializeField]
    private Transform m_Target = null;
    [SerializeField]
    private float m_CameraSpaceAngleOffset = .2f;

    public S_PlaceMachine m_MachinePlacer = null;
    public S_MachineInteraction Interactor = null;
    public bool interacting;



    private S_CharacterUpgrade CharacterUpgrade = null;
    private S_GravityController GC = null;
    private Animator Animate;
    private Rigidbody rb;
    private Vector3 MoveDirection;
    private Vector3 SmoothVelocity;
    private float RemainingJetpack;
    private float RemainingOvercharge;
    private bool GroundAnimate = false;


    void Start()
    {
        Animate = gameObject.GetComponentInChildren<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        GC = GetComponent<S_GravityController>();
        CharacterUpgrade = GetComponent<S_CharacterUpgrade>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        RemainingJetpack = m_DefaultJetpackTime;
        RemainingOvercharge = m_OverchargeJetpackTime;
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
        else Animation(false);
    }

    void FixedUpdate()
    {
        if (!interacting)
        {
            if (GC.HasPlanet()) MovePlayerGrounded();
            else MovePlayerSpace();
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
        if (Input.GetButton("Sprint")) movementSpeed = m_PlayerSprintSpeed;

        Vector3 targetMove = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        MoveDirection = Vector3.SmoothDamp(MoveDirection, targetMove * movementSpeed, ref SmoothVelocity, m_MovementSmoothing);
    }

    // Calculates rotation of character from camera direction
    private void CalculateGroundRotation()
    {
        Vector3 cameraDirection = m_CameraControllerScript.GetForwardCamera();

        if (m_CameraControllerScript && m_Target)
        {
            if (Input.GetAxis("Vertical") != 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.forward, cameraDirection) * transform.rotation, m_PlayerRotateSpeed * Time.deltaTime);
                transform.rotation = Quaternion.FromToRotation(transform.up, m_Target.up) * transform.rotation;
            }
            else if (Input.GetAxis("Horizontal") != 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.forward, cameraDirection) * transform.rotation, m_PlayerRotateSpeed * Time.deltaTime);
                transform.rotation = Quaternion.FromToRotation(transform.up, m_Target.up) * transform.rotation;
            }
        }
    }

    // Moves rigidbody within fixedupdate
    private void MovePlayerGrounded()
    {
        m_Thrust.ClearThrust();
        if (m_Thrust.IsOvercharge()) m_Thrust.SetJetpackSpeed(false);

        // Check if player is on the ground
        if (IsGrounded())
        {
            rb.velocity += transform.TransformDirection(MoveDirection);
            if (rb.velocity.magnitude > 0) rb.velocity = rb.velocity * 0.95f;
            GroundAnimate = true;
        }
        else GroundAnimate = false;

        if (Input.GetAxis("Jetpack") > 0)
        {
            if (CharacterUpgrade.HasJetpackUpgrade())
            {
                rb.velocity += transform.up * m_JetpackForce;
                m_Thrust.SetThruster(Thruster.Jetpack, true);
            }
            else if (RemainingJetpack > .1f)
            {
                rb.velocity += transform.up * m_JetpackForce;
                RemainingJetpack -= Time.deltaTime;
                m_Thrust.SetThruster(Thruster.Jetpack, true);
            }
            else m_Thrust.SetThruster(Thruster.Jetpack, false);
        }
        else if (RemainingJetpack < m_DefaultJetpackTime)
        {
            RemainingJetpack += Time.deltaTime * .2f;
            m_Thrust.SetThruster(Thruster.Jetpack, false);
        }
        else m_Thrust.SetThruster(Thruster.Jetpack, false);
    }


    // Calculates player movement whilst in space
    private void CalculateSpaceMovement()
    {
        // Reset ground jetpack
        if (RemainingJetpack < m_DefaultJetpackTime) RemainingJetpack += Time.deltaTime * .2f;

        float tempSpeed = m_PlayerThrusterSpeed;
        float verticalJ = Input.GetAxisRaw("Jetpack");
        float verticalV = Input.GetAxisRaw("Vertical");
        float vertical = verticalJ;
        if (verticalV != 0) vertical = verticalV;
        if (!CharacterUpgrade.HasJetpackUpgrade())
        {
            vertical = Mathf.Min(vertical, 0);
            m_Thrust.SetJetpackSpeed(false);
        }
        else if (Input.GetButton("Sprint") && RemainingOvercharge > .1f)
        {
            tempSpeed = m_JetpackOverchargeSpeed;
            m_Thrust.SetJetpackSpeed(true);
            RemainingOvercharge -= Time.deltaTime;
        }
        else if (RemainingOvercharge < m_OverchargeJetpackTime)
        {
            m_Thrust.SetJetpackSpeed(false);
            RemainingOvercharge += Time.deltaTime * .2f;
        }
        else m_Thrust.SetJetpackSpeed(false);

        //float horizontal = Input.GetAxisRaw("Horizontal");
        //if (!CharacterUpgrade.HasThrusterUpgrade()) horizontal = 0f;

        Vector3 targetMove = new Vector3(0, vertical, 0).normalized;
        MoveDirection = targetMove * tempSpeed;
    }


    // Calculates rotation of character from camera direction
    private void CalculateSpaceRotation()
    {
        Vector3 cameraDirection = m_CameraControllerScript.GetForwardCamera();
        Vector3 temp = transform.InverseTransformDirection(cameraDirection);

        // DEADZONES
        temp.z = temp.z + Mathf.Min(Mathf.Max((temp.z * -1), -m_VerticalDeadzone), m_VerticalDeadzone);
        temp.x = temp.x + Mathf.Min(Mathf.Max((temp.x * -1), -m_HorizontalDeadzone), m_HorizontalDeadzone);

        Vector3 spaceCamera = transform.TransformDirection(temp);

        if (m_CameraControllerScript)
        {
            if ((Input.GetAxis("Vertical") > 0 || Input.GetAxis("Jetpack") > 0) && CharacterUpgrade.HasThrusterUpgrade())
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.up, spaceCamera) * transform.rotation, m_PlayerRotateSpeed * Time.deltaTime);
               
            }

            if (Input.GetAxis("Roll") != 0 && CharacterUpgrade.HasThrusterUpgrade())
            {
                transform.Rotate(0, -Input.GetAxis("Roll") * m_PlayerThrusterSpeed * .5f, 0);
            }
            /*
            if (Input.GetAxis("Horizontal") != 0 && CharacterUpgrade.HasThrusterUpgrade())
            {
                //transform.Rotate(0,0, -Input.GetAxis("Horizontal") * m_PlayerThrusterSpeed * .3f);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.up, horizontalDirection) * transform.rotation, m_PlayerRotateSpeed * Time.deltaTime);
            }
            */
        }
    }

    private void MovePlayerSpace()
    {
        
        // Damping
        if (rb.velocity.magnitude > 0) rb.velocity = rb.velocity * 0.95f;
        else rb.velocity = Vector3.zero;

        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);

        // Jetpack
        if (CharacterUpgrade.HasJetpackUpgrade())
        {
            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Jetpack") != 0)
                ThrusterVelocity(localVelocity.y, Thruster.Jetpack, Thruster.Down);
            else ThrusterVelocity(localVelocity.y, Thruster.Down, Thruster.Jetpack);
        }
        else ThrusterVelocity(localVelocity.y, Thruster.Jetpack, Thruster.Down);

        // Thrusters
        if (CharacterUpgrade.HasThrusterUpgrade())
        {

            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Jetpack") != 0)
            {
                ThrusterVelocity(localVelocity.x, Thruster.Left, Thruster.Right);
                ThrusterVelocity(localVelocity.z, Thruster.Backward, Thruster.Forward);
            }
            else
            {
                ThrusterVelocity(localVelocity.x, Thruster.Right, Thruster.Left);
                ThrusterVelocity(localVelocity.z, Thruster.Forward, Thruster.Backward);
            }

            // False setting handled by jetpack above
            if (Input.GetAxisRaw("Roll")>0) m_Thrust.SetThruster(Thruster.RollRight, true);
            if (Input.GetAxisRaw("Roll") < 0) m_Thrust.SetThruster(Thruster.RollLeft, true);
        }

        rb.velocity += transform.TransformDirection(MoveDirection);
    }


    private void ThrusterVelocity(float axis, Thruster positive, Thruster negative)
    {
        if (axis > 3f)
        {
            m_Thrust.SetThruster(negative, false);
            m_Thrust.SetThruster(positive, true);
        }
        else if (axis < -3f)
        {
            m_Thrust.SetThruster(positive, false);
            m_Thrust.SetThruster(negative, true);
        }
        else
        {
            m_Thrust.SetThruster(positive, false);
            m_Thrust.SetThruster(negative, false);
        }
    }


    // Method to check if player is on a surface
    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 2f, m_GroundMask)) return true;
        return false;
    }

    // Runs animation after certain keypresses
    private void Animation(bool Animating)
    {
        if (Animating)
        {
            if (GroundAnimate && (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")) != 0))
            { Animate.SetInteger("AnimationPar", 1);}
            else Animate.SetInteger("AnimationPar", 0);
        }
        else Animate.SetInteger("AnimationPar", 0);
    }
}
