﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum State
{
    Start,
    Repairing,
    Waiting,
    GameOver,
}

public class RepairSequence : MonoBehaviour
{
    public float targetTime;

    float waitTime;

    public Button newGameButton;
    public Text timer;
    public Text scoreText;
    public Text livesText;
    public Text gameOverText;
    public Text nextKeyText;

    public GameObject zambiePrefab;

    public State state;

    int nextKey;
    int seqIndex;

    int score;
    int lives;

    string[] keys = {"Fire1", "Fire3", "Jump"};
    string[] keyNames = { "Ctrl", "Shift", "Space" };

    // Start is called before the first frame update
    void Start()
    {
        state = State.Start;
        scoreText.text = "";
        livesText.text = "";
        scoreText.text = "";
        nextKeyText.text = "";
        timer.text = "";
        newGameButton.GetComponentInChildren<Text>().text = "Play";
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text  = lives.ToString();
        scoreText.text = score.ToString();

        if  (state == State.Waiting) {
            nextKeyText.text = "";
            waitTime -= Time.deltaTime;
            if (waitTime < 0) {
                NextSequence();
            }
        }  else if (state == State.Repairing) {
            targetTime -= Time.deltaTime;
            timer.text = seqIndex.ToString() +":"+ targetTime.ToString();

            if (Input.GetButtonDown(keys[nextKey])) {
                if(seqIndex < 3) {
                    NextKey();
                } else  {
                    state = State.Waiting;
                    waitTime = 2.0f;
                    score++;
                    Flamethrow.instance.Throw();
                }
            } else if (Input.anyKeyDown || targetTime < 0){
                lives --;
                waitTime = 1.0f;
                state = State.Waiting;

                if (lives < 1) {
                    state = State.GameOver;
                    timer.text = "";
                    nextKeyText.text = "";
                    gameOverText.gameObject.SetActive(true);
                    Officer.instance.Shoot();
                    newGameButton.enabled = true;
                    newGameButton.gameObject.SetActive(true);
                    newGameButton.GetComponentInChildren<Text>().text = "New Game";
                }
                else
                {
                    Officer.instance.Hit();
                }
            }
        } 
    }

    IEnumerator SpawZombies()
    {
        for (int i=0; i< 10; i++)
        {
            yield return new WaitForSeconds(0.5f);
            Instantiate(zambiePrefab, new Vector3(Random.RandomRange(5, 25), -1, Random.RandomRange(50, 80)), Quaternion.Euler(0, 180, 0));
        }
    }

    void NextSequence(){
        StartCoroutine(SpawZombies());
        state = State.Repairing;
        targetTime = 3.0f;
        seqIndex = 0;
        NextKey();
    }

    void NextKey(){
        seqIndex++;
        nextKey = Random.Range(0, keys.Length);
        nextKeyText.rectTransform.anchoredPosition = new Vector3(Random.RandomRange(-100f, 100f), Random.RandomRange(-100f, 100f), 0);
        nextKeyText.text = keyNames[nextKey];
    }

    public void NewGame(){
        gameOverText.gameObject.SetActive(false);
        if (state == State.GameOver)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            Flamethrow.instance.Flame();
            newGameButton.gameObject.SetActive(false);
            newGameButton.enabled = false;
            state = State.Repairing;
            score = 0;
            lives = 3;

            // Random.InitState()

            NextSequence();
        }
    }
}
