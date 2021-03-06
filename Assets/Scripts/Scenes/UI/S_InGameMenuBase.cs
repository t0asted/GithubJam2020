﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_InGameMenuBase : MonoBehaviour
{
    protected S_GameController ref_GameController = null;
    protected S_MachineBase ref_Machine = null;

    [Header("Inventory")]
    [SerializeField]
    private GameObject inventoryContent;
    [SerializeField]
    private GameObject inventoryItemToSpawn;

    [Header("Texts")]
    [SerializeField]
    private TextMeshProUGUI Txt_MachineOnOff;
    [SerializeField]
    private TextMeshProUGUI Txt_MachineMessage;
    [SerializeField]
    private TextMeshProUGUI Txt_Levelup;

    [Header("Buttons")]
    [SerializeField]
    private Button Btn_Levelup;

    private List<CL_Resource> InventorySpawned;

    public virtual void Start()
    {
        if (GameObject.Find("_GameController") != null)
        {
            ref_GameController = GameObject.Find("_GameController").GetComponent<S_GameController>();
        }
        else
        {
            Debug.Log("No Game controller!");
        }

        if (ref_GameController.CharacterController.Interactor.MachineFound != null)
        {
            ref_Machine = ref_GameController.CharacterController.Interactor.MachineFound;
        }
    }

    public virtual void Update()
    {
        if (ref_Machine != null)
        {
            if (Btn_Levelup != null)
            {
                Btn_Levelup.interactable = ref_Machine.CanLevelUp();
                if (Txt_Levelup != null)
                {
                    Txt_Levelup.SetText(ref_Machine.CanLevelUp() ? "Level Up!" : "Can't level up");
                }
            }
            if (Txt_MachineOnOff != null)
            {
                Txt_MachineOnOff.SetText(ref_Machine.MachineRunning ? "Machine is running" : "Machine is off");
            }
            if (Txt_MachineMessage != null)
            {
                Txt_MachineMessage.SetText(ref_Machine.Message);
            }

            if(inventoryContent != null && inventoryItemToSpawn != null)
            {
                if (ShouldRefreshInventory())
                {
                    RefreshInventory();
                }
            }
        }
    }

    public bool HasResourcesToMakeItem(List<CL_Resource> ResourcesPass)
    {
        return ref_GameController.GameData.Storage.HasResource(ResourcesPass);
    }

    public void Button_Action_LevelUp()
    {
        ref_Machine.LevelUp();
    }

    public void Button_Action_TurnOnOff()
    {
        if (ref_Machine != null)
        {
            ref_Machine.ToggleRunning();
        }
    }

    public bool ShouldRefreshInventory()
    {
        if (InventorySpawned == null) return false; 

        if (InventorySpawned.Count != ref_GameController.GameData.Storage.ResourceList.Count)
        {
            return true;
        }
        for (int i = 0; i < InventorySpawned.Count; i++)
        {
            if (InventorySpawned[i].ResourceName != ref_GameController.GetResourceList()[i].ResourceName)
            {
                return true;
            }
        }
        return false;
    }

    public void RefreshInventory()
    {
        if (inventoryContent != null)
        {
            for (int i = 0; i < inventoryContent.transform.childCount; i++)
            {
                Destroy(inventoryContent.transform.GetChild(i).gameObject);
            }
            SpawnInventoryItems();
        }
    }

    public void SpawnInventoryItems()
    {
        if (inventoryContent != null && inventoryItemToSpawn != null)
        {
            InventorySpawned = new List<CL_Resource>(ref_GameController.GetResourceList());
            foreach (var invItem in InventorySpawned)
            {
                GameObject invItemSpawn = Instantiate(inventoryItemToSpawn, inventoryContent.transform);
                if (invItemSpawn.GetComponent<S_InventoryItem>())
                {
                    invItemSpawn.GetComponent<S_InventoryItem>().SetItem(invItem);
                }
            }
        }
    }

}
