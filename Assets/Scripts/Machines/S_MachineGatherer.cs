using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_MachineGatherer : S_MachineBase
{
    [SerializeField]
    public TextMeshProUGUI TextToShowContent;

    private void Start()
    {
        ToggleMachineOnOff(true);
        if (GameObject.Find("_GameController") != null)
        {
            ref_GameController = GameObject.Find("_GameController").GetComponent<S_GameController>();
            ref_GameController.GameData.GameMode = GameModes.DebugJoshua;
        }
    }

    private void Update()
    {
        TextToShowContent.text = MachineRunning ? "Machine running" : "Machine is off";
    }

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
