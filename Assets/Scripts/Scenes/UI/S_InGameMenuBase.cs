using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Build Queue")]
    [SerializeField]
    private GameObject BuildQueueContent;
    [SerializeField]
    private GameObject BuildQueueItemToSpawn;

    [Header("Texts")]
    [SerializeField]
    private TextMeshProUGUI Txt_MachineToTake;
    [SerializeField]
    private TextMeshProUGUI Txt_MachineOnOff;
    [SerializeField]
    private TextMeshProUGUI Txt_MachineMessage;

    [Header("Buttons")]
    [SerializeField]
    private Button Button_TakeMachine;

    public void Start()
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
            if(ref_GameController.CharacterController.Interactor.MachineFound is S_MachineGenerator)
            {
                MachineGenerator = ref_GameController.CharacterController.Interactor.MachineFound as S_MachineGenerator;
            }
        }
    }

    private void Update()
    {
        if (MachineGenerator != null && Button_TakeMachine != null)
        {
            SetTexts();
            Button_TakeMachine.interactable = MachineGenerator.BuiltMachine != null;
        }
    }

    private void SetTexts()
    {
        if (Txt_MachineOnOff != null)
        {
            Txt_MachineOnOff.SetText(MachineGenerator.MachineRunning ? "Machine is running" : "Machine is off");
        }
        if (Txt_MachineToTake != null)
        {
            Txt_MachineToTake.SetText(((S_MachineGenerator)MachineGenerator).BuiltMachine != null ? "Take Machine" : "No Machine to take");
        }
        if(Txt_MachineMessage != null)
        {
            Txt_MachineMessage.SetText(MachineGenerator.Message);
        }
    }

    public void Btn_TurnOnOff()
    {
        if (MachineGenerator != null)
        {
            MachineGenerator.ToggleRunning();
        }
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

    public void AddToBuildQueue(CL_ItemConstructable itemToAdd)
    {
        if (MachineGenerator != null)
        {
            MachineGenerator.AddToQueue(itemToAdd);
        }
    }

    public void SpawnInventoryItems()
    {
        if (inventoryContent != null && inventoryItemToSpawn != null)
        {
            foreach (var invItem in ref_GameController.GameData.Storage.ResourceList)
            {
                GameObject invItemSpawn = Instantiate(inventoryItemToSpawn, inventoryContent.transform);
                if (invItemSpawn.GetComponent<S_InventoryItem>())
                {
                    invItemSpawn.GetComponent<S_InventoryItem>().SetItem(invItem);
                }
            }
        }
    }

    public void SpawnBuildQueue()
    {
        if (BuildQueueContent != null && BuildQueueItemToSpawn != null)
        {
            foreach (var invItem in ((S_MachineGenerator)ref_GameController.CharacterController.Interactor.MachineFound).BuildQueue)
            {
                GameObject invItemSpawn = Instantiate(BuildQueueItemToSpawn, BuildQueueContent.transform);
                if (invItemSpawn.GetComponent<S_BuildQueueItem>())
                {
                    invItemSpawn.GetComponent<S_BuildQueueItem>().SetItem(invItem);
                }
            }
        }
    }

    public void SpawnConstructable()
    {
        if (ConstructableContent != null && ConstructableItemToSpawn != null)
        {
            if (MachineGenerator.ItemsCanGenerate.Count > 0)
            {
                for (int i = 0; i < MachineGenerator.MachineData.Level; i++)
                {
                    foreach (var ConstItem in MachineGenerator.ItemsCanGenerate[i].ItemList)
                    {
                        GameObject invItemSpawn = Instantiate(ConstructableItemToSpawn, ConstructableContent.transform);
                        if (invItemSpawn.GetComponent<S_ConstructableItem>())
                        {
                            invItemSpawn.GetComponent<S_ConstructableItem>().SetupContructableItem(ConstItem.ItemData, this);
                        }
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
                        if (invItemSpawn.GetComponent<S_ConstructableItem>())
                        {
                            invItemSpawn.GetComponent<S_ConstructableItem>().SetupContructableItem(ConstItem.ItemData, this);
                        }
                    }
                }
            }
        }
    }

}
