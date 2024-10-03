using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float gameSpeed {  get; private set; }
    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float score { get; private set; }
    private int scoreMileStone = 100; 

    public GameObject gameOverPanel;
    public GameObject pauseMenuPanel;

    private AudioManager audioManager;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;
   
    private Player player;
    private Spawner spawner;
    private BackgroundColor backGround;

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
        backGround = FindObjectOfType<BackgroundColor>();

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
        pauseMenuPanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);

        backGround.SetDayMode();

        UpdateHiscore();
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        pauseMenuPanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(true);
       
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
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuPanel.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuPanel.gameObject.SetActive(false);
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        NewGame();
    }
}
