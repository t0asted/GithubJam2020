using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_HUD : S_SceneUIMain
{
    private S_GameController ref_GameController;

    private void Start()
    {
        ref_GameController = GameObject.Find("_GameController").GetComponent<S_GameController>();
    }
}
