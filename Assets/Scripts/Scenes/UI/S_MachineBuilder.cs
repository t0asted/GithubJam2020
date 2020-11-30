using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MachineBuilder : S_InGameMenuBase
{
    new void Start()
    {
        base.Start();
        
        SetupData();
    }

    private void SetupData()
    {
        SpawnConstructable();
        SpawnInventoryItems();
        SpawnBuildQueue();
    }
}
