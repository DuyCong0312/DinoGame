using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColor : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Color dayColor;    
    [SerializeField] private Color nightColor;   

    private bool isDay = true;
    public float colorChangeSpeed = 1.0f;  
    public int scoreThreshold = 100;    
    private int nextScoreThreshold;

    private void Awake()
    {
        camera = Camera.main;
        nextScoreThreshold = scoreThreshold; 
    }
    public void SetDayMode()
    {
        isDay = true;
        camera.backgroundColor = dayColor; 
    }
    private void Update()
    {
        if (GameManager.Instance.score >= nextScoreThreshold)
        {
            ToggleDayNightMode();
            nextScoreThreshold += scoreThreshold;  
        }

        Color targetColor = isDay ? dayColor : nightColor;
        camera.backgroundColor = Color.Lerp(camera.backgroundColor, targetColor, colorChangeSpeed * Time.deltaTime);
    }
    private void ToggleDayNightMode()
    {
        isDay = !isDay;
    }
}
