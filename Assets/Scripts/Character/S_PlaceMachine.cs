using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlaceMachine : MonoBehaviour
{
    [SerializeField]
    private S_CameraController Camera;
    [SerializeField]
    private S_GravityController m_GravityController;
    [SerializeField]
    private Transform characterDirection;

    private GameObject MachineToSpawn = null;
    private GameObject NewMachine = null;
    public bool HasItemToPlace = false;
    private GameObject AstroidOn;

    public void NewMachineToPlace(GameObject NewMachinePass)
    {
        MachineToSpawn = NewMachinePass;
    }

    private void Update()
    {
        if (MachineToSpawn != null)
        {
            PlaceMachine();
            if (Input.GetKey("j") && MachineToSpawn != null)
            {
                if(NewMachine.GetComponent<S_MachineBase>())
                {
                    NewMachine.GetComponent<S_MachineBase>().Place();
                }
                if (NewMachine.GetComponent<S_MachineGatherer>() != null)
                {
                    NewMachine.GetComponent<S_MachineGatherer>().AstroidData = AstroidOn.GetComponent<S_Astroid>().LevelData;
                }
                MachineToSpawn = null;
                NewMachine = null;
            }
        }
    }

    public void PlaceMachine()
    {

        if (NewMachine == null)
        {
            NewMachine = Instantiate(MachineToSpawn);
        }
        else
        {
            RaycastHit hit;
            Debug.DrawRay(characterDirection.position, Camera.GetForwardCamera() * 5);
            if (Physics.Raycast(characterDirection.position, Camera.GetForwardCamera() * 5, out hit))
            {
                if (hit.transform.gameObject.tag == "Astroid")
                {
                    AstroidOn = hit.transform.gameObject;
                    NewMachine.transform.position = hit.point;
                    NewMachine.transform.up = m_GravityController.GetGravityDirection(NewMachine.transform);
                    NewMachine.transform.rotation = characterDirection.rotation;
                }
            }
        }

    }
}
