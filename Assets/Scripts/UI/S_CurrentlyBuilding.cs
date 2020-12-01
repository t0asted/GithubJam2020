using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_CurrentlyBuilding : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_TextCurrentBuilding;
    [SerializeField]
    private TextMeshProUGUI m_TextCurrentBuildingQuantity;
    [SerializeField]
    private Image m_ImageCurrentBuilding;

    private CL_BuildQueue Data;

    public void SetData(CL_BuildQueue BuildDataPass)
    {
        Data = BuildDataPass;
        if(Data != null)
        {
            if(Data.DataObject != null)
            {
                if (m_TextCurrentBuilding != null)
                {
                    m_TextCurrentBuilding.SetText(BuildDataPass.DataObject.ResourceName.ToString());
                }
                if (m_TextCurrentBuildingQuantity != null)
                {
                    m_TextCurrentBuildingQuantity.SetText(BuildDataPass.DataObject.Quantity.ToString());
                }
                if (m_ImageCurrentBuilding != null)
                {
                    m_ImageCurrentBuilding.sprite = BuildDataPass.DataObject.Sprite;
                }
            }
        }
    }
}
