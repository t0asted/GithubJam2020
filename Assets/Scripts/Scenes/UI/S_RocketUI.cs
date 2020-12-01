using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_RocketUI : S_MachineBuilder
{
    private S_Rocket Rocket = null;

    public override void Start()
    {
        base.Start();
        if (ref_Machine is S_Rocket)
        {
            Rocket = ref_Machine as S_Rocket;
        }
    }

    public void DebugAddMachine(GameObject MachineTest)
    {
        Rocket.BuiltMachine = MachineTest;
    }

    public void Button_Action_AddAvailbleItemsToFix()
    {
        //TODO
    }

}
