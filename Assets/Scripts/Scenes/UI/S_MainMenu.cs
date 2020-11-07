using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_MainMenu : S_SceneUIMain
{
    public S_OpenCloseGameScene GameController;
    public UnityEvent m_StartGame;

    public void Btn_Play()
    {
        m_StartGame.Invoke();
        if(GameController != null)
        {
            GameController.OpenScene();
            CloseScene();
        }
    }

}
