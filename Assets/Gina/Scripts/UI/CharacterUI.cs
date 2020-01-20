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
            player.player_data.onCharacterChanged += OnCharacterChanged;

        FindObjectOfType<InputController>().onCharacter += () =>
        {
            var cg = GetComponent<CanvasGroup>();
            if (cg.alpha >= 1)
                StartCoroutine(Hide());
            if (cg.alpha <= 0)
                StartCoroutine(Show());

        };
    }

    private void Start()
    {
        FindObjectOfType<InputController>().onCharacter += () =>
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
    }
    private IEnumerator Hide()
    {
        var cg = GetComponent<CanvasGroup>();
    a:
        cg.alpha -= 3f * Time.deltaTime;
        yield return new WaitForSeconds(0f);
        if (cg.alpha > 0)
            goto a;
    }

    private void OnCharacterChanged(Dictionary<paramname, object>[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            slots[i].Set(new Item(items[i]));
        }
    }
}
