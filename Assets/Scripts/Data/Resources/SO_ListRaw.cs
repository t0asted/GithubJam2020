using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Item/Raw List")]
public class SO_ListRaw : ScriptableObject
{
    public List<RawList> RawList = new List<RawList>();
}


[System.Serializable]
public class RawList
{
    public int Quantity;
    public SO_Raw ItemData;
}