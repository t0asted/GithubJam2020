using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Resources")]
public class CL_ResourcesScriptable : ScriptableObject
{
    public List<CL_ResourceScriptable> ResourceList = new List<CL_ResourceScriptable>();
    public int StorageSize = 0;
    public bool stack = true;
}
