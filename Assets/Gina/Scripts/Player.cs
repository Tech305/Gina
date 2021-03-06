﻿using CodeMonkey.Utils;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string player_name;
    public Data data = new Data();

    private void Awake()
    {
        data.Name = player_name;
        data.onNameChaged += (n) => { player_name = n; gameObject.name = n; };
        data.onPositionChanged += (p) => transform.position = p;
        data.onRotationChanged += (r) => transform.rotation = Quaternion.Euler(r);
    }

    private void Start()
    {
        data.Load();
        FunctionPeriodic.Create(() => data.Save(), 10);
    }

    private void OnApplicationQuit()
    {
        data.Save();
    }
    private void Update()
    {
        data.Position = transform.position;
        data.Rotation = transform.rotation.eulerAngles;
    }
}
