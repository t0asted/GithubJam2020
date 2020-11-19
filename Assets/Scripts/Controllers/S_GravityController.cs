using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GravityController : MonoBehaviour
{
    [SerializeField]
    private S_Gravity Planet;

    private Rigidbody Player;

    [SerializeField]
    private float RotateSpeed = 20;

    [SerializeField]
    private float Gravity = 7;

    public void SetPlanet(S_Gravity planet)
    {
        Planet = planet;
        Debug.Log("Planet value set to " + planet.gameObject.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Planet)
        {
            Vector3 gravityUp = (transform.position - Planet.transform.position).normalized;
            Vector3 localUp = transform.up;

            Quaternion target = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;

            transform.up = Vector3.Lerp(transform.up, gravityUp, RotateSpeed * Time.deltaTime);

            //transform.rotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;
            Player.AddForce(-gravityUp * Gravity);
        }


    }
}
