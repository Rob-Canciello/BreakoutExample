using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Update is called once per frame

    private float hInput;
    public float paddleSpeed = 2f;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        if(gameManager.gameStarted)
        {
            hInput = Input.GetAxisRaw("Horizontal");

            if (hInput != 0)
            {
                transform.Translate(new Vector3(hInput, 0, 0) * Time.deltaTime * paddleSpeed);
            }
        }
    }
}
