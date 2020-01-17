﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(CanvasGroup))]
public class CharacterUI : MonoBehaviour
{
    private CharacterSlot[] slots;
    private void Reset()
    {
        slots = GetComponentsInChildren<CharacterSlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].name = $"Char Slot {i + 1}";
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
            player.player_data.onCharacterItemChaged += OnCharacterChanged;
    }

    private void OnCharacterChanged(List<Item> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            slots[i].Set(items[i]);
        }
    }
}
