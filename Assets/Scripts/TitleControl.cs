using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleControl : MonoBehaviour {

    public GameObject startText;
    public AudioSource swimSound;
    public Camera mycam;
    private int highscore;
    public Text recordText;

    //flag to determine if you want the blinking to happen
    bool isBlinking = true;

    void Start()
    {
        highscore = GetHighScore();
        recordText.text = "HighScore: " + highscore;
        //Call coroutine BlinkText on Start
        StartCoroutine(BlinkText());
        //call function to check if it is time to stop the flashing.
        //StartCoroutine(StopBlinking());
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            swimSound.Play();
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public int GetHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            return PlayerPrefs.GetInt("HighScore");
        }
        return 0;
    }

    //function to blink the text 
    IEnumerator BlinkText()
    {
        //blink it forever. You can set a terminating condition depending upon your requirement. Here you can just set the isBlinking flag to false whenever you want the blinking to be stopped.
        while (isBlinking)
        {
            //set the Text's text to blank
            startText.SetActive(false);
            //display blank text for 0.5 seconds
            yield return new WaitForSeconds(1f);
            //display “I AM FLASHING TEXT” for the next 0.5 seconds
            startText.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }
    //your logic here. I have set the isBlinking flag to false after 5 seconds
    IEnumerator StopBlinking()
    {
        //wait for 5 seconds
        yield return new WaitForSeconds(5f);
        //stop the blinking
        isBlinking = false;
        //set a different text just for sake of clarity
        startText.SetActive(false);
    }
}
