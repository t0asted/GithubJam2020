using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_DebugLoad : S_SceneUIMain
{
    
    [SerializeField]
    private S_OpenCloseGameScene GameController;

    public UnityEvent m_StartDebugJoshua;
    public UnityEvent m_StartDebugStefan;
    public UnityEvent m_StartDebugMatt;
    public UnityEvent m_StartDebugJames;

    public void Btn_DebugJoshuaScene()
    {
        m_StartDebugJoshua.Invoke();
        if(GameController != null)
        {
            GameController.OpenScene();
            CloseScene();
        }
    }

    public void Btn_DebugStefanScene()
    {
        m_StartDebugStefan.Invoke();
        if (GameController != null)
        {
            GameController.CloseScene();
            CloseScene();
        }
    }

    public void Btn_DebugMattScene()
    {
        m_StartDebugMatt.Invoke();
        if (GameController != null)
        {
            GameController.CloseScene();
            CloseScene();
        }
    }

    public void Btn_DebugJamesScene()
    {
        m_StartDebugJames.Invoke();
        if (GameController != null)
        {
            GameController.CloseScene();
            CloseScene();
        }
    }
}
