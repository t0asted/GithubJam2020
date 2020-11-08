using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_DebugLoad : S_SceneUIMain
{
    public UnityEvent m_StartDebugJoshua;
    public UnityEvent m_StartDebugStefan;
    public UnityEvent m_StartDebugMatt;
    public UnityEvent m_StartDebugJames;

    private S_GameController ref_GameController;

    public void Btn_DebugJoshuaScene()
    {
        m_StartDebugJoshua.Invoke();
        if (GameObject.Find("_GameController") != null)
        {
            ref_GameController = GameObject.Find("_GameController").GetComponent<S_GameController>();
            ref_GameController.GameData.GameMode = GameModes.DebugJoshua;
        }
        else
            Debug.Log("GameMode setter : Did not find gamecontroller");
        CloseScene();
    }

    public void Btn_DebugStefanScene()
    {
        m_StartDebugStefan.Invoke();
        if (GameObject.Find("_GameController") != null)
        {
            ref_GameController = GameObject.Find("_GameController").GetComponent<S_GameController>();
            ref_GameController.GameData.GameMode = GameModes.DebugStefan;
        }
        else
            Debug.Log("GameMode setter : Did not find gamecontroller");
        CloseScene();
    }

    public void Btn_DebugMattScene()
    {
        m_StartDebugMatt.Invoke();
        if (GameObject.Find("_GameController") != null)
        {
            ref_GameController = GameObject.Find("_GameController").GetComponent<S_GameController>();
            ref_GameController.GameData.GameMode = GameModes.DebugMatt;
        }
        else
            Debug.Log("GameMode setter : Did not find gamecontroller");
        CloseScene();
    }

    public void Btn_DebugJamesScene()
    {
        m_StartDebugJames.Invoke();
        if (GameObject.Find("_GameController") != null)
        {
            ref_GameController = GameObject.Find("_GameController").GetComponent<S_GameController>();
            ref_GameController.GameData.GameMode = GameModes.DebugJames;
        }
        else
            Debug.Log("GameMode setter : Did not find gamecontroller");
        CloseScene();
    }
}
