﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Rocket : S_MachineGenerator
{
    public SO_ListItem ToFixRocket;
    private CL_Storage ItemsAddedToShip = new CL_Storage();

    public override void Start()
    {
        base.Start();
        foreach (Collider c in GetComponents<Collider>())
        {
            c.enabled = true;
        }
    }

    public bool IsShipFixed()
    {
        foreach (var item in ToFixRocket.ItemList)
        {
            CL_Resource ResourceFound = ItemsAddedToShip.ResourceList.Find(f => f.ResourceName == item.ItemData.ResourceName);
            if (ResourceFound == null)
            {
                return false;
            }
        }
        //foreach (var item in ToFixRocket.PartList)
        //{
        //    CL_Resource ResourceFound = ItemsAddedToShip.ResourceList.Find(f => f.ResourceName == item.ItemData.ResourceName);
        //    if (ResourceFound == null)
        //    {
        //        return false;
        //    }
        //}
        //foreach (var item in ToFixRocket.RawList)
        //{
        //    CL_Resource ResourceFound = ItemsAddedToShip.ResourceList.Find(f => f.ResourceName == item.ItemData.ResourceName);
        //    if (ResourceFound == null)
        //    {
        //        return false;
        //    }
        //}
        return true;
    }

    public void AddPartToShip(CL_Resource resourceToAdd)
    {
        ItemsAddedToShip.AddResources(resourceToAdd);
    }

}
