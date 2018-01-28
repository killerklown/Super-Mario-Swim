using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    public GameObject gameOverText;
    public bool gameOver = false;
    public float scrollSpeed = -1.5f;
    private int score = 0;
    public Text scoreText;
    public Text recordText;
    public GameObject restartText;
    public AudioSource coinSound;

    public bool has_highscore = false;

    private HighScore highScore;

    void Awake() // before Start
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        LoadHighScore();
        recordText.text = "Hi-Score: " + highScore.highscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver == true && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title", LoadSceneMode.Single);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void BirdScored()
    {
        if(gameOver)
        {
            SaveHighScore(highScore.highscore);
            return;
        }
        score++;
        scrollSpeed -= 0.2f;
        scoreText.text = "Score: " + score.ToString();
        if(score > highScore.highscore)
        {
            highScore.highscore = score;
            recordText.text = "Hi-Score: " + highScore.highscore.ToString();
            SaveHighScore(highScore.highscore);
            LoadHighScore();
        }
        coinSound.Play();
    }

    public void LoadHighScore()
    {
        highScore = new HighScore();
        if(PlayerPrefs.HasKey("HighScore"))
        {
            has_highscore = true;
            highScore.highscore = PlayerPrefs.GetInt("HighScore");
        }
    }

    public void SaveHighScore(int s)
    {
        PlayerPrefs.SetInt("HighScore", s);
        PlayerPrefs.Save();
    }

    IEnumerator GameOverWait()
    {
        yield return new WaitForSeconds(5.0f);
        gameOver = true;
        restartText.SetActive(true);
    }

    public void BirdDied()
    {
        restartText.SetActive(false);
        gameOverText.SetActive(true);
        StartCoroutine("GameOverWait");
    }
}
