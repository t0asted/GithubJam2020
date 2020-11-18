using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CharacterController : MonoBehaviour
{

    [SerializeField]
    private float m_Player_Movement_Speed = 2;
    [SerializeField]
    private float m_Mouse_Sensititivity = 1;
    [SerializeField]
    private Transform m_Camera;
    [SerializeField]
    private Transform m_Target;



    private Animator Animate;

    // Start is called before the first frame update
    void Start()
    {
        Animate = gameObject.GetComponentInChildren<Animator>();
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

        Vector3 playerMovement = new Vector3(horizontal, 0f, forwards) * m_Player_Movement_Speed * Time.deltaTime;

        transform.Translate(playerMovement, Space.Self);
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

        m_Camera.transform.LookAt(m_Target);


        transform.localRotation = Quaternion.Euler(0, mouseX, 0) * transform.localRotation;
        m_Target.localRotation = Quaternion.Euler(mouseY, mouseX, 0) * m_Target.localRotation;

    }

}
