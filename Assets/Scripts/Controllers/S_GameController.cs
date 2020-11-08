using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GameController : MonoBehaviour
{
    private S_CharacterSpawner m_CharacterSpawner = null;
    private S_LevelSpawner m_LevelSpawner = null;

    private LoadingStages isLoaded = LoadingStages.NotLoaded;

    private bool CharacterSpawned = false;
    private bool LevelSpawned = false;

    [SerializeField]
    private CL_Level LevelData = null;
    [SerializeField]
    private CL_Character CharacterData = null;

    public CL_Game GameData = new CL_Game();

    private void Update()
    {
        // Check if not loaded and has references
        if(isLoaded == LoadingStages.NotLoaded && m_CharacterSpawner != null && m_LevelSpawner != null)
        {
            SetupGame();
            isLoaded = LoadingStages.Loading;
        }

        if(isLoaded == LoadingStages.Loading && CharacterSpawned && LevelSpawned)
        {
            isLoaded = LoadingStages.Loaded;
        }

        if(isLoaded == LoadingStages.Loaded)
        {
            // do shit
        }
    }

    private void SetupGame()
    {
        CharacterSpawned = m_CharacterSpawner.SpawnCharacter(CharacterData);
        LevelSpawned = m_LevelSpawner.SpawnLevel(LevelData);
    }

    public void SetCharacterSpawner(S_CharacterSpawner CharacterSpawnerPass)
    {
        m_CharacterSpawner = CharacterSpawnerPass;
    }

    public void SetLevelSpawner(S_LevelSpawner LevelSpawnerPass)
    {
        m_LevelSpawner = LevelSpawnerPass;
    }

}

public enum LoadingStages
{
    NotLoaded,
    Loading,
    Loaded
}