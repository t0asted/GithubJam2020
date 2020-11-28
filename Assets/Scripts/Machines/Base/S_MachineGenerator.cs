using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MachineGenerator : S_MachineBase
{
    public SO_ItemList ItemsCanGenerate = new SO_ItemList();

    private SO_Part PartToBuild;
    private SO_Item ItemToBuild;

    public void Process()
    {
        // Get resources on planet
        // if not on planet look in storage

        if(MachineRunning)
        {
            if (ref_GameController.GameData.Storage.HasResource(ItemBuilding()))
            {

            }
        }

        //ref_GameController.GameData.Storage.TakeResources(MachineData.ItemsCanProcess);
    }

    private List<CL_Resource> ItemBuilding()
    {
        if (PartToBuild != null)
        {
            return PartToBuild.CostToBuild;
        }
        if (ItemToBuild != null)
        {
            return ItemToBuild.CostToBuild;
        }
        return null;
    }

    private Enum_Items enum_ItemBuilding()
    {
        if (PartToBuild != null)
        {
            return PartToBuild.ResourceName;
        }
        if (ItemToBuild != null)
        {
            return ItemToBuild.ResourceName;
        }
        return Enum_Items.None;
    }

}
