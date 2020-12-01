using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GravityController : MonoBehaviour
{
    [SerializeField]
    private S_Gravity m_Planet = null;
    
    [SerializeField] 
    private float m_TargetOffset = 2;
    [SerializeField]
    private float m_RotateToPlanetSpeed = 3;
    [SerializeField]
    private float m_Gravity = 7;

    [Header("Space Camera")]
    [SerializeField]
    private Transform m_Camera = null;
    [SerializeField]
    private Transform m_Target = null;
    [SerializeField]
    private float m_RotateToJetpackSpeed = 2;
    [SerializeField]
    private float m_XCameraOffset = 0f;
    [SerializeField]
    private float m_YCameraOffset = 1.4f;



    private Rigidbody Player;
    private Vector3 gravityDirection;
    private float CameraXOffset;
    private float CameraYOffset;
    private S_CharacterUpgrade CharUpgrade = null;


    public void SetPlanet(S_Gravity planet)
    {
        m_Planet = planet;
    }

    public void ClearPlanet()
    {
        m_Planet = null;
    }

    public bool HasPlanet()
    {
        return m_Planet;
    }

    public Vector3 GetGravityDirection(Transform transformPass)
    {
        if(m_Planet != null)
        {
            return (transformPass.position - m_Planet.transform.position).normalized;
        }
        return new Vector3();
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Rigidbody>();
        CharUpgrade = GetComponent<S_CharacterUpgrade>();
        if (m_Camera)
        {
            CameraXOffset = m_Camera.transform.localPosition.x;
            CameraYOffset = m_Camera.transform.localPosition.y;
        }
    }

    // Fixed Update 
    void Update()
    {
        m_Target.transform.position = transform.position;
        m_Target.transform.position += m_Target.up * m_TargetOffset;
        if (m_Planet)
        {
            if (m_Camera.localPosition != new Vector3(CameraXOffset, CameraYOffset, m_Camera.localPosition.z))
            {
                m_Camera.localPosition = Vector3.Lerp(m_Camera.localPosition, new Vector3(CameraXOffset, CameraYOffset, m_Camera.localPosition.z), m_RotateToPlanetSpeed * Time.deltaTime);
            }

            // Calculate Vector for gravity direction
            gravityDirection = (transform.position - m_Planet.transform.position).normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.up, gravityDirection) * transform.rotation, m_RotateToPlanetSpeed * Time.deltaTime);


            // Move Target with Character
            m_Target.rotation = Quaternion.Slerp(m_Target.rotation, Quaternion.FromToRotation(m_Target.up, gravityDirection) * m_Target.rotation, m_RotateToPlanetSpeed * Time.deltaTime);
            


            if (m_Camera.transform.localPosition != new Vector3(CameraXOffset, m_Camera.localPosition.y, m_Camera.localPosition.z))
            {
                m_Camera.transform.localPosition = Vector3.Lerp(m_Camera.localPosition, new Vector3(CameraXOffset, m_Camera.localPosition.y, m_Camera.localPosition.z), m_RotateToPlanetSpeed * Time.deltaTime);
            }
        }
        else if (CharUpgrade && CharUpgrade.HasThrusterUpgrade())
        {
            //m_Target.position = Vector3.Lerp(m_Target.position, transform.position, m_RotateToJetpackSpeed * Time.deltaTime);
            m_Target.position = transform.position;

            m_Target.rotation = Quaternion.Slerp(m_Target.rotation, Quaternion.FromToRotation(m_Target.up, -transform.forward) * m_Target.rotation, m_RotateToJetpackSpeed * Time.deltaTime);

            if (m_Camera.localPosition != new Vector3(m_XCameraOffset, m_YCameraOffset, m_Camera.localPosition.z))
            {
                m_Camera.localPosition = Vector3.Lerp(m_Camera.localPosition, new Vector3(m_XCameraOffset, m_YCameraOffset, m_Camera.localPosition.z), m_RotateToJetpackSpeed * Time.deltaTime);
            }
        }
        else
        {
            
            m_Target.rotation = Quaternion.Slerp(m_Target.rotation, Quaternion.FromToRotation(m_Target.up, transform.up) * m_Target.rotation, m_RotateToJetpackSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        if (m_Planet)
        {
            // Add Gravity
            Player.AddForce(-gravityDirection * m_Gravity);
        }
    }
}
