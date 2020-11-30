using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class S_BuildQueueItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_NameBuildQueueItem;
    [SerializeField]
    private Image m_IconBuildQueueItem;
    public void SetItem(CL_BuildQueue resourcePass)
    {
        if (m_NameBuildQueueItem != null)
        {
            m_NameBuildQueueItem.SetText(resourcePass.DataObject.ResourceName.ToString() + " " + resourcePass.Quantity);
        }
        if (m_IconBuildQueueItem != null)
        {
            m_IconBuildQueueItem = null;
        }
    }
}
