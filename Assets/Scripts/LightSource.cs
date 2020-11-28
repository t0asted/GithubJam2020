using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    private float timestamp;

    void Start()
    {
        timestamp = Time.time;
    }

    void Update()
    {
        float angle = (Time.time - timestamp) / 1140.0f * 360.0f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
