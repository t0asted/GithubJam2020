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
    private Button m_ButtonConstructableItem;

    private S_InGameMenuBase ToPassBackTo;

    private CL_ItemConstructable Data;

    public void SetupContructableItem(CL_ItemConstructable DataPass, S_InGameMenuBase rocketUIPass)
    {
        Data = DataPass;
        ToPassBackTo = rocketUIPass;
        if(m_ButtonConstructableItem != null)
        {
            Debug.Log("Setup button!");
            m_ButtonConstructableItem.onClick.AddListener(AddToRenderQueue);
        }
        if(m_TextConstructableItem != null)
        {
            m_TextConstructableItem.SetText(Data.ResourceName.ToString() + " " + Data.Quantity);
        }
        if(Text_ItemsToContruct != null)
        {
            Text_ItemsToContruct.SetText(ItemsToCreate());
        }
    }

    public void AddToRenderQueue()
    {
        if(ToPassBackTo is S_MachineBuilder)
        {
            ((S_MachineBuilder)ToPassBackTo).AddToBuildQueue(Data);
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
