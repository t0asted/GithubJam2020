using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class S_BuildQueueItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_NameBuildQueueItem;
    [SerializeField]
    private TextMeshProUGUI m_BuildQueueItemQuantity;
    [SerializeField]
    private Image m_IconBuildQueueItem;
    [SerializeField]
    private Button m_ButtonBuildQueue;

    private CL_BuildQueue Data;

    private S_InGameMenuBase ToPassBackTo;

    private void Update()
    {
        if (m_NameBuildQueueItem != null)
        {
            m_NameBuildQueueItem.SetText(Data.DataObject.ResourceName.ToString());
        }
        if(m_BuildQueueItemQuantity != null)
        {
            m_BuildQueueItemQuantity.SetText(Data.Quantity.ToString());
        }
    }

    public void SetItem(CL_BuildQueue resourcePass, S_InGameMenuBase ToPassBackToPass)
    {
        ToPassBackTo = ToPassBackToPass;
        Data = resourcePass;
        if (m_IconBuildQueueItem != null)
        {
            if (Data.DataObject != null)
            {
                m_IconBuildQueueItem.sprite = Data.DataObject.Sprite != null ? Data.DataObject.Sprite : null;
            }
        }

        if(m_ButtonBuildQueue != null)
        {
            m_ButtonBuildQueue.onClick.AddListener(RemoveItemFromQueue);
        }

    }

    public void RemoveItemFromQueue()
    {
        if (ToPassBackTo is S_MachineBuilder)
        {
            ((S_MachineBuilder)ToPassBackTo).RemoveFromQueue(Data);
        }
    }

}
