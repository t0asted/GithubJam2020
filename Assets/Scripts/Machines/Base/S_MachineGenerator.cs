using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MachineGenerator : S_MachineBase
{
    [SerializeField]
    public List<SO_ListItem> ItemsCanGenerate;
    [SerializeField]
    public List<SO_ListMachine> MachinesCanGenerate;

    public List<CL_BuildQueue> BuildQueue = new List<CL_BuildQueue>();
    public GameObject BuiltMachine = null;
    private bool building = false;

    private void LateUpdate()
    {
        if (!building && MachineRunning)
        {
            if (BuiltMachine == null)
            {
                if (BuildQueue.Count > 0)
                {
                    if (ref_GameController.GameData.Storage.HasResource(BuildQueue[0].DataObject.CostToBuild))
                    {
                        ref_GameController.GameData.Storage.TakeResources(BuildQueue[0].DataObject.CostToBuild);
                        StartCoroutine(ConstructAfterTime(BuildQueue[0]));
                    }
                    else
                        Debug.Log("Not enough resources");
                }
                else
                    Debug.Log("Nothing in build queue");
            }
            else
                Debug.Log("You need to take your new machine out first");
        }
    }

    IEnumerator ConstructAfterTime(CL_BuildQueue ItemToConstruct)
    {
        if (building)
            yield break;

        building = true;

        yield return new WaitForSeconds((int)ItemToConstruct.DataObject.TimeToBuild);

        SO_Machine MachineFound = (SO_Machine)ItemToConstruct.DataObject;
        if (MachineFound != null)
        {
            BuiltMachine = MachineFound.PrefabForMachine;
        }
        else
        {
            ref_GameController.GameData.Storage.AddResources(new CL_Resource(BuildQueue[0].DataObject.ResourceName, BuildQueue[0].Quantity));
        }

        BuildQueue[0].Quantity -= 1;
        if (BuildQueue[0].Quantity <= 0)
        {
            BuildQueue.RemoveAt(0);
        }

        building = false;
    }

    public void AddToQueue(CL_ItemConstructable ItemToAdd)
    {
        Debug.Log("Adding to Queue");
        if (BuildQueue != null)
        {
            CL_BuildQueue ItemFound = BuildQueue.Find(f => f.DataObject.ResourceName == ItemToAdd.ResourceName);

            if (ItemFound != null)
            {
                ItemFound.Quantity += 1;
            }
            else
            {
                BuildQueue.Add(new CL_BuildQueue(ItemToAdd, 1));
            }
        }
    }
}

[System.Serializable]
public class CL_BuildQueue
{
    public CL_ItemConstructable DataObject;
    public int Quantity;

    public CL_BuildQueue(CL_ItemConstructable DataObjectPass, int QuantityPass)
    {
        DataObject = DataObjectPass;
        Quantity = QuantityPass;
    }
}