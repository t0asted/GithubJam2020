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
    private float m_RotateSpeed = 20;
    [SerializeField]
    private float m_Gravity = 7;

    private Rigidbody Player;
    private Vector3 gravityDirection;

    public void SetPlanet(S_Gravity planet)
    {
        m_Planet = planet;
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Rigidbody>();
    }

    // Fixed Update 
    void Update()
    {
        if (m_Planet)
        {
            // Calculate Vector for gravity direction
            gravityDirection = (transform.position - m_Planet.transform.position).normalized;
            transform.rotation = Quaternion.FromToRotation(transform.up, gravityDirection) * transform.rotation;
            

            // Move Target with Character
            m_Target.transform.position = transform.position;
            m_Target.transform.position += m_Target.up * m_TargetOffset;
            m_Target.transform.rotation = Quaternion.FromToRotation(m_Target.transform.up, gravityDirection) * m_Target.transform.rotation;



            // IDONTKNOWITWORKPLSHELPIAMGOINGINSANE
            // Lerp to new 
            //transform.up = Vector3.Lerp(transform.up, gravityDirection, m_RotateSpeed * Time.deltaTime);
            //m_Target.transform.rotation = transform.rotation;
            //transform.rotation = Quaternion.FromToRotation(transform.up, gravityDirection) * transform.rotation;
            //m_Target.transform.up = gravityDirection;

        }
    }

    void FixedUpdate()
    {
        // Add Gravity
        Player.AddForce(-gravityDirection * m_Gravity);
    }
}
