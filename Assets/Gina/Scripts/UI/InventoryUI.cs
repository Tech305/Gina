﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryUI : MonoBehaviour
{
    private GridLayoutGroup grid;
    public InventorySlot[] slots;
    private void Reset()
    {
        grid = GetComponentInChildren<GridLayoutGroup>();
        slots = grid.GetComponentsInChildren<InventorySlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].name = $"Inv Slot {i + 1}";
            slots[i].Set();
            slots[i].Awake();
            slots[i].Update();
        }
    }

    private void Awake()
    {
        if (slots == null || slots.Length <= 0) Reset();
        var player = FindObjectOfType<Player>();
        if (player)
            player.data.inventory.onChanged += Inventory_onChanged;
   
        FindObjectOfType<InputController>().onInventory += () =>
        {
            var cg = GetComponent<CanvasGroup>();
            if (cg.alpha >= 1)
                StartCoroutine(Hide());
            if (cg.alpha <= 0)
                StartCoroutine(Show());

        };
    }

    private IEnumerator Show()
    {
        var cg = GetComponent<CanvasGroup>();
    a:
        cg.alpha += 3f * Time.deltaTime;
        yield return new WaitForSeconds(0f);
        if (cg.alpha < 1)
            goto a;
        cg.blocksRaycasts = true;
    }
    private IEnumerator Hide()
    {
        var cg = GetComponent<CanvasGroup>();
    a:
        cg.alpha -= 3f * Time.deltaTime;
        yield return new WaitForSeconds(0f);
        if (cg.alpha > 0)
            goto a;
        cg.blocksRaycasts = false;
    }

    private void Inventory_onChanged(Dictionary<string, object>[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            slots[i].item = new Item(items[i]);
        }
    }
}
