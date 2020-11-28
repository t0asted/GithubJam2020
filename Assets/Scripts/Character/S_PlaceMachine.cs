using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlaceMachine : MonoBehaviour
{
    // get sent which prefab is going to be instantiated
    // cast from character in direction of camera X distance
    // on hit transform posistion show ghost machine of placing
    // detect if colliding with anything
    // on click place machine

    [SerializeField]
    private GameObject MachineToSpawn;

    private GameObject MachineRef;

    private void Update()
    {
        if(Input.GetKey("j") && MachineToSpawn != null)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward, Color.red);
            if (Physics.Linecast(transform.position, transform.forward, out hit))
            {
                if (hit.transform.gameObject.tag == "Astroid")
                {
                    MachineRef = Instantiate(MachineToSpawn, hit.point, new Quaternion());
                    MachineToSpawn = null;
                }
            }
            else
            {
                Debug.Log("No hit!");
            }
        }
        else
        {
            Debug.Log("No J!");
        }
    }
}
