using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform m_ObjectToFollow;

    [SerializeField]
    private Transform CameraArm;
    [SerializeField]
    private Transform Camera;
    [SerializeField]
    private float distance = 5.0f;
    [SerializeField]
    private float xSpeed = 1f;
    [SerializeField]
    private float ySpeed = 1f;

    [SerializeField]
    private float yMinLimit = -30f;
    [SerializeField]
    private float yMaxLimit = 80f;

    [SerializeField]
    private float distanceMin = .5f;
    [SerializeField]
    private float distanceMax = 15f;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    public Vector3 GetForwardCamera()
    {
        return CameraArm.forward;
    }

    void LateUpdate()
    {
        transform.position = m_ObjectToFollow.position;

        transform.rotation = m_ObjectToFollow.rotation;

        if (CameraArm)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance;
            y -= Input.GetAxis("Mouse Y") * ySpeed;
            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
            
            RaycastHit hit;
            if (Physics.Linecast(CameraArm.position, transform.position, out hit))
            {
                distance -= hit.distance;
            }

            //Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            //Vector3 position = rotation * negDistance + Camera.position;
            //
            //Camera.position = position;

            CameraArm.localRotation = rotation;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }


}
