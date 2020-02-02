using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public State state;

    int nextKey;
    int seqIndex;

    int score;
    int lives;

    string[] keys = {"Fire1", "Fire2", "Fire3", "Jump"};

    // Start is called before the first frame update
    void Start()
    {
        state = State.Start;
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
                if(seqIndex < 5) {
                    NextKey();
                } else  {
                    state = State.Waiting;
                    waitTime = 3.0f;
                    score++;
                    Flamethrow.instance.Throw();
                }
            } else if (Input.anyKeyDown || targetTime < 0){
                lives --;
                waitTime = 2.0f;
                state = State.Waiting;

                if (lives < 1) {
                    state = State.GameOver;
                    timer.text = "Game Over!";
                    Officer.instance.Shoot();
                    newGameButton.enabled = true;
                }
            }
        } 
    }

    void NextSequence(){
        state = State.Repairing;
        targetTime = 10.0f;
        seqIndex = 0;
        NextKey();
    }

    void NextKey(){
        seqIndex++;
        nextKey = Random.Range(0, keys.Length);
        nextKeyText.rectTransform.anchoredPosition = new Vector3(Random.RandomRange(-100f, 100f), Random.RandomRange(-100f, 100f), 0);
        nextKeyText.text = keys[nextKey];
    }

    public void NewGame(){
        newGameButton.enabled = false;
        state = State.Repairing;
        score = 0;
        lives = 3;

        // Random.InitState()

        NextSequence();
    }
}
