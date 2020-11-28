using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item/Planet")]
public class SO_Planet : ScriptableObject
{
    public int size = 10;
    public int gravity = 1;
    public List<CL_PlanetData> PlanetData;
    public int numberOfFeatures;
}