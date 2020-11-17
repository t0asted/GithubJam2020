using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class S_MachineBase : MonoBehaviour
{
    public S_GameController ref_GameController = new S_GameController();
    public CL_Machine MachineData = new CL_Machine();

    [SerializeField]
    private TextMeshProUGUI TextToDisplay = null;

    private void Start()
    {
        InvokeRepeating("RunMachine", 1f, 1f);
    }

    public void RunMachine()
    {
        MachineData.Process();

        TextToDisplay.text = MachineData.ResourceContent();
    }

    public string GetTextProcessSpeed()
    {
        return "Process Speed : " + MachineData.ProcessSpeed;
    }

}