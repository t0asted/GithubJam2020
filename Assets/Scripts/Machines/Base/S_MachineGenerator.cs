using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_MachineGenerator : S_MachineBase
{
    [SerializeField]
    public List<SO_ListItem> ItemsCanGenerate;
    [SerializeField]
    public List<SO_ListMachine> MachinesCanGenerate;
    [SerializeField]
    private Button Button_TakeMachine;

    public CL_BuildQueue ItemBuilding = null;
    private List<CL_BuildQueue> BuildQueue = new List<CL_BuildQueue>();
    public GameObject BuiltMachine = null;
    private bool building = false;

    public float ProgressBuilt = 0;

    private DateTime StartedBuilding;
    private DateTime EndBuilding;

    public override void Update()
    {
        if(building)
        {
            ProgressBuilt = (float)((DateTime.Now - StartedBuilding).TotalSeconds * 100 / (EndBuilding - StartedBuilding).TotalSeconds) / 100;
        }

        if (TextToShowContent != null)
        {
            TextToShowContent.text = MachineRunning ?  "Machine running" : "Machine is off";
        }

        if (!building && MachineRunning)
        {
            if (BuiltMachine == null)
            {
                if (BuildQueue.Count > 0)
                {
                    if (ref_GameController.GameData.Storage.HasResource(BuildQueue[0].DataObject.CostToBuild))
                    {
                        Message = "Building";
                        StartCoroutine(ConstructAfterTime());
                    }
                    else
                        Message = "Not enough resources";
                }
                else
                    Message = "Nothing in build queue";
            }
            else
                Message = "You need to take your new machine out first";
        }
    }

    public List<CL_BuildQueue> GetBuildQueue()
    {
        return BuildQueue;
    }

    private IEnumerator ConstructAfterTime()
    {
        StartedBuilding = DateTime.Now;

        ItemBuilding = GetNextItemInBuildQueue();
        ref_GameController.GameData.Storage.TakeResources(ItemBuilding.DataObject.CostToBuild);

        EndBuilding = StartedBuilding.AddSeconds((int)ItemBuilding.DataObject.TimeToBuild);
        if (building)
            yield break;

        building = true;

        yield return new WaitForSeconds((int)ItemBuilding.DataObject.TimeToBuild);

        if (ItemBuilding.DataObject is SO_Machine)
        {
            BuiltMachine = ((SO_Machine)ItemBuilding.DataObject).PrefabForMachine;
        }
        else
        {
            ref_GameController.GameData.Storage.AddResources(new CL_Resource(ItemBuilding.DataObject));
        }

        ItemBuilding = null;
        building = false;
    }

    public CL_BuildQueue GetNextItemInBuildQueue()
    {
        if(BuildQueue.Count > 0)
        {
            CL_BuildQueue BuildQueueToReturn = new CL_BuildQueue(BuildQueue[0].DataObject, 1);
            BuildQueue[0].Quantity -= 1;
            if (BuildQueue[0].Quantity <= 0)
            {
                BuildQueue.RemoveAt(0);
            }
            return BuildQueueToReturn;
        }
        return null;
    }

    public void AddToQueue(CL_ItemConstructable ItemToAdd)
    {
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

    public void RemoveFromQueue(CL_BuildQueue itemToRemove)
    {
        CL_BuildQueue ItemFound = BuildQueue.Find(f => f.DataObject.ResourceName == itemToRemove.DataObject.ResourceName);

        if(ItemFound != null)
        {
            ItemFound.Quantity = ItemFound.Quantity - 1;
            if(ItemFound.Quantity <= 0)
            {
                BuildQueue.Remove(ItemFound);
            }
        }
    }

}

[System.Serializable]
public class CL_BuildQueue
{
    public CL_ItemConstructable DataObject;
    public int Quantity;

    public CL_BuildQueue(CL_BuildQueue dataPass)
    {
        DataObject = dataPass.DataObject;
        Quantity = dataPass.Quantity;
    }

    public CL_BuildQueue(CL_ItemConstructable DataObjectPass, int QuantityPass)
    {
        DataObject = DataObjectPass;
        Quantity = QuantityPass;
    }

}