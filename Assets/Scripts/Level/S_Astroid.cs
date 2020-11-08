using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider), typeof(MeshRenderer))]
public class S_Astroid : MonoBehaviour
{
    CL_Level LevelData;
    public void SetSizeOfAstroid(CL_Level LevelDataPass)
    {
        LevelData = LevelDataPass;
        this.transform.localScale = new Vector3(LevelData.size, LevelData.size, LevelData.size);
    }
}
