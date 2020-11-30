﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_MachineGatherer : S_MachineBase
{
    public SO_Planet AstroidData = null;
    [SerializeField]
    public List<SO_ListRaw> ItemsCanProcess = new List<SO_ListRaw>();


    private List<CL_PlanetData> PlanetResourcesGatherable = new List<CL_PlanetData>();
    private int levelCalculatedFor = 0; 

    private void Update()
    {
        if(MachineData.Level != levelCalculatedFor && Placed)
        {
            levelCalculatedFor = MachineData.Level;
            PlanetResourcesGatherable = ResourcesCanBeGathered();
            ToggleMachineOnOff(true);
        }
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

    private void RunMachine()
    {
        Process();
    }

    public string GetTextProcessSpeed()
    {
        return "Process Speed : " + MachineData.ProcessSpeed;
    }

    public void Process()
    {
        Debug.Log("Process?");

        if (PlanetResourcesGatherable.Count > 0)
        {
            Debug.Log("YES?");
            foreach (var PlanetResourceToGather in PlanetResourcesGatherable)
            {
                Debug.Log("FOREACH");

                if (PlanetResourceToGather.Rarity > Random.Range(1, 5)) // Rarity chance that you get this resource
                {
                    Debug.Log("Processing");
                    CL_Resource ResourceToAdd = new CL_Resource(PlanetResourceToGather.ResourceName, MachineData.AmountCanProcess);
                    ref_GameController.GameData.AddResource(ResourceToAdd);
                }
            }
        }
        else
        {
            Debug.Log("Resources Can Collect Are Empty!");
        }
    }

    private List<CL_PlanetData> ResourcesCanBeGathered()
    {
        List<CL_PlanetData> resourcesCanBeGathered = new List<CL_PlanetData>();

        for (int i = 0; i < MachineData.Level; i++)
        {
            foreach (var ResourceThatMachineCanCollect in ItemsCanProcess[i].RawList)
            {
                Debug.Log(AstroidData.PlanetData);
                Debug.Log(ItemsCanProcess[i].RawList);
                CL_PlanetData ResourceFound = AstroidData.PlanetData.Find(f => f.ResourceName == ResourceThatMachineCanCollect.ItemData.ResourceName);

                if (ResourceFound != null)
                {
                    resourcesCanBeGathered.Add(ResourceFound);
                }
                // TODO: Get items that can be harvested off planet
            }
        }

        return resourcesCanBeGathered;
    }

}
