﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_InGameMenuBase : S_SceneUIMain
{
    public S_GameController ref_GameController = null;
    public S_MachineGenerator MachineGenerator = null;

    [Header("Inventory")]
    [SerializeField]
    private GameObject inventoryContent;
    [SerializeField]
    private GameObject inventoryItemToSpawn;

    [Header("Constructable")]
    [SerializeField]
    private GameObject ConstructableContent;
    [SerializeField]
    private GameObject ConstructableItemToSpawn;

    [Header("Texts")]
    [SerializeField]
    public TextMeshProUGUI TestText;
    [SerializeField]
    public TextMeshProUGUI MachineOnOff;

    public void Btn_TurnOnOff()
    {
        MachineGenerator.ToggleRunning();
    }

    public void Btn_TakeMachine()
    {
        if (MachineGenerator.BuiltMachine != null)
        {
            ref_GameController.CharacterController.m_MachinePlacer.NewMachineToPlace(MachineGenerator.BuiltMachine);
            ref_GameController.CharacterController.Interactor.Interact();
            ((S_MachineGenerator)ref_GameController.CharacterController.Interactor.MachineFound).BuiltMachine = null;
        }
        else
        {
            Debug.Log("No Machine!");
        }
    }

    public void SpawnInventoryItems()
    {
        foreach (var invItem in ref_GameController.GameData.Storage.ResourceList)
        {
            GameObject invItemSpawn = Instantiate(inventoryItemToSpawn, inventoryContent.transform);
            invItemSpawn.GetComponent<S_InventoryItem>().SetItem(invItem);
        }
    }

    public void AddToBuildQueue(CL_ItemConstructable itemToAdd)
    {
        MachineGenerator.AddToQueue(itemToAdd);
    }

    public void SpawnConstructable()
    {
        if (MachineGenerator.ItemsCanGenerate.Count > 0)
        {
            for (int i = 0; i < MachineGenerator.MachineData.Level; i++)
            {
                foreach (var ConstItem in MachineGenerator.ItemsCanGenerate[i].ItemList)
                {
                    GameObject invItemSpawn = Instantiate(ConstructableItemToSpawn, ConstructableContent.transform);
                    invItemSpawn.GetComponent<S_ConstructableItem>().SetupContructableItem(ConstItem.ItemData, this);
                }
            }
        }

        if (MachineGenerator.MachinesCanGenerate.Count > 0)
        {
            for (int i = 0; i < MachineGenerator.MachineData.Level; i++)
            {
                foreach (var ConstItem in MachineGenerator.MachinesCanGenerate[i].ItemList)
                {
                    GameObject invItemSpawn = Instantiate(ConstructableItemToSpawn, ConstructableContent.transform);
                    invItemSpawn.GetComponent<S_ConstructableItem>().SetupContructableItem(ConstItem.ItemData, this);
                }
            }
        }
    }

}
