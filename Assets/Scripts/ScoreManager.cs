using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int playerScore = 0;
    public Text scoreUI;
    public int lives = 3;
    public Text livesUI;
    public GameObject gameOver;
    private PlayerController player;
    public float timeUp = 100f;
    public Text timeUI;
    public GameObject timer;
    public bool isTimeUp = false;
    public GameObject TimeUP;
    public GameObject waveChange;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); //access or communicate with player script
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isGameOver != true)                                  //if game over time will stop
        {
            timeUp -= Time.deltaTime;                                   //count time using delta time
            timeUI.text = "Timer: " + timeUp;                           //Display time in UI
            if (timeUp <= 50)                                           //after 50 second wave 2 start
            {
                waveChange.SetActive(true);                             //display wave 2 UI
            }
            if (timeUp <= 48)
            {
                waveChange.SetActive(false);                            //disable wave 2 UI 2 second after it display on screen

            }
            if (timeUp <= 0)                                            //if timer reach zero it mean level is completed
            {
                isTimeUp = true;                                        //bool to check that time is up
                timer.SetActive(false);                                 //remove timer UI
                TimeUP.SetActive(true);                                 //Display Level End UI on screen
            }
        }

    }
    //Score function which increment in score whene it call
    public void AddScore()
    {
        playerScore++;
        scoreUI.text = "Score: " + playerScore;
    }
    //adding living when player collect pink and blue box in game;
    public void Addlives()
    {
        lives++;
        livesUI.text = "Lives: " + lives;
    }
    //live is reduce when it collide with any ball
    public void LostLives()
    {
        lives--;
        livesUI.text = "Lives: " + lives;
        if (lives < 1)                      //if live is zero than Game Over function will be call
        {
            GameOver();
        }
    }
    //game over function
    public void GameOver()
    {
        player.isGameOver = true;                                               //check mean game is over now
        gameOver.SetActive(true);                                               //display gameover UI
        player.playAnim.SetBool("Death_b", true);                              //perform death animation
        player.playAnim.SetInteger("DeathType_int", 1);                         //this mean that player will fall backward
        player.deathExplosion.Play();                                           //spawn explosion partices
        player.playerAudio.PlayOneShot(player.deathSound, 1f);                  //play death sound when live = 0
        player.dirtParticle.Stop();                                             //stop dirt particles
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);             //it will reload the scene again
    }
    //time adder function when player collide with or collect watch in game it time is increase by 5 second;
    public void AddTime()
    {
        timeUp += 5;
        timeUI.text = "Timer: " + timeUp;
    }
}
