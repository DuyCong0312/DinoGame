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
    public GameObject settingMenuPanel;

    [SerializeField] private AudioManager audioManager;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;
   
    private Player player;
    private Spawner spawner;
    private BackgroundColor backGround;

    private const string HiScore = "HiScore";
    private void Awake()
    {
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

    private void Update() 
    {
        UpdateSpeed();
        UpdateScore();
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
        settingMenuPanel.gameObject.SetActive(false);
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
        settingMenuPanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(true);
       
        UpdateHiscore();
    }

    private void UpdateSpeed()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;   
        if( gameSpeed >= 100)
        {
            gameSpeed = 100;
        }
    }

    private void UpdateScore()
    {
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
        float hiscore = PlayerPrefs.GetFloat(HiScore, 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat(HiScore, hiscore);
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
        settingMenuPanel.gameObject.SetActive(false);
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        NewGame();
    }

    public void SettingGame()
    {
        Time.timeScale = 0f;
        settingMenuPanel.gameObject.SetActive(true);
        pauseMenuPanel.gameObject.SetActive(false);
    }

    public void CloseSetting()
    {
        Time.timeScale = 0f;
        settingMenuPanel.gameObject.SetActive(false);
        pauseMenuPanel.gameObject.SetActive(true);
    }
}
