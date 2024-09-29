using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed {  get; private set; }

    private AudioManager audioManager;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;
    public Button retryButton;

    private Player player;
    private Spawner spawner;

    private float score;
    private int scoreMileStone = 100;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    { 
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        NewGame();
    }

    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        
        foreach (var obstacle in obstacles )
        {
            Destroy( obstacle.gameObject );
        }

        score = 0f;
        gameSpeed = initialGameSpeed;
        enabled = true;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        UpdateHiscore();
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        UpdateHiscore();
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.RoundToInt(score).ToString("D5");

        if ( score > scoreMileStone)
        {
            audioManager.PlaySFX(audioManager.scoreReachClip);
            scoreMileStone += 100;
        }
    }
    private void UpdateHiscore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }

        hiscoreText.text = Mathf.RoundToInt(hiscore).ToString("D5");
    }
}
