using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_InGameMenu : S_SceneUIMain
{
    private S_GameController ref_GameController;

    private void Start()
    {
        ref_GameController = GameObject.Find("_GameController").GetComponent<S_GameController>();
    }

    public void Btn_Resume()
    {

    }

    public void Btn_Quit()
    {

    }

}
