using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zamby : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Officer.instance.transform.position) < 3f)
        {
            GetComponent<Animator>().SetTrigger("Attack");
        }
    }
}
