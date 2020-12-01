using UnityEngine;

[System.Serializable]
public class CL_Resource : CL_ResourceBase
{
    public CL_Resource(SO_ResourceBase ResourceDataPass)
    {
        ResourceName = ResourceDataPass.ResourceName;
        Quantity = ResourceDataPass.Quantity;
        ResourceData = ResourceDataPass;
    }

    public CL_Resource(Enum_Items ResourceNamePass, float QuanityPass)
    {
        ResourceName = ResourceNamePass;
        Quantity = QuanityPass;
    }

    public CL_Resource(Enum_Items namePass, float quantityPass, SO_ResourceBase ResourceDataPass)
    {
        ResourceName = namePass;
        Quantity = quantityPass;
        ResourceData = ResourceDataPass;
    }

    public CL_Resource(SO_Item dataPass)
    {
        ResourceName = dataPass.ResourceName;
        Quantity = dataPass.Quantity;
        ResourceData = dataPass;
    }

    public bool AddResource(CL_Resource resourcePass)
    {
        if (resourcePass.ResourceName == ResourceName)
        {
            Quantity += resourcePass.Quantity;
            return true;
        }
        return false;
    }

    public bool TakeResource(CL_Resource resourcePass)
    {
        if(resourcePass.Quantity <= Quantity)
        {
            Quantity -= resourcePass.Quantity;
            return true;
        }
        return false;
    }

}