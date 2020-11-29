using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class S_ConstructableItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_TextConstructableItem;
    [SerializeField]
    private Button m_ButtonConstructableItem;

    private S_InGameMenuBase ToPassBackTo;

    private CL_ItemConstructable Data;

    public void SetupContructableItem(CL_ItemConstructable DataPass, S_InGameMenuBase rocketUIPass)
    {
        Data = DataPass;
        ToPassBackTo = rocketUIPass;
        if(m_ButtonConstructableItem != null)
        {
            m_ButtonConstructableItem.onClick.AddListener(delegate { AddToRenderQueue(); });
        }
        if(m_TextConstructableItem != null)
        {
            m_TextConstructableItem.SetText(Data.ResourceName.ToString());
        }
    }

    public void AddToRenderQueue()
    {
        ToPassBackTo.AddToBuildQueue(Data);
    }

}
