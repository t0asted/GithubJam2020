using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(SphereCollider))]
public class S_Gravity : MonoBehaviour
{

    private SphereCollider sc;

    [SerializeField]
    private float TriggerRadius = 0.7f;

    void Start()
    {
        sc = this.GetComponent<SphereCollider>();
        sc.isTrigger = true;
        sc.radius = TriggerRadius;
    }


    private void OnTriggerStay(Collider other)
    {

        if (other.GetComponent<S_GravityController>())
        {

            other.GetComponent<S_GravityController>().SetPlanet(this.GetComponent<S_Gravity>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<S_GravityController>())
        {
            other.GetComponent<S_GravityController>().ClearPlanet();
        }
    }
}
