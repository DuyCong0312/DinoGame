using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float leftEdge;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector2.zero).x - 2f;
    }
    private void Update()
    {
        transform.Translate(Vector2.left * GameManager.Instance.gameSpeed * Time.deltaTime);

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
}

