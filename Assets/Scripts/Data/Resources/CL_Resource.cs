using UnityEngine;

[System.Serializable]
public class CL_Resource
{
    public Enum_Resource ResourceName = Enum_Resource.None;
    public float Quantity = 0;
    
    public CL_Resource()
    {
        ResourceName = Enum_Resource.None;
        Quantity = 0;
    }

    public CL_Resource(Enum_Resource namePass, float quantityPass)
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