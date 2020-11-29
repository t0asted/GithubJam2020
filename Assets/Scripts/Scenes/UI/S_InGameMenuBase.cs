using System.Collections;
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
        }
        else
        {
            Debug.Log("No Machine!");
        }
    }

    public void SpawnInventoryItems()
    {
        foreach (var invItem in MachineGenerator.MachineData.ResourcesCollected.ResourceList)
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
        if (MachineGenerator.MachineData.ItemsCanProcess.Count > 0)
        {
            for (int i = 0; i < MachineGenerator.MachineData.Level; i++)
            {
                foreach (var ConstItem in MachineGenerator.MachineData.ItemsCanProcess[i].ItemList)
                {
                    GameObject invItemSpawn = Instantiate(ConstructableItemToSpawn, ConstructableContent.transform);
                    invItemSpawn.GetComponent<S_ConstructableItem>().SetupContructableItem((CL_ItemConstructable)ConstItem.ItemData, this);
                }
                foreach (var ConstItem in MachineGenerator.MachineData.ItemsCanProcess[i].MachineList)
                {
                    GameObject invItemSpawn = Instantiate(ConstructableItemToSpawn, ConstructableContent.transform);
                    invItemSpawn.GetComponent<S_ConstructableItem>().SetupContructableItem((CL_ItemConstructable)ConstItem.ItemData, this);
                }
                foreach (var ConstItem in MachineGenerator.MachineData.ItemsCanProcess[i].PartList)
                {
                    GameObject invItemSpawn = Instantiate(ConstructableItemToSpawn, ConstructableContent.transform);
                    invItemSpawn.GetComponent<S_ConstructableItem>().SetupContructableItem((CL_ItemConstructable)ConstItem.ItemData, this);
                }
            }
        }
    }

}
