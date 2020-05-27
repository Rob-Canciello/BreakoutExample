using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip paddleHit;
    [SerializeField] private AudioClip wallHit;
    [SerializeField] private AudioClip brickHit;
    [SerializeField] private AudioClip ballMissed;

    [Header("Movement")]
    [SerializeField] private Rigidbody2D rb;
    // We leave this public so the GameManager or other objects can modify it later.
    public float moveSpeed = 4f;
    public GameObject paddleParticles;

    private float savedSpeed;
    private Vector2 randomDirection;
    private Vector2 startPos;
    private SpriteRenderer renderer;
    private CircleCollider2D collider;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

        savedSpeed = moveSpeed;
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<CircleCollider2D>();
        startPos = transform.position;
    }

    private void LateUpdate()
    {
        rb.velocity = moveSpeed * (rb.velocity.normalized);
    }

    private void OnEnable()
    {
        collider.enabled = true;
        renderer.enabled = true;
        Spawn();
    }

    void Spawn()
    {
        transform.position = startPos;
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-0.1f, -0.7f));
        rb.velocity = randomDirection * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(paddleHit);
            GameObject go = Instantiate(paddleParticles, collision.transform.position, Quaternion.identity);
        }

        if (collision.gameObject.tag == "Brick")
        {
            moveSpeed = savedSpeed;
            audioSource.PlayOneShot(brickHit);
        }

        if (collision.gameObject.tag == "Wall")
        {
            audioSource.PlayOneShot(wallHit);
        }

        if (collision.gameObject.tag == "Killzone")
        {
            gameManager.ballCount--;
            gameManager.UpdateUI();
            audioSource.PlayOneShot(ballMissed);
            collider.enabled = false;
            renderer.enabled = false;
            rb.velocity = Vector2.zero;
            StartCoroutine(Respawn());
            // START RESET BALL COROUTINE AND talk with gamemanager to LOWER BALL COUNT
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1f);
        this.enabled = false;

        yield return new WaitForSeconds(0.75f);

        this.enabled = true;
        collider.enabled = true;
        renderer.enabled = true;
    }
}
