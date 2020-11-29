using JetBrains.Annotations;
using System.Collections.Generic;

[System.Serializable]
public class CL_Game
{
    public GameModes GameMode = GameModes.Normal;
    public bool Paused = false;
    public int NumberOfPlanets = 8;

    public CL_Storage Storage = new CL_Storage();

    public void AddResource(CL_Resource ResourcePass)
    {
        CL_Resource ItemFound = Storage.ResourceList.Find(f => f.ResourceName == ResourcePass.ResourceName);
        if (ItemFound != null)
        {
            ItemFound.AddResource(ResourcePass);
            return;
        }
        else
        {
            Storage.ResourceList.Add(ResourcePass);
        }

    }
}