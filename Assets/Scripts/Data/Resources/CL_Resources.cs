using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Resources")]
public class CL_Resources : ScriptableObject
{
    public List<CL_Resource> ResourceList = new List<CL_Resource>();

    public CL_Resources()
    {
        ResourceList = new List<CL_Resource>();
    }

    public CL_Resources(List<CL_Resource> ResourcesPass)
    {
        ResourceList = ResourcesPass;
    }

    public void AddResources(CL_Resource ResourceToAdd)
    {
        CL_Resource ResourceFound = ResourceList.Find(f => f.ResourceName == ResourceToAdd.ResourceName);

        if (ResourceFound != null)
        {
            ResourceFound.AddResource(ResourceToAdd);
        }
        else
        {
            ResourceList.Add(ResourceToAdd);
        }
    }

    public void AddResources(List<CL_Resource> ResourcesToAdd)
    {
        if (ResourceList.Count > 0)
        {
            foreach (var itemToAdd in ResourcesToAdd)
            {
                CL_Resource ResourceFound = ResourceList.Find(f => f.ResourceName == itemToAdd.ResourceName);

                if (ResourceFound != null)
                {
                    ResourceFound.AddResource(itemToAdd);
                    continue;
                }
                else
                {
                    ResourceList.Add(itemToAdd);
                }
            }
        }
        else
        {
            ResourceList = ResourcesToAdd;
        }
    }

    public void AddResources(CL_Resources ResourcesToAdd)
    {
        if (ResourceList.Count > 0)
        {
            foreach (var itemToAdd in ResourcesToAdd.ResourceList)
            {
                CL_Resource ResourceFound = ResourceList.Find(f => f.ResourceName == itemToAdd.ResourceName);

                if (ResourceFound != null)
                {
                    ResourceFound.AddResource(itemToAdd);
                    continue;
                }
                else
                {
                    ResourceList.Add(itemToAdd);
                }
            }
        }
        else
        {
            ResourceList = ResourcesToAdd.ResourceList;
        }
    }

}