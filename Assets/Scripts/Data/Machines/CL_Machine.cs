using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

[System.Serializable]
public class CL_Machine
{
    public CL_ResourcesScriptable CostToBuild { get; }
    public CL_ResourcesScriptable ResourceCanCollect { get; }
    public CL_Resources ResourcesCollected { get; }
    public int ProcessSpeed { get; }
    public int AmountCanProcess { get; }

    public CL_Machine()
    {
        ProcessSpeed = 1;
        AmountCanProcess = 1;
    }
    
    public void Process()
    {
        foreach (var ResourceInList in ResourceCanCollect.ResourceList)
        {
            if (ResourceInList.rarity > Random.Range(1, 5)) // Rarity chance that you get this resource
            {
                CL_Resource ResourceSetQuantity = new CL_Resource(ResourceInList.ResourceName, Random.Range(1, AmountCanProcess));
                ResourcesCollected.AddResources(ResourceSetQuantity);
            }
        }
    }

    public string ResourceContent()
    {
        string text = "";
        foreach (var item in ResourcesCollected.ResourceList)
        {
            text = text + item.ResourceName + " : " + item.Quantity + "\n";
        }
        return text;
    }

}
