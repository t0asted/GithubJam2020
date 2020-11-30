using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item/Machine List")]
public class SO_ListMachine : ScriptableObject
{
    public List<MachineList> ItemList = new List<MachineList>();
}

[System.Serializable]
public class MachineList
{
    public SO_Machine ItemData;
}