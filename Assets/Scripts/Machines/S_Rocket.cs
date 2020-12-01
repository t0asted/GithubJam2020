using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Rocket : S_MachineGenerator
{
    public SO_ListItem ToFixRocket;
    public List<ItemList> ItemsStillToFix = new List<ItemList>();
    public List<ItemList> ItemsFixed = new List<ItemList>();

    public string TextToAddcomponent = "Add all availble components!";

    public override void Start()
    {
        base.Start();
        foreach (Collider c in GetComponents<Collider>())
        {
            c.enabled = true;
        }
        ItemsStillToFix = ToFixRocket.ItemList;
    }

    public void IsShipFixed()
    {
        if(ItemsFixed.Count >= 5)
        {
            ref_GameController.EndGame();
        }
    }

    public void AddAllAvailbleParts()
    {
        if(HasAnyResource())
        {
            TextToAddcomponent = "You added a part to fix the ship";
        }
        else
        {
            TextToAddcomponent = "You didn't have any items to fix ship";
        }
        IsShipFixed();
    }

    public bool HasAnyResource()
    {
        bool DidFindSomethingToAdd = false;
        foreach (var item in ItemsStillToFix)
        {
            if (ref_GameController.GameData.Storage.HasResource(new CL_Resource(item.ItemData)))
            {
                ref_GameController.GameData.Storage.TakeResources(new CL_Resource(item.ItemData));
                ItemsFixed.Add(item);
                DidFindSomethingToAdd = true;
            }
        }
        return DidFindSomethingToAdd;
    }

}
