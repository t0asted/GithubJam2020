﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Level")]
public class CL_Level : ScriptableObject
{
    public int size = 10;
    public int gravity = 1;
    public SO_ListRaw Resources;
}