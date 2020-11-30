using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CharacterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject CharacterToSpawn = null;

    [SerializeField]
    private GameObject m_Rocket = null;

    private S_GameController ref_GameController;

    private void Start()
    {
        ref_GameController = GameObject.Find("_GameController").GetComponent<S_GameController>();
        if (GameObject.Find("_GameController") != null)
        {
            ref_GameController.SetCharacterSpawner(this);
        }
        else
            Debug.Log("Character spawner : Did not find gamecontroller");
    }


    public GameObject SpawnCharacter()
    {

        //find spawn points

        var spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        // This is just getting first spawn point it finds for now. Might add multiple

        GameObject CharacterGO = null;

        if (CharacterToSpawn != null && spawnPoints.Length > 0)
        {
            CharacterGO = Instantiate(CharacterToSpawn);
            CharacterGO.transform.position = spawnPoints[0].transform.position;
            CharacterGO.transform.SetParent(this.transform);

            //Temp
            if (m_Rocket != null)
            {
                Vector3 SpawnLocation = GameObject.FindGameObjectsWithTag("SpawnPoint")[0].transform.position + new Vector3(5, (float)-1.5, 0);
                GameObject rocketGO = Instantiate(m_Rocket, SpawnLocation, new Quaternion());
                rocketGO.transform.SetParent(this.transform);
            }

        }



        return CharacterGO;
    }
}
