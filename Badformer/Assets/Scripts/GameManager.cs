using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    Vector3 startpoint = new Vector3(0,0,0);
    Vector3 checkpoint;

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

    Pickable[] pickables;
    HealthKit[] healthKits;
    IUP[] iUPs;
    Powerup[] powerups;

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
        SetUIText("Run Forrest, RUN!");
        Randomizer();
    }

    void Update() {
        textTimer -= Time.deltaTime;
        if(textTimer < 0) {
            SetUIText("");
        }
        if(Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(0);
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
        if(lives < 0) {
            SceneManager.LoadScene(0);
            SetUIText("Game over! =(");
        }
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

    public void SetCheckpoint(Vector3 c) {
        checkpoint = c;
    }

    void Randomizer() {
        healthKits = FindObjectsOfType<HealthKit>();
        pickables = FindObjectsOfType<Pickable>();
        powerups = FindObjectsOfType<Powerup>();
        iUPs = FindObjectsOfType<IUP>();

        foreach(Pickable item in pickables) {
            item.bad = Random.value > 0.5f;
        }
        foreach(HealthKit item in healthKits) {
            item.bad = Random.value > 0.5f;
        }
        foreach(Powerup item in powerups) {

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

    public Vector3 Checkpoint() {
        return checkpoint;
    }
}
