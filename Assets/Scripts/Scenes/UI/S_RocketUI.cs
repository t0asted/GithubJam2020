using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_RocketUI : S_InGameMenuBase
{
    private S_Rocket Rocket = null;

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

        if (ref_GameController.CharacterController.Interactor.MachineFound != null)
        {
            //MachineData = ref_GameController.CharacterController.Interactor.MachineFound;
            MachineGenerator = (S_MachineGenerator)ref_GameController.CharacterController.Interactor.MachineFound;
            if (MachineGenerator is S_Rocket)
            {
                Rocket = (S_Rocket)MachineGenerator;
            }
            SetupData();
        }
    }

    public void DebugAddMachine(GameObject MachineTest)
    {
        Rocket.BuiltMachine = MachineTest;
    }

    private void Update()
    {
        if (Rocket != null)
        {
            SetTexts();
        }
    }

    private void SetTexts()
    {
        if (MachineOnOff != null)
        {
            MachineOnOff.SetText(Rocket.MachineRunning ? "Rocket is running" : "Rocket is off");
        }
    }

    private void SetupData()
    {
        SpawnConstructable();
        SpawnInventoryItems();
    }

    public void Btn_AddAvailbleItemsToFix()
    {
        //TODO
    }

}
