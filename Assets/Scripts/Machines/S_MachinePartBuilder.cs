using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MachinePartBuilder : S_MachineBase
{
    public List<SO_Part> PartsCanBuild = new List<SO_Part>();
    public List<SO_Item> ItemsCanBuild = new List<SO_Item>();

    public List<SO_Part> PartsInBuildQueue = new List<SO_Part>();
    public List<SO_Item> ItemsInBuildQueue = new List<SO_Item>();

    public void RunMachine()
    {
        Process();
    }

    public void Process()
    {
        if (MachineData.ItemsCanProcess.RawList.Count > 0)
        {
            foreach (var ResourceInList in MachineData.ItemsCanProcess.RawList)
            {
                // TODO: Get items that can be harvested off planet
                if (ResourceInList.Rarity > Random.Range(1, 5)) // Rarity chance that you get this resource
                {
                    CL_Resource ResourceToAdd = new CL_Resource(ResourceInList.ResourceName, MachineData.AmountCanProcess);
                    ref_GameController.GameData.AddResource(ResourceToAdd);
                }
            }
        }
        else
        {
            Debug.Log("Resources Can Collect Are Empty!");
        }
    }

}
