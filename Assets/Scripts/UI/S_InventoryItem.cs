using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class S_InventoryItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_NameInventoryItem;
    [SerializeField]
    private Image m_IconInventoryItem;
    public void SetItem(CL_Resource resourcePass)
    {
        if(m_NameInventoryItem != null)
        {
            m_NameInventoryItem.SetText(resourcePass.ResourceName.ToString() + " " + resourcePass.Quantity);
        }
        if(m_IconInventoryItem != null)
        {
            m_IconInventoryItem = null;
        }
    }
}
