using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    public float resetPosition = -20f;
    public float startPosition = 20f;

    private Vector2 initialPosition;
    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector2.left * GameManager.Instance.gameSpeed * Time.deltaTime);

        if (transform.position.x < resetPosition)
        {
            transform.position = new Vector2(startPosition, transform.position.y);
        }
    }

}
