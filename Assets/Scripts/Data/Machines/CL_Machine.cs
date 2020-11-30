using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CL_Machine
{
    [SerializeField]
    public int ProcessSpeed = 1;
    [SerializeField]
    public int AmountCanProcess = 1;

    [SerializeField]
    public Enum_Items TypeOfMachine = Enum_Items.None;

    public int Level = 1;
    public bool CanLevelUp = true;

    public CL_Machine()
    {
        ProcessSpeed = 1;
        AmountCanProcess = 1;
    }

    public void LevelUp()
    {
        if (CanLevelUp)
            Level += 1;
    }

}