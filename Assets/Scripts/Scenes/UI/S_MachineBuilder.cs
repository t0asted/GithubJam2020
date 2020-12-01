using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_MachineBuilder : S_InGameMenuBase
{
    private S_MachineGenerator MachineGenerator = null;

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

    [Header("Machine Take")]
    [SerializeField]
    private TextMeshProUGUI Txt_MachineToTake;
    [SerializeField]
    private Button Btn_TakeMachine;

    [Header("Progress")]
    [SerializeField]
    private Image Image_ProgressBar;

    [Header("Currently building")]
    [SerializeField]
    private S_CurrentlyBuilding CurrentlyBuilding;

    private List<CL_BuildQueue> BuildQueueSpawned = new List<CL_BuildQueue>();
    public List<SO_ListMachine> MachineList = new List<SO_ListMachine>();
    public List<ItemList> ItemList = new List<ItemList>();
    private int Level;

    public override void Start()
    {
        base.Start();

        if (ref_GameController.CharacterController.Interactor.MachineFound is S_MachineGenerator)
        {
            MachineGenerator = ref_GameController.CharacterController.Interactor.MachineFound as S_MachineGenerator;
            SpawnConstructable();
            SpawnInventoryItems();
            SpawnBuildQueue();
        }
    }

    public override void Update()
    {
        base.Update();

        if (MachineGenerator != null)
        {
            if (Txt_MachineToTake != null)
            {
                Txt_MachineToTake.SetText(((S_MachineGenerator)MachineGenerator).BuiltMachine != null ? "Take Machine" : "No Machine to take");
            }

            if (Image_ProgressBar != null)
            {
                Image_ProgressBar.fillAmount = MachineGenerator.ProgressBuilt;
            }

            if (Btn_TakeMachine != null)
            {
                Btn_TakeMachine.interactable = MachineGenerator.BuiltMachine != null;
            }

            if (needToRefreshBuildQueue())
            {
                RefreshBuildQueue();
            }

            if (needToRefreshConstructable())
            {
                RefreshConstructable();
            }

            if (CurrentlyBuilding != null)
            {
                //CurrentlyBuilding.gameObject.SetActive(MachineGenerator.ItemBuilding != null);
                CurrentlyBuilding.SetData(MachineGenerator.ItemBuilding);
            }
        }
    }

    private bool needToRefreshBuildQueue()
    {
        if (BuildQueueSpawned.Count != ((S_MachineGenerator)ref_GameController.CharacterController.Interactor.MachineFound).GetBuildQueue().Count)
        {
            return true;
        }
        for (int i = 0; i < BuildQueueSpawned.Count; i++)
        {
            if (BuildQueueSpawned[i].DataObject.ResourceName != ((S_MachineGenerator)ref_GameController.CharacterController.Interactor.MachineFound).GetBuildQueue()[i].DataObject.ResourceName)
            {
                return true;
            }
        }

        return false;
    }

    private bool needToRefreshConstructable()
    {
        return Level != MachineGenerator.MachineData.Level;
    }

    public void Button_Action_TakeMachine()
    {
        if (MachineGenerator.BuiltMachine != null)
        {
            ref_GameController.CharacterController.m_MachinePlacer.NewMachineToPlace(MachineGenerator.BuiltMachine);
            ref_GameController.CharacterController.Interactor.Interact();
            ((S_MachineGenerator)ref_GameController.CharacterController.Interactor.MachineFound).BuiltMachine = null;
        }
    }

    public void AddToBuildQueue(CL_ItemConstructable itemToAdd)
    {
        if (MachineGenerator != null)
        {
            MachineGenerator.AddToQueue(itemToAdd);
        }
    }

    public void RemoveFromQueue(CL_BuildQueue itemToRemove)
    {
        if (MachineGenerator != null)
        {
            MachineGenerator.RemoveFromQueue(itemToRemove);
        }
    }

    protected void RefreshConstructable()
    {
        if (ConstructableContent != null)
        {
            for (int i = 0; i < ConstructableContent.transform.childCount; i++)
            {
                Destroy(ConstructableContent.transform.GetChild(i).gameObject);
            }
            SpawnConstructable();
        }
    }

    protected void SpawnConstructable()
    {
        if (ConstructableContent != null && ConstructableItemToSpawn != null)
        {
            Level = MachineGenerator.MachineData.Level;
            if (MachineGenerator.ItemsCanGenerate.Count > 0)
            {
                for (int i = 0; i < MachineGenerator.MachineData.Level; i++)
                {
                    ItemList = MachineGenerator.ItemsCanGenerate[i].ItemList;
                    foreach (var ConstItem in ItemList)
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
                    MachineList = new List<SO_ListMachine>(MachineGenerator.MachinesCanGenerate);
                    foreach (var ConstItem in MachineList[i].ItemList)
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

    protected void RefreshBuildQueue()
    {
        if (BuildQueueContent != null)
        {
            for (int i = 0; i < BuildQueueContent.transform.childCount; i++)
            {
                Destroy(BuildQueueContent.transform.GetChild(i).gameObject);
            }
            SpawnBuildQueue();
        }
    }

    protected void SpawnBuildQueue()
    {
        if (BuildQueueContent != null && BuildQueueItemToSpawn != null)
        {
            BuildQueueSpawned = new List<CL_BuildQueue>(((S_MachineGenerator)ref_GameController.CharacterController.Interactor.MachineFound).GetBuildQueue());
            foreach (var invItem in BuildQueueSpawned)
            {
                GameObject invItemSpawn = Instantiate(BuildQueueItemToSpawn, BuildQueueContent.transform);
                if (invItemSpawn.GetComponent<S_BuildQueueItem>())
                {
                    invItemSpawn.GetComponent<S_BuildQueueItem>().SetItem(invItem, this);
                }
            }
        }
    }

}
