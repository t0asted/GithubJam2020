using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LevelSpawner : MonoBehaviour
{
    private CL_Level LevelData;
    public bool SpawnLevel(CL_Level LevelToLoad)
    {
        LevelData = LevelToLoad;

        return true;
    }
}
