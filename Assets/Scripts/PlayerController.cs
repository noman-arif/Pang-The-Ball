using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public GameObject bulletPrefab;
    public Transform firePosition;
    public float xRange = 13f;
    public AudioSource playerAudio;
    public AudioClip shootSound;
    public AudioClip blastSound;
    public AudioClip deathSound;
    private ScoreManager score;
    public bool isGameOver = false;
    public Animator playAnim;
    public ParticleSystem dirtParticle;
    public ParticleSystem deathExplosion;
    public bool isRunning=false;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playAnim = GetComponent<Animator>();
        playAnim.SetFloat("Speed_f", 0.1f);
        score = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //this code is define a Movement range for our player in the game mean player can not cross the certain boundries.
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        //this code is making player to move along x axis until time up or game is over
        if (isGameOver != true && score.isTimeUp != true)
        {
            float horizontalInput = Input.GetAxis("Horizontal");            //taking horizontal Input
            Vector3 movePlayer = new Vector3(horizontalInput, 0f, 0f);      //storing than input in a vector 
            if (movePlayer.magnitude > 0.1)                                 //taking magnitude of the vector which is not neccesary but in my case
                                                                            //my code working so i didnt remove it but if you remove your code will
                                                                            //work.
            {
                transform.Translate(movePlayer * speed * Time.deltaTime, Space.World); //now here the thing Space.World this will make our player unbound from axis
                                                                                      //means it ignore rotation of a player 
                transform.rotation = Quaternion.LookRotation(movePlayer);             //to rotate player based on which key is press if left it will face on left similar for right key
                dirtParticle.Play();                                                  //display dirt particle
                playAnim.SetFloat("Speed_f", 0.6f);                                   //perform runing animation
                playAnim.SetBool("Static_b", true);                                   //player is on static walk animation
            }
            else
            {

                playAnim.SetFloat("Speed_f", 0.1f);                                    //if player is not moving it animation will sset to idle
                dirtParticle.Stop();                                                  //display dirt particle
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(bulletPrefab, firePosition.position, bulletPrefab.transform.rotation);  //fire bullet or spawn bullet hehe
                playerAudio.PlayOneShot(shootSound, 1f);                                            //play shot sound whenever player press space bar
                playAnim.SetInteger("WeaponType_int", 6);                                           //using the 6 weapon from animation
                playAnim.SetBool("Shoot_b", true);                                                  //perform shoot animation kind of hehe

            }
            else
            {
                playAnim.SetBool("Shoot_b", false);                                                 //make animation shoot to false if space key is not press 
                playAnim.SetInteger("WeaponType_int", 0);                                           //make weapon selection to zero again its not complusory but 
                                                                                                    //for safe side i did this
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Big") || collision.gameObject.CompareTag("Medium") || collision.gameObject.CompareTag("Small"))
        {
            score.LostLives();                                                  //if player collide with any size of ball it lostlive functionn will be call
        }
        else if (collision.gameObject.CompareTag("Clock"))
        {
            score.AddTime();                                                    //han g bhai time add kr rha ap sahi samjy
            Destroy(collision.gameObject);                                      //or phir usko destroy bhi kr rha taky kabab ma hadi na bany player k lia
        }
        else if (collision.gameObject.CompareTag("Life"))
        {
            score.Addlives();                                                   //add lives when use collect that specific beautiful made by me object in game
            Destroy(collision.gameObject);                                      //dukh k saat likin wo bhi destroy hujana collect k baad
        }
    }
}

