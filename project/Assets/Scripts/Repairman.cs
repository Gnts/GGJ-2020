﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IKControl))]
public class Repairman : MonoBehaviour
{
    IKControl _ik;
    Animator _anim;
    public static Repairman instance;
    static readonly int k_Death = Animator.StringToHash("Death");
    static readonly int k_TakeHit = Animator.StringToHash("TakeHit");
    public GameObject particles;

    void Awake()
    {
        instance = this;
        _ik = GetComponent<IKControl>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() { }

    public void Die()
    {
        _anim.SetTrigger(k_Death);
        _ik.enabled = false;
        particles.SetActive(true);
    }

    public void TakeHit()
    {
        _anim.SetTrigger(k_TakeHit);
        var src = GetComponent<AudioSource>();
        src.Stop();
        src.Play();
    }

}
