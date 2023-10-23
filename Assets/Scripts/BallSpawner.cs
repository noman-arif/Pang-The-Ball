using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject mediumBall;
    public GameObject smallBall;
    private Vector3 offset = new Vector3(1.5f, 0, 0);
    private Vector3 smallOffset = new Vector3(0.9f, 0, 0);
    private PlayerController player;
    private ScoreManager score;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); //access player script
        score = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();//access score script

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        //this if statement will instantiate two medium ball at different when large ball collide with bullet increment score and play bullet shot sound 
        //as well as destroy big ball as well as bullet
        if (collision.gameObject.CompareTag("Big"))
        {
            Instantiate(mediumBall, transform.position + offset, mediumBall.transform.rotation);
            Instantiate(mediumBall, transform.position - offset, mediumBall.transform.rotation);
            Destroy(gameObject);
            Destroy(collision.gameObject);
            player.playerAudio.PlayOneShot(player.blastSound, 1f);
            score.AddScore();
        }
        //this if statement will instantiate two Small balls at different position when medium ball collide with bullet increment score and play bullet shot sound 
        //as well as destroy big ball as well as bullet
        else if (collision.gameObject.CompareTag("Medium"))
        {
            Instantiate(smallBall, transform.position + smallOffset, smallBall.transform.rotation);
            Instantiate(smallBall, transform.position - smallOffset, smallBall.transform.rotation);
            Destroy(gameObject);
            Destroy(collision.gameObject);
            player.playerAudio.PlayOneShot(player.blastSound, 1f);
            score.AddScore();
        }
        //this if statement will destroy small ball collide with bullet increment score and play bullet shot sound as well as destroy big ball as well as bullet
        else if (collision.gameObject.CompareTag("Small"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            player.playerAudio.PlayOneShot(player.blastSound, 1f);
            score.AddScore();
        }

    }
}
