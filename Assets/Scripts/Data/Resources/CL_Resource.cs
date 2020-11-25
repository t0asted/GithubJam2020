using UnityEngine;

[System.Serializable]
public class CL_Resource : CL_ResourceBase
{
    public CL_Resource()
    {
        ResourceName = Enum_Items.None;
        Quantity = 1;
    }

    public CL_Resource(Enum_Items namePass, float quantityPass)
    {
        ResourceName = namePass;
        Quantity = quantityPass;
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
}