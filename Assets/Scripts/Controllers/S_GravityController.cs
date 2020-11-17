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
    private float Gravity = 20;

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

            Debug.Log("Centre of Planet - " + Planet.transform.position);


            Player.AddForce(-gravityUp * Gravity);

            transform.rotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;



            //Vector3 gravityUp = Vector3.zero;
            //
            //gravityUp = (transform.position - gravityUp).normalized;
            //
            //Vector3 local = transform.up;
            //
            //Quaternion targetRotation = Quaternion.Euler(gravityUp);//Quaternion.FromToRotation(local, gravityUp) * transform.rotation;
            //
            //transform.rotation = targetRotation;
            //
            ////transform.up = Vector3.Lerp(transform.up, targetRotation, RotateSpeed * Time.deltaTime);
            //
            //Player.AddForce((-gravityUp * Gravity) * Player.mass);
            //
            //Debug.Log(targetRotation);
            ////transform.Translate(-gravityUp);
        }


    }
}
