using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_RocketUI : S_InGameMenuBase
{
    private S_Rocket Rocket = null;

    new void Start()
    {
        base.Start();
        if (MachineGenerator is S_Rocket)
        {
            Rocket = (S_Rocket)MachineGenerator;
        }
        SetupData();
    }

    private void SetupData()
    {
        SpawnConstructable();
        SpawnInventoryItems();
        SpawnBuildQueue();
    }

    public void DebugAddMachine(GameObject MachineTest)
    {
        Rocket.BuiltMachine = MachineTest;
    }

    public void Btn_AddAvailbleItemsToFix()
    {
        //TODO
    }

}
