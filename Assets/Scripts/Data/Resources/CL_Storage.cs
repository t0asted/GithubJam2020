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
        CL_Storage NewStorage = new CL_Storage(new List<CL_Resource>() { ResourceToAdd });
        return AddResources(NewStorage);
    }

    public bool AddResources(List<CL_Resource> ResourcesToAdd)
    {
        CL_Storage NewStorage = new CL_Storage(ResourcesToAdd);
        return AddResources(NewStorage);
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

    public bool TakeResources(CL_Resource ResourceToAdd)
    {
        CL_Storage newStorage = new CL_Storage(new List<CL_Resource>() { ResourceToAdd });
        return TakeResources(newStorage);
    }

    public bool TakeResources(List<CL_Resource> ResourcesToTake)
    {
        CL_Storage newStorage = new CL_Storage(ResourcesToTake);
        return TakeResources(newStorage);
    }

    public bool TakeResources(CL_Storage ResourceToTake)
    {
        if (HasResource(ResourceToTake))
        {
            if (ResourceList.Count > 0)
            {
                foreach (var itemToTake in ResourceToTake.ResourceList)
                {
                    CL_Resource ResourceFound = ResourceList.Find(f => f.ResourceName == itemToTake.ResourceName);

                    if (ResourceFound != null)
                    {
                        ResourceFound.TakeResource(itemToTake);
                        continue;
                    }
                    else
                        return false;
                }
                return true;
            }
        }
        return false;
    }

    public bool HasResource(CL_Resource ResourceToAdd)
    {
        CL_Storage newStorage = new CL_Storage(new List<CL_Resource>() { ResourceToAdd });
        return HasResource(newStorage);
    }

    public bool HasResource(List<CL_Resource> ResourcesToTake)
    {
        CL_Storage newStorage = new CL_Storage(ResourcesToTake);
        return HasResource(newStorage);
    }

    public bool HasResource(CL_Storage ResourceToTake)
    {
        if (ResourceList.Count > 0)
        {
            foreach (var itemToTake in ResourceToTake.ResourceList)
            {
                CL_Resource ResourceFound = ResourceList.Find(f => f.ResourceName == itemToTake.ResourceName);

                if (ResourceFound != null)
                {
                    if (ResourceFound.Quantity > itemToTake.Quantity)
                    {
                        continue;
                    }
                }
                return false;
            }
            return true;
        }
        else
        {
            Debug.Log("This is Empty!");
            return false;
        }
    }


}