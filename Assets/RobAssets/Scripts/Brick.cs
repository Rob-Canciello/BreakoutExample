using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Sprite normal;
    [SerializeField] private Sprite damaged;

    public int health = 2;
    public int scoreValue = 100;

    private GameManager gameManager;

    void Awake()
    {
        if(gameManager == null)
        {
            gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        }
    }

    public void ResetSprite()
    {
        spriteRenderer.sprite = normal;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("I got hit!");
        if (collision.gameObject.tag == "Ball")
        {
            health--;

            if (health == 1)
            {
                spriteRenderer.sprite = damaged;
            }

            if (health <= 0)
            {
                GameManager.gameScore += scoreValue;
                gameManager.UpdateUI();
                this.gameObject.SetActive(false);
            }
        }
    }
}
