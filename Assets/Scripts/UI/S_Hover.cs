﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class S_Hover : MonoBehaviour
{

    [SerializeField]
    private Color m_Default_Color = new Color(188,188,188,255);
    [SerializeField]
    private Color m_Hover_Color = new Color(255, 255, 255, 255);
    [SerializeField]
    private TextMeshProUGUI buttonText;


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText) buttonText.color = m_Hover_Color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText) buttonText.color = m_Default_Color;
    }
}
