using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CharacterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject CharacterToSpawn = null;

    private CL_Character CharacterData;
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


    public bool SpawnCharacter(CL_Character CharacterDataPass)
    {
        CharacterData = CharacterDataPass;

        //find spawn points

        var spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        // This is just getting first spawn point it finds for now. Might add multiple

        if (CharacterToSpawn != null && spawnPoints.Length > 0)
        {
            GameObject CharacterGO = Instantiate(CharacterToSpawn);
            CharacterGO.transform.position = spawnPoints[0].transform.position;
            CharacterGO.transform.SetParent(this.transform);
        }

        return true;
    }
}
