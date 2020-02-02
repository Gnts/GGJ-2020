using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    Rigidbody _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(Vector3.forward*500f);
        _rb.AddForce(Vector3.up*200f);
    }
}
