using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Item/List")]
public class SO_ItemList : ScriptableObject
{
    public List<SO_Raw> RawList = new List<SO_Raw>();
    public List<SO_Item> ItemList = new List<SO_Item>();
    public List<SO_Part> PartList = new List<SO_Part>();

    public bool HasItems()
    {
        if(RawList.Count > 0)
        {
            return true;
        }
        if (ItemList.Count > 0)
        {
            return true;
        }
        if (PartList.Count > 0)
        {
            return true;
        }
        return false;
    }
}