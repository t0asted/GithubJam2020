using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CL_Machine
{
    [SerializeField]
    public int ProcessSpeed = 1;
    [SerializeField]
    public int AmountCanProcess = 1;

    public int Level = 1;
    public bool CanLevelUp = true;

    //public CL_Storage ResourcesCollected;

    public CL_Machine()
    {
        //ResourcesCollected = new CL_Storage();
        ProcessSpeed = 1;
        AmountCanProcess = 1;
    }

    //public string ResourceContent()
    //{
    //    string text = "";
    //    if (ResourcesCollected != null)
    //        foreach (var item in ResourcesCollected.ResourceList)
    //            text = text + item.ResourceName + " : " + item.Quantity + "\n";
    //    return text;
    //}

    public void LevelUp()
    {
        if (CanLevelUp)
            Level += 1;
    }

}