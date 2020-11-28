using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Item/List")]
public class SO_ItemList : ScriptableObject
{
    public List<RawList> RawList = new List<RawList>();
    public List<ItemList> ItemList = new List<ItemList>();
    public List<PartList> PartList = new List<PartList>();
    public List<MachineList> MachineList = new List<MachineList>();

    public bool HasItems()
    {
        if(RawList.Count > 0 || ItemList.Count > 0 || PartList.Count > 0 || MachineList.Count > 0)
        {
            return true;
        }
        return false;
    }
}

[System.Serializable]
public class RawList
{
    public int Quantity;
    public SO_Raw ItemData;
}
[System.Serializable]
public class ItemList
{
    public int Quantity;
    public SO_Item ItemData;
}
[System.Serializable]
public class PartList
{
    public int Quantity;
    public SO_Part ItemData;
}
[System.Serializable]
public class MachineList
{
    public int Quantity;
    public SO_Machine ItemData;
}