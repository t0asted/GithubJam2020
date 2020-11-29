using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider), typeof(MeshRenderer))]
public class S_Astroid : MonoBehaviour
{
    SO_Planet LevelData;

    [SerializeField]
    MeshRenderer meshRend;
    public void SetSizeOfAstroid(SO_Planet LevelDataPass)
    {
        LevelData = LevelDataPass;
        this.transform.localScale = new Vector3(LevelData.size, LevelData.size, LevelData.size);
        meshRend.material = LevelData.material;
        
    }

    public Material spawnMaterial()
    {
        Material thisOne = LevelData.material;
        return thisOne;
    }

    public int numberOfSpawns()
    {
        int spawns = LevelData.numberOfFeatures;
        return spawns;
    }
}
