using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColor : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Color dayColor;    
    [SerializeField] private Color nightColor;   

    public bool isDay = true;
    public float colorChangeSpeed = 1.0f;  
    public int scoreThreshold = 100;

    private void Awake()
    {
        camera = Camera.main;
    }
    public void SetDayMode()
    {
        isDay = true;
        camera.backgroundColor = dayColor;
        scoreThreshold = 100;
}
    private void Update()
    {
        CheckScore();   
        ChangeColor();
    }
    private void ToggleDayNightMode()
    {
        isDay = !isDay;
    }

    private void CheckScore()
    {
        if (GameManager.Instance.score >= scoreThreshold)
        {
            ToggleDayNightMode();
            scoreThreshold += 100;
        }
    }

    private void ChangeColor()
    {
        Color targetColor = isDay ? dayColor : nightColor;
        camera.backgroundColor = Color.Lerp(camera.backgroundColor, targetColor, colorChangeSpeed * Time.deltaTime);
    }

}
