using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Resource")]
public class CL_ResourceScriptable : ScriptableObject
{
    public Enum_Resource ResourceName = Enum_Resource.None;
    public float Quantity = 0;
    public int rarity = 0; // Range between 0 - 5
}
