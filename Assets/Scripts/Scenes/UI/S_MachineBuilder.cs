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

        if (Txt_MachineToTake != null)
        {
            Txt_MachineToTake.SetText(((S_MachineGenerator)MachineGenerator).BuiltMachine != null ? "Take Machine" : "No Machine to take");
        }

        if(Image_ProgressBar != null)
        {
            Image_ProgressBar.fillAmount = MachineGenerator.ProgressBuilt;
        }

        if(Btn_TakeMachine != null)
        {
            Btn_TakeMachine.interactable = MachineGenerator.BuiltMachine != null;
        }
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

}
