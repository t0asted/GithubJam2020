using JetBrains.Annotations;
using System.Collections.Generic;

[System.Serializable]
public class CL_Game
{
    public GameModes GameMode = GameModes.Normal;
    public bool Paused = false;

    public List<CL_Resource> Resources = new List<CL_Resource>();

    public void AddResource(CL_Resource ResourcePass)
    {
        CL_Resource ItemFound = Resources.Find(f => f.ResourceName == ResourcePass.ResourceName);
        if (ItemFound != null)
        {
            ItemFound.AddResource(ResourcePass);
            return;
        }
        else
        {
            Resources.Add(ResourcePass);
        }

    }
}