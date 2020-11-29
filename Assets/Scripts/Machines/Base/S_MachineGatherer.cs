using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_MachineGatherer : S_MachineBase
{
    [SerializeField]
    public List<SO_ListRaw> ItemsCanProcess = new List<SO_ListRaw>();
    private void ToggleMachineOnOff()
    {
        if (MachineRunning)
        {
            InvokeRepeating("RunMachine", 1f, 1f);
            MachineRunning = !MachineRunning;
        }
        else
        {
            CancelInvoke("RunMachine");
            MachineRunning = !MachineRunning;
        }
    }

    private void ToggleMachineOnOff(bool TurnOnOff)
    {
        if (TurnOnOff)
        {
            InvokeRepeating("RunMachine", 1f, 1f);
            MachineRunning = TurnOnOff;
        }
        else
        {
            CancelInvoke("RunMachine");
            MachineRunning = TurnOnOff;
        }
    }

    public void RunMachine()
    {
        Process();
    }

    public string GetTextProcessSpeed()
    {
        return "Process Speed : " + MachineData.ProcessSpeed;
    }

    public void Process()
    {
        if (ItemsCanProcess[0].RawList.Count > 0)
        {
            foreach (var ResourceInList in ItemsCanProcess[0].RawList)
            {
                // TODO: Get items that can be harvested off planet
                //if (ResourceInList.ItemData.Rarity > Random.Range(1, 5)) // Rarity chance that you get this resource
                //{
                //    
                //}
                CL_Resource ResourceToAdd = new CL_Resource(ResourceInList.ItemData.ResourceName, MachineData.AmountCanProcess);
                ref_GameController.GameData.AddResource(ResourceToAdd);
            }
        }
        else
        {
            Debug.Log("Resources Can Collect Are Empty!");
        }
    }

}
