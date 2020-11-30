using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Item/Items To Repair")]
public class SO_ItemsToFixRocket : ScriptableObject
{
    public List<ItemList> ItemList = new List<ItemList>();
}

[System.Serializable]
public class CL_ItemToFixRocket
{
    public int Quanity;
    public SO_Item ItemData;
}