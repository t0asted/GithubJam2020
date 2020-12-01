using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class S_InventoryItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_NameInventoryItem;
    [SerializeField]
    private TextMeshProUGUI m_NameInventoryItemQuantity;
    [SerializeField]
    private Image m_IconInventoryItem;

    private CL_Resource Data;

    private void Update()
    {
        if (m_NameInventoryItem != null)
        {
            m_NameInventoryItem.SetText(Data.ResourceName.ToString());
        }
        if(m_NameInventoryItemQuantity != null)
        {
            m_NameInventoryItemQuantity.SetText(Data.Quantity.ToString());
        }
    }

    public void SetItem(CL_Resource resourcePass)
    {
        Data = resourcePass;
        if (m_IconInventoryItem != null)
        {
            if (resourcePass.ResourceData != null)
            {
                m_IconInventoryItem.sprite = resourcePass.ResourceData.Sprite;
            }
        }
    }
}
