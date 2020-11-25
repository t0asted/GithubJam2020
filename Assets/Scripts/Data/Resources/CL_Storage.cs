using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

[System.Serializable]
public class CL_Storage
{
    public List<CL_Resource> ResourceList = new List<CL_Resource>();
    public int StorageSize = 0;
    public bool stack = true;

    public CL_Storage()
    {
        ResourceList = new List<CL_Resource>();
        StorageSize = 0;
        stack = true;
    }

    public CL_Storage(List<CL_Resource> ResourcesPass)
    {
        ResourceList = ResourcesPass;
        StorageSize = 0;
        stack = true;
    }

    public float GetResourcesSize()
    {
        float Size = 0;
        foreach (var item in ResourceList)
        {
            Size += item.Quantity;
        }
        return Size;
    }

    public bool IsStorageFull()
    {
        return GetResourcesSize() >= StorageSize;
    }

    public bool AddResources(CL_Resource ResourceToAdd)
    {
        CL_Resource ResourceFound = ResourceList.Find(f => f.ResourceName == ResourceToAdd.ResourceName);

        if (IsStorageFull())
        {
            if (ResourceFound != null)
            {
                ResourceFound.AddResource(ResourceToAdd);
            }
            else
            {
                ResourceList.Add(ResourceToAdd);
            }
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool AddResources(List<CL_Resource> ResourcesToAdd)
    {
        if (IsStorageFull())
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
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool AddResources(CL_Storage ResourcesToAdd)
    {
        if (IsStorageFull())
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
            return true;
        }
        else
        {
            return false;
        }
    }

}