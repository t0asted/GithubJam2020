using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject ObjectToFollow;

    public float speed = .5f;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, ObjectToFollow.transform.position, speed);
    }
}
