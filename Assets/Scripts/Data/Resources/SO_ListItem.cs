using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Item/List")]
public class SO_ListItem : ScriptableObject
{
    public List<ItemList> ItemList = new List<ItemList>();
}

[System.Serializable]
public class ItemList
{
    public SO_Item ItemData;
}