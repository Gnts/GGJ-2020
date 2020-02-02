using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairSequence : MonoBehaviour
{
    public float targetTime = 60.0f;

    public Text timer;
    public Text score;
    public Text nextKey;

    public bool started, failed, repaired, died;

    string[] keys = {"Fire1", "Fire2", "Fire3", "Jump"};

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;
        timer.text = targetTime.ToString();
        if (Input.GetButtonDown("Fire1")) {
            targetTime = 100.0f;
        }
    }

    public void NewGame(){
        targetTime = 60.0f;
    }
}
