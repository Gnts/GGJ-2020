﻿using System;
using UnityEngine;

public class Officer : MonoBehaviour
{
    Animator _anim;
    static readonly int k_Shoot = Animator.StringToHash("Shoot");
    public static Officer instance;

    void Awake()
    {
        instance = this;
        _anim = GetComponent<Animator>();
    }

    public void Shoot()
    {
        _anim.SetTrigger(k_Shoot);
    }
}