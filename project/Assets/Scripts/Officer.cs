using System;
using System.Collections;
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
        Repairman.instance.Die();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Shoot();
            StartCoroutine(ShootersGonnaShoot());
        }
    }

    IEnumerator ShootersGonnaShoot()
    {
        var src = GetComponent<AudioSource>();

        src.Stop();
        src.Play();
        yield return  new WaitForSeconds(0.3f);
        src.Stop();
        src.Play();
        yield return  new WaitForSeconds(0.3f);
        src.Stop();
        src.Play();
    }
    
}
