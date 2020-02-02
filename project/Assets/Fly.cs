﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    Rigidbody _rb;
    public GameObject go;
    public GameObject go2;
    float t;
    bool exploded;
    static readonly int k_Death = Animator.StringToHash("Death");

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(Vector3.forward*500f);
        _rb.AddForce(Vector3.up*200f);
    }

    void Update()
    {
        t += Time.deltaTime;
        if (t > 2f && !exploded)
        {
            Explode();
        }
    }

    void Explode()
    {
        go.SetActive(true);
        go2.SetActive(true);
        exploded = true;

        var rhs = Physics.SphereCastAll(transform.position, 10f, transform.position, 1f);

        foreach (var rh in rhs)
        {
            if (rh.collider.gameObject.CompareTag("Zamby"))
            {
                rh.collider.gameObject.AddComponent<Rigidbody>();
                var rb = rh.collider.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(rh.collider.transform.position * 50f);
                rh.collider.GetComponent<Animator>().SetTrigger(k_Death);
            }
        }
        
    }
}