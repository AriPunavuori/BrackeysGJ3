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
}
