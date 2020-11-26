using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MachineGenerator : S_MachineBase
{
    public SO_ItemList ItemsCanGenerate = new SO_ItemList();

    public bool UseItemsInStorage = true;

    public void Process()
    {
        // Get resources on planet
        // if not on planet look in storage

        if(UseItemsInStorage)
        {
             //Enough Materials

             //ref_GameController.GameData.Storage.
        }
        else
        {

        }
    }

}
 