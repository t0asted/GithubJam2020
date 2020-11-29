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
    public int Rarity;
    public int Quantity;
    public SO_Item ItemData;
}