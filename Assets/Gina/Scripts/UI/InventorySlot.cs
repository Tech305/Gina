﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class InventorySlot : Slot
{
    private Image icon;
    private TextMeshProUGUI count;

    public override void Awake()
    {
        if (!icon) icon = transform.Find("Icon").GetComponent<Image>();
        if (!count) count = transform.Find("Count").GetComponent<TextMeshProUGUI>();
    }
    public override void Update()
    {
        if(item != null &&  item.IsValid)
        {
            if (item.IsStackable)
            {
                if (item.Get<int>(paramname.curStack) > 1)
                    count.text = item.Get<int>(paramname.curStack).ToString();
                else
                    count.text = string.Empty;
            }
            else count.text = string.Empty;

            if(!string.IsNullOrEmpty(item.Get<string>(paramname.icon)))
            {
                icon.sprite = item.Get<Sprite>(paramname.icon);
                icon.enabled = true;
            }
            else
            {
                icon.enabled = false;
                icon.sprite = null;
            }
        }
        else
        {
            count.text = string.Empty;
            icon.enabled = false;
            icon.sprite = null;
        }
        
    }

    public override void OnDrop(PointerEventData eventData)
    {
        
    }

    public override void OnDrag(PointerEventData eventData)
    {
        
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        
    }
}
