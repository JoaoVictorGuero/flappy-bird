using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int maxScore = 0;
    private int score = 0;

    public static float currentSpeed;
    public static float relativeSpeed;
    public static bool canMove;

    [Header("Basic")]
    [SerializeField] private TMPro.TMP_Text scoreText;
    [SerializeField] private TMPro.TMP_Text recordScoreText;
    [SerializeField] private TMPro.TMP_Text matchScoreText;
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject gameoverUI;
    [Header("Game speed")]
    [SerializeField] private float increaseSpeedRate = 1f;
    [SerializeField] private float minSpeed = 2f;
    [SerializeField] private float maxSpeed = 4f;
    [Header("Particles")]
    public GameObject effectParticle;
    [Header("Music")]
    public AudioSource backgroundMusic;


    public static GameManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        resetSpeed();
        startGame();
    }
    void Update()
    {
        if (Player.isMoving && !PowerUp.timeEffectIsActive)
        {
            canMove = currentSpeed >= maxSpeed;
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += increaseSpeedRate * Time.deltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
                relativeSpeed = currentSpeed / minSpeed;
            }
        }
    }

    public void resetSpeed()
    {
        currentSpeed = minSpeed;
        relativeSpeed = 1;
        Player.position = 0;
        canMove = false;
    }
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
        if (score > maxScore) {
            maxScore = score;
            PlayerPrefs.SetInt("MAX_SCORE", maxScore);
        } 
    }
    public void startGame(bool isStarting = false)
    {
        maxScore = PlayerPrefs.HasKey("MAX_SCORE") ? PlayerPrefs.GetInt("MAX_SCORE") : 0;
        scoreText.enabled = isStarting;
        recordScoreText.text = maxScore.ToString();
        gameoverUI.SetActive(false);
        startUI.SetActive(!isStarting);
        if (isStarting) backgroundMusic.Play();
    }
    public void gameOver()
    {
        backgroundMusic.Stop();
        scoreText.enabled = false;
        matchScoreText.text = score.ToString();
        gameoverUI.SetActive(true);
        startUI.SetActive(false);
    }
}
