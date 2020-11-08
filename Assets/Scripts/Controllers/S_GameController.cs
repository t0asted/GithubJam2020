using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_GameController : MonoBehaviour
{
    private S_CharacterSpawner m_CharacterSpawner = null;
    private S_LevelSpawner m_LevelSpawner = null;

    private LoadingStages isLoaded = LoadingStages.NotLoaded;

    private bool CharacterSpawned = false;
    private bool LevelSpawned = false;

    [SerializeField]
    private UnityEvent OnStart;
    [SerializeField]
    private UnityEvent OnStartDebugJoshua;
    [SerializeField]
    private UnityEvent OnStartDebugStefan;
    [SerializeField]
    private UnityEvent OnStartDebugMatt;
    [SerializeField]
    private UnityEvent OnStartDebugJames;
    [SerializeField]
    private CL_Level LevelData = null;
    [SerializeField]
    private CL_Character CharacterData = null;

    public CL_Game GameData = new CL_Game();

    private void Start()
    {
        OnStart.Invoke();
    }

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
        LevelSpawned = m_LevelSpawner.SpawnLevel(LevelData);
        if(LevelSpawned)
        {
            CharacterSpawned = m_CharacterSpawner.SpawnCharacter(CharacterData);
        }
    }

    public void TogglePauseGame()
    {
        GameData.Paused = !GameData.Paused;
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

public enum GameModes
{ 
    Normal,
    DebugJoshua,
    DebugStefan,
    DebugMatt,
    DebugJames
}