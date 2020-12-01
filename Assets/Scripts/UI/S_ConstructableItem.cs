using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class S_ConstructableItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_TextConstructableItem = null;
    [SerializeField]
    private TextMeshProUGUI Text_ItemsToContruct = null;
    [SerializeField]
    private TextMeshProUGUI m_TextConstructableItemQuantity = null;
    [SerializeField]
    private Button m_ButtonConstructableItem = null;
    [SerializeField]
    private Image m_Sprite = null;

    private S_InGameMenuBase ToPassBackTo;
    private CL_ItemConstructable Data;

    //private void Update()
    //{
    //    m_ButtonConstructableItem.interactable = ToPassBackTo.HasResourcesToMakeItem(Data.CostToBuild);
    //}

    public void SetupContructableItem(CL_ItemConstructable DataPass, S_InGameMenuBase ToPassBackToPass)
    {
        Data = DataPass;
        ToPassBackTo = ToPassBackToPass;
        if(m_ButtonConstructableItem != null)
        {
            m_ButtonConstructableItem.onClick.AddListener(AddToRenderQueue);
        }
        if (m_TextConstructableItem != null)
        {
            m_TextConstructableItem.SetText(Data.ResourceName.ToString());
        }
        if(m_TextConstructableItemQuantity != null)
        {
            m_TextConstructableItemQuantity.SetText(Data.Quantity.ToString());
        }
        if (Text_ItemsToContruct != null)
        {
            Text_ItemsToContruct.SetText(ItemsToCreate());
        }
        if (m_Sprite != null)
        {
            if(Data.Sprite)
            {
                m_Sprite.sprite = Data.Sprite;
            }
        }
    }

    public void AddToRenderQueue()
    {
        if(ToPassBackTo.HasResourcesToMakeItem(Data.CostToBuild))
        {
            if (ToPassBackTo is S_MachineBuilder)
            {
                ((S_MachineBuilder)ToPassBackTo).AddToBuildQueue(Data);
            }
        }
    }

    private string ItemsToCreate()
    {
        string itemsToCreate = "";

        foreach (var item in Data.CostToBuild)
        {
            itemsToCreate = itemsToCreate + item.ResourceName.ToString() + " : " + item.Quantity + "\n";
        }

        return itemsToCreate;
    }

}
