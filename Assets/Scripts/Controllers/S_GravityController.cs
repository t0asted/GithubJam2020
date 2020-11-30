using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GravityController : MonoBehaviour
{
    [SerializeField]
    private S_Gravity m_Planet;
    [SerializeField]
    private Transform m_Target;
    
    [SerializeField] 
    private float m_TargetOffset = 2;
    [SerializeField]
    private float m_RotateSpeed = 5;
    [SerializeField]
    private float m_Gravity = 7;

    private Rigidbody Player;
    private Vector3 gravityDirection;


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
    }

    // Fixed Update 
    void Update()
    {
        m_Target.transform.position = transform.position;
        m_Target.transform.position += m_Target.up * m_TargetOffset;
        if (m_Planet)
        {
            // Calculate Vector for gravity direction
            gravityDirection = (transform.position - m_Planet.transform.position).normalized;
            //transform.rotation = Quaternion.FromToRotation(transform.up, gravityDirection) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.up, gravityDirection) * transform.rotation, m_RotateSpeed * Time.deltaTime);


            // Move Target with Character
            m_Target.transform.rotation = Quaternion.Slerp(m_Target.transform.rotation, Quaternion.FromToRotation(m_Target.transform.up, gravityDirection) * m_Target.transform.rotation, m_RotateSpeed * Time.deltaTime);
            //m_Target.transform.rotation = Quaternion.FromToRotation(m_Target.transform.up, gravityDirection) * m_Target.transform.rotation;



            // IDONTKNOWITWORKPLSHELPIAMGOINGINSANE
            // Lerp to new 
            //transform.up = Vector3.Lerp(transform.up, gravityDirection, m_RotateSpeed * Time.deltaTime);
            //m_Target.transform.rotation = transform.rotation;
            //transform.rotation = Quaternion.FromToRotation(transform.up, gravityDirection) * transform.rotation;
            //m_Target.transform.up = gravityDirection;

        }
        else
        {
            //m_Target.transform.rotation = Quaternion.Slerp(m_Target.transform.rotation, Quaternion.FromToRotation(m_Target.transform.right, transform.right) * m_Target.transform.rotation, m_RotateSpeed * Time.deltaTime);
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
