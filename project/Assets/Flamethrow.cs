using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrow : MonoBehaviour
{
    public GameObject dynamo;
    public static Flamethrow instance;
    public Transform _transform;
    Animator _anim;
    
    
    
    void Awake()
    {
        instance = this;
        _anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Throw();
            _anim.SetTrigger("Shoot");       
        }
    }

    public void Throw()
    {
        Instantiate(dynamo, _transform.position, Quaternion.identity);
    }
    
}
