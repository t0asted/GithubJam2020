using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_MainMenu : S_SceneUIMain
{
    public UnityEvent m_StartGame;

    private S_GameController ref_GameController;

    private void Start()
    {
        //ref_GameController = GameObject.Find("_GameController").GetComponent<S_GameController>();
    }

    public void Btn_Play()
    {
        m_StartGame.Invoke();
        if (GameObject.Find("_GameController") != null)
        {
            ref_GameController.GameData.GameMode = GameModes.Normal;
        }
        else
            Debug.Log("GameMode setter : Did not find gamecontroller");
        CloseScene();
    }

    public void Btn_Quit()
    {
        Application.Quit();
    }
}
