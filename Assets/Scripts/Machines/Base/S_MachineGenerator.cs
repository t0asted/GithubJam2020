using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MachineGenerator : S_MachineBase
{
    private List<CL_ItemConstructable> BuildQueue = null;

    public GameObject BuiltMachine = null;

    private bool building = false;

    public void Process()
    {
        // Get resources on planet
        // if not on planet look in storage

        if (MachineRunning && enum_ItemBuilding() != Enum_Items.None)
        {
            if(BuiltMachine == null)
            {
                if (ref_GameController.GameData.Storage.HasResource(ItemBuilding()))
                {
                    building = true;
                }
            }
            else
            {
                Debug.Log("You need to take your new machine out first!");
            }
        }

        //ref_GameController.GameData.Storage.TakeResources(MachineData.ItemsCanProcess);
    }

    public void SetBuilding()
    {

    }

    public void AddToQueue(CL_ItemConstructable ItemToAdd)
    {
        if (BuildQueue.Count > 0)
        {
            CL_ItemConstructable ItemFound = BuildQueue.Find(f => f.ResourceName == ItemToAdd.ResourceName);

            if(ItemFound != null)
            {
                ItemFound.Quantity += ItemToAdd.Quantity;
            }
            else
            {
                BuildQueue.Add(ItemToAdd);
            }
        }
    }

    private List<CL_Resource> ItemBuilding()
    {
        if (BuildQueue.Count > 0)
        {
            return BuildQueue[0].CostToBuild;
        }
        return null;
    }

    private Enum_Items enum_ItemBuilding()
    {
        if (BuildQueue.Count > 0)
        {
            return BuildQueue[0].ResourceName;
        }
        return Enum_Items.None;
    }

}
