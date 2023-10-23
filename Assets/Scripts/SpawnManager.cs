using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public float xRange = 15f;
    public float yRange = 19f;
    public float delayTime = 2f;
    public float repeatRate = 5f;
    public float clockdelay = 10f;
    public float clockRepeatInterval = 10f;
    private PlayerController player;
    public GameObject[] giftPrefab;
    private ScoreManager score;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnBall", delayTime, repeatRate);                                        //spawn ball after fixed time 
        InvokeRepeating("SpawnClock", clockdelay, clockRepeatInterval);                             //spawn lives and time after fix time
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();       //access the player script
        score = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();      //access the score script
    }

    // Update is called once per frame
    void Update()
    {

    }
    //this fucntion will instantiate Ball until game is not or time is not up;
    void SpawnBall()
    {
        if (player.isGameOver != true && score.isTimeUp != true)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-xRange, xRange), yRange, 0);
            Instantiate(ballPrefab, spawnPos, ballPrefab.transform.rotation);
        }
    }
    //this fucntion will instantiate collectable until game is not or time is not up;
    void SpawnClock()
    {
        if (player.isGameOver != true && score.isTimeUp != true)
        {
            int index = Random.Range(0, giftPrefab.Length);
            Vector3 spawnPos = new Vector3(Random.Range(-xRange, xRange), yRange, 0);
            Instantiate(giftPrefab[index], spawnPos, giftPrefab[index].transform.rotation);
        }
    }

}
