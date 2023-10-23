using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPush : MonoBehaviour
{
    private Rigidbody rb;
    public float jumpForce = 10f;
    public float sideForce = 20f;
    public float playerHitForce = 20f;
    private PlayerController player;
    private ScoreManager score;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();                                                         //access rigidbody of the game object with whom this script is attached
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();   //access player script
        score = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();  //access score script
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isGameOver != false || score.isTimeUp == true)                                  //this if statement will destroy ball in scene when game is over 
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (player.isGameOver != true && score.isTimeUp != true)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);                 //when ball collide with ground it bounce back
            }
            else if (collision.gameObject.CompareTag("Player"))
            {
                rb.AddForce(Vector3.up * playerHitForce, ForceMode.Impulse);            //when ball collide with Player it bounce back
            }
            else if (collision.gameObject.CompareTag("LeftWall"))
            {
                rb.AddForce(Vector3.right * sideForce, ForceMode.Impulse);              //when ball collide with left invisible  it bounce right
            }
            else if (collision.gameObject.CompareTag("RightWall"))
            {
                rb.AddForce(Vector3.left * sideForce, ForceMode.Impulse);               //when ball collide with right invisible wall it bounce left
            }
        }

    }
}
