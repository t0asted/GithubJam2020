using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class S_RocketUI : S_MachineBuilder
{
    [SerializeField]
    private GameObject RocketItemsToFix;
    [SerializeField]
    private GameObject RocketItemToFixSpawn;

    [SerializeField]
    private TextMeshProUGUI m_TextItemsToAdd;

    private S_Rocket Rocket = null;
    private List<ItemList> ItemsToFixSpawned = new List<ItemList>();

    public override void Update()
    {
        base.Update();

        if (needToRefreshPartsToFix())
        {
            RefreshPartsToFix();
        }
        m_TextItemsToAdd.SetText(Rocket.TextToAddcomponent);
    }

    public override void Start()
    {
        base.Start();
        if (ref_Machine is S_Rocket)
        {
            Rocket = ref_Machine as S_Rocket;
        }
    }

    public void DebugAddMachine(GameObject MachineTest)
    {
        Rocket.BuiltMachine = MachineTest;
    }

    public void Button_Action_AddAvailbleItemsToFix()
    {
        Rocket.AddAllAvailbleParts();
    }

    private bool needToRefreshPartsToFix()
    {
        if (ItemsToFixSpawned.Count != Rocket.ItemsStillToFix.Count)
        {
            return true;
        }
        for (int i = 0; i < Rocket.ItemsStillToFix.Count; i++)
        {
            if (ItemsToFixSpawned[i].ItemData.ResourceName != Rocket.ItemsStillToFix[i].ItemData.ResourceName)
            {
                return true;
            }
        }
        return false;
    }

    protected void RefreshPartsToFix()
    {
        if (RocketItemsToFix != null)
        {
            for (int i = 0; i < RocketItemsToFix.transform.childCount; i++)
            {
                Destroy(RocketItemsToFix.transform.GetChild(i).gameObject);
            }
            SpawnPartsToFix();
        }
    }

    protected void SpawnPartsToFix()
    {
        Debug.Log("Spawning!");
        if (RocketItemsToFix != null && RocketItemToFixSpawn != null)
        {
            ItemsToFixSpawned = new List<ItemList>(Rocket.ItemsStillToFix);
            if (ItemsToFixSpawned.Count > 0)
            {
                foreach (var Item in ItemsToFixSpawned)
                {
                    ItemList PartFound = Rocket.ItemsFixed.Find(f => f.ItemData.ResourceName == Item.ItemData.ResourceName);

                    if(PartFound == null)
                    {
                        GameObject invItemSpawn = Instantiate(RocketItemToFixSpawn, RocketItemsToFix.transform);
                        if (invItemSpawn.GetComponent<S_InventoryItem>())
                        {
                            invItemSpawn.GetComponent<S_InventoryItem>().SetItem(new CL_Resource(Item.ItemData));
                        }
                    }
                }
            }
        }
    }

}
