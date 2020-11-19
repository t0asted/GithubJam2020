using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CharacterController : MonoBehaviour
{

    [SerializeField]
    private float m_Player_Movement_Speed = 2f;
    [SerializeField]
    private float m_Player_Sprint_Speed = 10f;
    [SerializeField]
    private float m_Jump_Force = 100f;
    [SerializeField]
    private float m_Mouse_Sensititivity = 1f;
    [SerializeField]
    private Transform m_Camera;
    [SerializeField]
    private Transform m_Target;
    [SerializeField]
    private LayerMask m_Ground_Mask;



    private Animator Animate;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Animate = gameObject.GetComponentInChildren<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Animation();
        CameraMovement();
    }


    private void PlayerMovement()
    {
        float forwards = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");


        if (Input.GetButton("Sprint"))
        {
            Vector3 playerMovement = new Vector3(horizontal, 0f, forwards) * m_Player_Sprint_Speed * Time.deltaTime;
            transform.Translate(playerMovement, Space.Self);
        } 
        else
        {
            Vector3 playerMovement = new Vector3(horizontal, 0f, forwards) * m_Player_Movement_Speed * Time.deltaTime;
            transform.Translate(playerMovement, Space.Self);
        }

        if (Input.GetButton("Jump") /*&& IsGrounded()*/)
        {
            rb.AddForce(transform.up * m_Jump_Force);
        }
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, 1, m_Ground_Mask)) {
            return true;
        }
        return false;

    }

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



    private void CameraMovement()
    {
        float mouseX = 0, mouseY = 0;
        mouseX += Input.GetAxis("Mouse X") * m_Mouse_Sensititivity;
        mouseY -= Input.GetAxis("Mouse Y") * m_Mouse_Sensititivity;

        //m_Camera.transform.LookAt(m_Target);


        transform.rotation = Quaternion.Euler(0, mouseX, 0) * transform.rotation;
        //m_Target.localRotation = Quaternion.Euler(mouseY, mouseX, 0) * m_Target.localRotation;

    }

}
