using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider), typeof(MeshRenderer))]
public class S_Astroid : MonoBehaviour
{
    SO_Planet LevelData;
    public void SetSizeOfAstroid(SO_Planet LevelDataPass)
    {
        LevelData = LevelDataPass;
        this.transform.localScale = new Vector3(LevelData.size, LevelData.size, LevelData.size);
    }

    public bool CheckLocationToSpawn(Vector3 spawn)
    {
        
        
        return false;
    }
}
