using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LevelSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject m_astroid;

    private S_GameController ref_GameController;
    private CL_Level LevelData;

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

    public bool SpawnLevel(CL_Level LevelToLoad)
    {
        LevelData = LevelToLoad;

        if (m_astroid != null)
        {
            GameObject astroidGO = Instantiate(m_astroid, this.transform);
            astroidGO.GetComponent<S_Astroid>().SetSizeOfAstroid(LevelData);
            astroidGO.transform.SetParent(this.transform);
        }

        return true;
    }

}