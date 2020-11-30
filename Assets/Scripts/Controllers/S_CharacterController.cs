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
    private float m_DefaultJetpackTime = 3f;
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
    public bool interacting;

    [Space]
    // Thrusters
    [Header("Thrusters")]
    [SerializeField]
    private GameObject m_JetpackLeft = null;
    [SerializeField]
    private GameObject m_JetpackRight = null;
    [SerializeField]
    private GameObject m_ThrusterDownLeft = null;
    [SerializeField]
    private GameObject m_ThrusterDownRight = null;
    [SerializeField]
    private GameObject m_ThrusterLeftTop = null;
    [SerializeField]
    private GameObject m_ThrusterLeftBottom = null;
    [SerializeField]
    private GameObject m_ThrusterRightTop = null;
    [SerializeField]
    private GameObject m_ThrusterRightBottom = null;
    [SerializeField]
    private GameObject m_ThrusterForwardLeft = null;
    [SerializeField]
    private GameObject m_ThrusterForwardRight = null;
    [SerializeField]
    private GameObject m_ThrusterBackwardLeft = null;
    [SerializeField]
    private GameObject m_ThrusterBackwardRight = null;
    [SerializeField]
    private GameObject m_ThrusterReverseLeft = null;
    [SerializeField]
    private GameObject m_ThrusterReverseRight = null;


    private S_CharacterUpgrade CharacterUpgrade = null;
    private S_GravityController GC = null;
    private Animator Animate;
    private Rigidbody rb;
    private Vector3 MoveDirection;
    private Vector3 SmoothVelocity;
    public float RemainingJetpack;
    private bool GroundAnimate = false;

    void Start()
    {
        Animate = gameObject.GetComponentInChildren<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        GC = GetComponent<S_GravityController>();
        CharacterUpgrade = GetComponent<S_CharacterUpgrade>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        GameObject[] thrusters = new GameObject[]{
            m_JetpackLeft, m_JetpackRight, m_ThrusterLeftTop, m_ThrusterLeftBottom,
            m_ThrusterRightTop, m_ThrusterRightBottom, m_ThrusterForwardLeft,
            m_ThrusterForwardRight, m_ThrusterBackwardLeft, m_ThrusterBackwardRight,
            m_ThrusterReverseLeft, m_ThrusterReverseRight, m_ThrusterDownLeft, m_ThrusterDownRight
        };

        foreach (GameObject toSet in thrusters) toSet.SetActive(false);
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
                if (Input.GetAxis("SpaceRotate") > 0)
                {
                    ToggleThrust(Thrusters.SpinForward, true);
                }
                else ToggleThrust(Thrusters.SpinBackward, true);
            }
            else if (transform.InverseTransformDirection(rb.velocity).z < 5)
            {
                ToggleThrust(Thrusters.SpinForward, false);
                ToggleThrust(Thrusters.SpinBackward, false);
            }
        }
    }


    // Moves rigidbody within fixedupdate
    private void MovePlayerGrounded()
    {
        if (CharacterUpgrade.HasThrusterUpgrade()) ClearThrust();

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
                ToggleThrust(Thrusters.Jetpack, true);
            }
            else if (RemainingJetpack > .2f)
            {
                rb.velocity += transform.up * m_JetpackForce;
                RemainingJetpack -= Time.deltaTime;
                ToggleThrust(Thrusters.Jetpack, true);
            }
            else ToggleThrust(Thrusters.Jetpack, false);
        }
        else if (RemainingJetpack < m_DefaultJetpackTime)
        {
            RemainingJetpack += Time.deltaTime * .2f;
            ToggleThrust(Thrusters.Jetpack, false);
        }
        else ToggleThrust(Thrusters.Jetpack, false);


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
            Vector3 direction = ExpoVelocity();

            // Right 
            if (direction.x > 3f) ToggleThrust(Thrusters.Right, true);
            else ToggleThrust(Thrusters.Right, false);

            // Left
            if (direction.x < -3f) ToggleThrust(Thrusters.Left, true);
            else ToggleThrust(Thrusters.Left, false);

            // Forward
            if (direction.z > 3f) ToggleThrust(Thrusters.Forward, true);
            else ToggleThrust(Thrusters.Forward, false);

            // Backward
            if (direction.z < -3f) ToggleThrust(Thrusters.Backwards, true);
            else ToggleThrust(Thrusters.Backwards, false);
        }

        if (Input.GetButton("Jump") && CharacterUpgrade.HasJetpackUpgrade())
        {
            rb.velocity += transform.up * m_PlayerThrusterSpeed;
            ToggleThrust(Thrusters.Jetpack, true);
        }
        else ToggleThrust(Thrusters.Jetpack, false);

        if (Input.GetButton("Descend"))
        {
            rb.velocity -= transform.up * m_PlayerThrusterSpeed;
            ToggleThrust(Thrusters.Down, true);
        }
        else ToggleThrust(Thrusters.Down, false);
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

    // Toggle Thrusters

    private void ToggleThrust(Thrusters thrusters, bool power)
    {
        switch (thrusters)
        {
            case Thrusters.Jetpack:
                m_JetpackLeft.SetActive(power);
                m_JetpackRight.SetActive(power);
                break;
            case Thrusters.Down:
                m_ThrusterDownLeft.SetActive(power);
                m_ThrusterDownRight.SetActive(power);
                break;
            case Thrusters.Forward: 
                m_ThrusterForwardLeft.SetActive(power);
                m_ThrusterForwardRight.SetActive(power);
                m_ThrusterBackwardLeft.SetActive(power);
                m_ThrusterBackwardRight.SetActive(power);
                break;
            case Thrusters.Right:
                m_ThrusterLeftTop.SetActive(power);
                m_ThrusterLeftBottom.SetActive(power);
                break;
            case Thrusters.Left:
                m_ThrusterRightTop.SetActive(power);
                m_ThrusterRightBottom.SetActive(power);
                break;
            case Thrusters.SpinForward:
                m_ThrusterForwardLeft.SetActive(power);
                m_ThrusterForwardRight.SetActive(power);
                break;
            case Thrusters.SpinBackward:
                m_ThrusterBackwardLeft.SetActive(power);
                m_ThrusterBackwardRight.SetActive(power);
                break;
            case Thrusters.Backwards:
                m_ThrusterReverseLeft.SetActive(power);
                m_ThrusterReverseRight.SetActive(power);
                break;
        }
    }

    private void ClearThrust()
    {
        ToggleThrust(Thrusters.Down, false);
        ToggleThrust(Thrusters.Left, false);
        ToggleThrust(Thrusters.Right, false);
        ToggleThrust(Thrusters.Forward, false);
        ToggleThrust(Thrusters.Backwards, false);
    }

    private Vector3 ExpoVelocity()
    {
        Vector3 temp = transform.InverseTransformDirection(rb.velocity);
        temp = new Vector3(Exponential(temp.x), Exponential(temp.y), Exponential(temp.z));
        Debug.Log(temp);
        return temp;
    }

    private float Exponential(float val)
    {
        return Mathf.Clamp((val / Mathf.Abs(val)) * Mathf.Pow(1.1f, Mathf.Abs(val)), -1000, 1000);
    }
}

public enum Thrusters
{
    Jetpack = 1,
    Down = 2, 
    Forward = 3, 
    Right = 4, 
    Left = 5, 
    SpinForward = 6, 
    SpinBackward = 7, 
    Backwards = 8
}
