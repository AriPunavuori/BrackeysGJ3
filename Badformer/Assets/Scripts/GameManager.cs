using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    int lives;
    int livesStart = 3;
    int health;
    int healthStart = 100;
    int score;

    TextMeshProUGUI scoreText;
    TextMeshProUGUI healthText;
    TextMeshProUGUI livesText;
    TextMeshProUGUI uiText;

    float textTime = 3f;
    float textTimer;

    bool gameStarted;
    bool gameHalted;
    bool gameOver;
    bool gameWon;

    Coin[] coins;
    Hearth[] hearths;
    IUP[] iUPs;
    Shroom[] shrooms;

    void Start() {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        healthText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        livesText = GameObject.Find("LivesText").GetComponent<TextMeshProUGUI>();
        uiText = GameObject.Find("UIText").GetComponent<TextMeshProUGUI>();
        lives = livesStart;
        health = healthStart;
        SetScore(0);
        SetHealth(0);
        SetLives(0);
        SetUIText("Jiihaa");
        Randomizer();
    }

    void Update() {
        textTimer -= Time.deltaTime;
        if(textTimer < 0) {
            SetUIText("");
        }
    }

    public void SetUIText(string t) {
        uiText.text = t;
        textTimer = textTime;
    }

    public void SetScore(int s) {
        score += s;
        scoreText.text = "Score:" + score;
    }

    public void SetLives(int l) {
        lives += l;
        livesText.text = "Lives:" + lives;
        if(l == -1)
            SetUIText("A life was lost");
        if(l == 1)
            SetUIText("Extra life!!!");
    }

    public void SetHealth(int h) {
        health += h;
        if(health > 100)
            health = 100;
        healthText.text = "Health:" + health;
        if(health == 0) {
            SetLives(-1);
            SetHealth(healthStart);
        }
    }

    void Randomizer() {
        hearths = FindObjectsOfType<Hearth>();
        coins = FindObjectsOfType<Coin>();
        shrooms = FindObjectsOfType<Shroom>();
        iUPs = FindObjectsOfType<IUP>();

        foreach(Coin item in coins) {
            item.bad = Random.value > 0.5f;
        }
        foreach(Hearth item in hearths) {
            item.bad = Random.value > 0.5f;
        }
        foreach(Shroom item in shrooms) {

            var random = Random.Range(0f, 1f);

            if(random > .75f)
                item.ps = PlayerController.Playerstate.Jumper;
            else if(random > .5f)
                item.ps = PlayerController.Playerstate.MessUp;
            else if(random > .25f)
                item.ps = PlayerController.Playerstate.Slowdown;
            else 
                item.ps = PlayerController.Playerstate.Speedboost;
        }
        foreach(IUP item in iUPs) {
            item.bad = Random.value > 0.5f;
        }
    }
}
