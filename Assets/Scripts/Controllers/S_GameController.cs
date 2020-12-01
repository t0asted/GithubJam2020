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
    private SO_Planet m_StarterPlanet;
    [SerializeField]
    private List<SO_Planet> m_Planets;
    [SerializeField]
    private List<SO_Planet> m_JoiningPlanets;


    public S_CharacterController CharacterController = null;

    public S_HUDController m_HUDController = null;

    public CL_Game GameData = new CL_Game();

    public GameObject ref_Character = null;

    private S_Timer timer = null;

    private void Start()
    {
        OnStart.Invoke();
    }

    public List<CL_Resource> GetResourceList()
    {
        return GameData.Storage.ResourceList;
    }

    private void Update()
    {
        // Check if not loaded and has references
        if (isLoaded == LoadingStages.NotLoaded && m_CharacterSpawner != null && m_LevelSpawner != null)
        {
            SetupGame();
            timer = GetComponent<S_Timer>();
            isLoaded = LoadingStages.Loading;
        }

        if (isLoaded == LoadingStages.Loading && CharacterSpawned && LevelSpawned)
        {
            isLoaded = LoadingStages.Loaded;
            timer.StartTimer();
        }

        if (isLoaded == LoadingStages.Loaded)
        {
            // do shit
        }

        //Debug Respawn
        if (Input.GetKeyDown(KeyCode.K))
        {
            Destroy(ref_Character);
            ref_Character = m_CharacterSpawner.SpawnCharacter();
        }
    }

    private void SetupGame()
    {
        LevelSpawned = m_LevelSpawner.SpawnLevel(m_StarterPlanet, m_Planets, m_JoiningPlanets, GameData.NumberOfPlanets);
        if (LevelSpawned)
        {
            ref_Character = m_CharacterSpawner.SpawnCharacter();
            if (ref_Character != null)
            {
                foreach (Transform child in ref_Character.transform)
                {
                    if (child.GetComponent<S_CharacterController>())
                    {
                        CharacterController = child.GetComponent<S_CharacterController>();
                    }
                }
                CharacterSpawned = true;
            }
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

    public void SetHUDScript(S_HUDController HUDController)
    {
        m_HUDController = HUDController;
    }

    public string GetTime()
    {
        if (timer) return timer.formattedTime;
        return "11:11";
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