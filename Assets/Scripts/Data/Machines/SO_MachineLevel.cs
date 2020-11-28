using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item/Machine Level Data")]
public class CL_MachineLevel : ScriptableObject
{
    public int Level = 1;
    public SO_ItemList itemList;
}
