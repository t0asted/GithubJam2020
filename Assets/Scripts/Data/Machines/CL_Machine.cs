using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CL_Machine 
{
    [SerializeField]
    public SO_ItemList ItemsCanProcess = new SO_ItemList();
    [SerializeField]
    public int ProcessSpeed = 1;
    [SerializeField]
    public int AmountCanProcess = 1;

    private CL_Storage ResourcesCollected;


    public CL_Machine()
    {
        ResourcesCollected = new CL_Storage();
        ProcessSpeed = 1;
        AmountCanProcess = 1;
    }
    
    

    public string ResourceContent()
    {
        string text = "Hello!";
        if(ResourcesCollected != null)
            foreach (var item in ResourcesCollected.ResourceList)
                text = text + item.ResourceName + " : " + item.Quantity + "\n";
        return text;
    }

}
