using System;
using System.Collections;
using UnityEngine;

public class Officer : MonoBehaviour
{
    Animator _anim;
    static readonly int k_Shoot = Animator.StringToHash("Shoot");
    static readonly int k_Hit = Animator.StringToHash("Hit");
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
        StartCoroutine(ShootersGonnaShoot());
    }

    public void Hit()
    {
        _anim.SetTrigger(k_Hit);
        Repairman.instance.TakeHit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Shoot();
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
