using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [SerializeField]

    float moveSpeed = 4f;

    Vector3 forward, right;

    private Animator anim;
    private CharController controller;

    public float speed = 600.0f;
    public float turnSpeed = 400.0f;
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharController>();
        anim = gameObject.GetComponentInChildren<Animator>();
        forward = (Camera.main.transform.forward);
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }
    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            anim.SetInteger("AnimationPar", 1);
            Move();
        }
        else
        {
            anim.SetInteger("AnimationPar", 0);
        }
    }

    public void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        transform.forward = heading;
        transform.position += rightMovement;
        transform.position += upMovement;
    }


}