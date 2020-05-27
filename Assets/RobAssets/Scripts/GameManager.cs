using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gameStartAudio;
    [SerializeField] private AudioClip gameOverAudio;

    public GameObject menuCanvas;
    public GameObject gameCanvas;
    public GameObject playButton;

    public TextMeshProUGUI gameScoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI highScoreText;
    
    public int ballCount = 4;
    private int savedBallCount;

    public static int gameScore = 0;
    public static int highScore = 0;

    public bool gameStarted = false;

    [SerializeField] private Ball ballRef;
    [SerializeField] private GameObject ballObject;
    private GameObject[] bricks;
   
    // Start is called before the first frame update
    void Awake()
    {
        UpdateUI();
        gameCanvas.SetActive(false);
        ballRef.enabled = false;
        savedBallCount = ballCount;
        DontDestroyOnLoad(this.gameObject);
        bricks = GameObject.FindGameObjectsWithTag("Brick");
    }

    private void Update()
    {
        if(ballCount <= 0 && gameStarted)
        {
            StartCoroutine(EndGame());
        }
    }

    public void StartGame()
    {
        HideMenu();
        ballRef.enabled = true;
        ballObject.SetActive(true);
        gameStarted = true;
        audioSource.PlayOneShot(gameStartAudio);
        gameScore = 0;
        ballCount = savedBallCount;
       
    }

    IEnumerator EndGame()
    {
        audioSource.PlayOneShot(gameOverAudio);
        yield return new WaitForEndOfFrame();
        ballRef.enabled = false;
        ballObject.SetActive(false);
        gameStarted = false;
        
        DisplayMenu();

        for (int i = 0; i < bricks.Length; i++)
        {
            bricks[i].GetComponent<Brick>().ResetSprite();
            bricks[i].SetActive(true);
        }

        // PLAY SOUND

        if(gameScore > highScore)
        {
            highScore = gameScore;
            highScoreText.text = "High Score: " + highScore;
        }
    }

    void DisplayMenu()
    {
        gameCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }

    void HideMenu()
    {
        menuCanvas.SetActive(false);
        gameCanvas.SetActive(true);
    }

    public void UpdateUI()
    {
        gameScoreText.text = "Score: " + gameScore;
        livesText.text = "Lives: " + ballCount;
    }
}
