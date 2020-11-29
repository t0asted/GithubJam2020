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
    private int m_MinimumPlanetColliderSeperation = 25;
    [SerializeField]
    private int m_MaximumPlanetColliderSeperation = 100;
    [SerializeField]
    private int m_JoiningPlanetSeperation = 10;

    // Spawned Planets Storage
    private List<SpawnedPlanet> SpawnedPlanets;
    private List<SpawnedPlanet> SpawnedJoiningPlanets;

    private S_GameController ref_GameController;

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

    public bool SpawnLevel(SO_Planet starterPlanet, List<SO_Planet> planets, List<SO_Planet> joiningPlanets, int numberOfPlanets)
    {
        SpawnedPlanets = new List<SpawnedPlanet>();
        SpawnedJoiningPlanets = new List<SpawnedPlanet>();

        // Instantiate Starter Planet
        SpawnStarterPlanet(starterPlanet);


        for (int i = 0; i < numberOfPlanets; i++)
        {
            bool spawned = false;
            while (!spawned)
            {
                // Select Random planets
                int rSpawnPlanet = Random.Range(0, SpawnedPlanets.Count);
                int rJoiningPlanet = Random.Range(0, joiningPlanets.Count);
                int rPlanet = Random.Range(0, planets.Count);
                rPlanet = i < planets.Count ? i : rPlanet;

                // Get Planet Data
                SO_Planet joiningData = joiningPlanets[rJoiningPlanet];
                SO_Planet planetData = planets[rPlanet];

                Debug.Log("THIS -----> " + SpawnedPlanets[rSpawnPlanet].Size);

                // Create random vector for joining planet
                Vector3 joiningOrigin = RandomSpawnPoint(SpawnedPlanets[rSpawnPlanet], joiningData.size, true);
                
                // Check if it is a spawnable vector
                if (IsSpawnable(joiningOrigin, joiningData.size))
                {
                    SpawnedPlanet joiningPlanet = new SpawnedPlanet(joiningOrigin, joiningData.size);
                    
                    // Repeat for the spawning planet
                    Vector3 planetOrigin = RandomSpawnPoint(joiningPlanet, planetData.size, false);

                    if (IsSpawnable(planetOrigin, planetData.size))
                    {
                        SpawnedPlanet planet = new SpawnedPlanet(planetOrigin, planetData.size);

                        // Spawn Planets
                        SpawnPlanet(m_Astroid, joiningData, joiningOrigin);
                        SpawnPlanet(m_Astroid, planetData, planetOrigin);
                        SpawnedJoiningPlanets.Add(joiningPlanet);
                        SpawnedPlanets.Add(planet);

                        // End Loop
                        spawned = true;
                    }
                }
            }
        }
        return true;
    }

    // Function to add planet to game world
    private void SpawnPlanet(GameObject asteroid, SO_Planet data, Vector3 position)
    {
        // Check gameobject is not null
        if (asteroid)
        {
            GameObject toSpawn = Instantiate(asteroid, position, new Quaternion(), this.transform);
            toSpawn.GetComponent<S_Astroid>().SetSizeOfAstroid(data);
            Debug.Log("Size of Asteroid - " + data.size);
        }
    }


    // Method that builds starter planet
    private void SpawnStarterPlanet(SO_Planet starterPlanet)
    {
        SpawnPlanet(m_AsteroidStarter, starterPlanet, new Vector3(0, 0, 0));
        SpawnedPlanets.Add(new SpawnedPlanet(new Vector3(0, 0, 0), starterPlanet.size));
    }


    // Method that checks a possible spawn against all existing spawned planets
    private bool IsSpawnable(Vector3 spawnPosition, int planetSize)
    {
        // Combine spawned list
        List<SpawnedPlanet> combined = new List<SpawnedPlanet>(SpawnedPlanets);
        combined.AddRange(SpawnedJoiningPlanets);

        foreach (SpawnedPlanet sp in combined)
        {
            int minSep = sp.Size + planetSize + m_JoiningPlanetSeperation;

            if (Vector3.Distance(sp.Origin, spawnPosition) < minSep)
            {
                return false;
            }
        }
        return true;
    }

    private Vector3 RandomSpawnPoint(SpawnedPlanet spawnPlanet, int size, bool isJoining)
    {
        Debug.Log("Random Spawn Point");

        int minSep = spawnPlanet.Size + size;
        int maxSep = minSep + m_MaximumPlanetColliderSeperation;

        Debug.Log("Min Sep - " + minSep);
        Debug.Log("Max Sep - " + maxSep);

        int rAxis = Random.Range(0, 3);
        int[] rVals = new int[] {
            Random.Range(-minSep, minSep), Random.Range(-minSep, minSep), Random.Range(-minSep, minSep)
        };

        if (isJoining) rVals[rAxis] = ((Random.Range(0, 2) * 2) - 1) * (minSep + m_JoiningPlanetSeperation);
        else rVals[rAxis] = ((Random.Range(0, 2) * 2) - 1) * Random.Range(minSep + m_MinimumPlanetColliderSeperation, maxSep);

        Debug.Log("Value moved - " + rVals[rAxis]);
        return spawnPlanet.Origin + new Vector3(rVals[0], rVals[1], rVals[2]);
    }
}


// Helper class for storing spawned planets
public class SpawnedPlanet {

    public Vector3 Origin { get; }
    public int Size { get; }

    public SpawnedPlanet(Vector3 planetOrigin, int planetSize)
    {
        Origin = planetOrigin;
        Size = planetSize;
    }

}
