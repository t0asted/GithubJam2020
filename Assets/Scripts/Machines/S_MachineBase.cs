using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class S_MachineBase : MonoBehaviour
{
    public S_GameController ref_GameController = null;
    [SerializeField]
    public CL_Machine MachineData = new CL_Machine();
    [SerializeField]
    public bool MachineRunning = true;

    public void Interact()
    {
        MachineRunning = !MachineRunning;
    }



}