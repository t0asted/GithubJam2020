using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_RocketUI : S_SceneUIMain
{
    private S_GameController ref_GameController = null;
    private S_MachineBase Rocket;
    [SerializeField]
    private CL_Machine MachineData;

    private void Start()
    {
        if (GameObject.Find("_GameController") != null)
        {
            ref_GameController = GameObject.Find("_GameController").GetComponent<S_GameController>();
        }
        else
        {
            Debug.Log("No Game controller!");
        }

        MachineData = ref_GameController.CharacterController.Interactor.MachineFound.MachineData;
    }

}
