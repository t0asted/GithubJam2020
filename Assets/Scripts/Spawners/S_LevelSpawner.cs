using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LevelSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Astroid;

    [SerializeField]
    private GameObject m_AsteroidStarter;

    [SerializeField]
    private int m_MinimumPlanetColliderSeperation = 50;

    [SerializeField]
    private int m_MaximumPlanetColliderSeperation = 250;



    private S_GameController ref_GameController;
    private List<SO_Planet> LevelData;

    private void Start()
    {
        if (GameObject.Find("_GameController") != null)
        {
            ref_GameController = GameObject.Find("_GameController").GetComponent<S_GameController>();
            ref_GameController.SetLevelSpawner(this);
        }
        else
            Debug.Log("Level spawner : Did not find gamecontroller");
    }

    public bool SpawnLevel(List<SO_Planet> LevelToLoad)
    {
        LevelData = LevelToLoad;

        // Sort planet data by planet size
        List<SO_Planet> sortedList = new List<SO_Planet>(LevelToLoad);
        sortedList.Sort((SO_Planet p1, SO_Planet p2) => p2.size.CompareTo(p1.size));

        // Calculate planet seperation boundaries
        int minPlanetSeperation = (3 * sortedList[0].size) + m_MinimumPlanetColliderSeperation;
        Debug.Log(minPlanetSeperation);
        int maxPlanetSeperation = minPlanetSeperation + m_MaximumPlanetColliderSeperation - m_MinimumPlanetColliderSeperation;

        // Create spawned planet list and add first planet (STARTER)
        List<Vector3> spawnedPlanets = new List<Vector3>();
        spawnedPlanets.Add(new Vector3(0, 0, 0));

        // Add planet to world and remove from list ready for for loop
        InstantiatePlanet(m_AsteroidStarter, LevelData[0], new Vector3(100, 0, 0));
        LevelData.Remove(LevelData[0]);


        foreach (var Planet in LevelData)
        {
            bool spawnable = false;
            Vector3 randomPosition = Vector3.zero;
            while (!spawnable)
            {
                // Randomly choose planet to spawn from
                int closestPlanet = Random.Range(0, spawnedPlanets.Count);
                int largestVector = Random.Range(0, 3);

                int[] randomValues = new int[] {
                    Random.Range(0, minPlanetSeperation),
                    Random.Range(0, minPlanetSeperation),
                    Random.Range(0, minPlanetSeperation)
                };

                randomValues[largestVector] = Random.Range(minPlanetSeperation + 1, maxPlanetSeperation);

                randomPosition = spawnedPlanets[closestPlanet] + new Vector3(randomValues[0], randomValues[1], randomValues[2]);

                // Check position against positions of spawned planets
                foreach (Vector3 spawnedVector in spawnedPlanets)
                {
                    if (Vector3.Distance(randomPosition, spawnedVector) > minPlanetSeperation)
                    {
                        spawnable = true;
                    } 
                    else
                    {
                        spawnable = false;
                        break;
                    }
                }
            }
            InstantiatePlanet(m_Astroid, Planet, randomPosition);
            spawnedPlanets.Add(randomPosition);
        }

        return true;
    }

    // Function to add planet to game world
    private void InstantiatePlanet(GameObject asteroid, SO_Planet data, Vector3 position)
    {
        // Check gameobject is not null
        if (asteroid)
        {
            GameObject toSpawn = Instantiate(asteroid, position, new Quaternion(), this.transform);
            toSpawn.GetComponent<S_Astroid>().SetSizeOfAstroid(data);
        }
    }
}