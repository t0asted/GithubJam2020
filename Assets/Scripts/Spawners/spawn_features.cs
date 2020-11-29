using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_features : MonoBehaviour
{
   
    private int numberOfSpawns;
    public List<GameObject> spawnLocations;
    public List<GameObject> featuresToSpawn;
    private S_Astroid astroid;

    public void Start()
    {
        astroid = GetComponent<S_Astroid>();
        numberOfSpawns = astroid.numberOfSpawns();
        for (int i = 0; i < numberOfSpawns; i++)
        {
            SpawnFeature();
        }
    }

    public void SpawnFeature()
    {
        int rndLocal = Random.Range(0, spawnLocations.Count);
        Vector3 selectedSpawn = spawnLocations[rndLocal].transform.position;

        int rndFeature = Random.Range(0, featuresToSpawn.Count);
        GameObject featurePrefab = featuresToSpawn[rndFeature];

        GameObject newFeature = Instantiate(featurePrefab, selectedSpawn, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        newFeature.transform.SetParent(spawnLocations[rndLocal].transform);

        MeshRenderer meshRenderer = newFeature.GetComponent<MeshRenderer>();
        meshRenderer.material = astroid.spawnMaterial();

        spawnLocations.RemoveAt(rndLocal);
    }



}

