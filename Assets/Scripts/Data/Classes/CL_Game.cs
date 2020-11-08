using System.Collections.Generic;

public class CL_Game
{
    public List<CL_Resource> Resources = new List<CL_Resource>() { 
        new CL_Resource(Enum_Resource.Carbon, 0), 
        new CL_Resource(Enum_Resource.Gold, 0), 
        new CL_Resource(Enum_Resource.Nickel, 0), 
        new CL_Resource(Enum_Resource.Titanium, 0)
    };

    public void AddResource(CL_Resource ResourcePass)
    {
        CL_Resource ItemFound = Resources.Find(f => f.ResourceName == ResourcePass.ResourceName);
        if (ItemFound != null)
            ItemFound.AddResource(ResourcePass);
    }
}