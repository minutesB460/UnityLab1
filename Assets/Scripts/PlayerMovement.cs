using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody2D marioBody;

    //Variable for Impulse force upwards
    public float upSpeed = 10;
    private bool onGroundState = true;
    public float maxSpeed = 20;


    // global variables
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    // public TextMeshProUGUI scoreText;
    // public TextMeshProUGUI scoreTextOver;
    public GameObject enemies;

    // public GameObject gameOverUI;
    GameManager gameManager;
    public JumpOverGoomba jumpOverGoomba;

    private Animator marioAnimator;

    public AudioSource marioAudio;

    // public AudioClip marioDeath;
    public AudioSource marioDeathAudio;
    public float deathImpulse = 15;

    // state
    [System.NonSerialized]
    public bool alive = true;
    public Transform gameCamera;

    private bool jumpedState = false;
    private bool moving = false;
    
    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        
        marioSprite = GetComponent<SpriteRenderer>();

        // gameOverUI.SetActive(false);
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();

        marioAnimator = GetComponent<Animator>();
        // update animator state
        marioAnimator.SetBool("onGround", onGroundState);

    }

    // Update is called once per frame
    // void Update()
    // {
    //     // Flip Mario based on diff keys
    //     // Do not implement the flipping of Sprite under FixedUpdate since it has nothing to do with the Physics Engine.
    //     // toggle state
    //     if (Input.GetKeyDown("a") && faceRightState)
    //     {
    //         faceRightState = false;
    //         marioSprite.flipX = true;

    //         if (marioBody.linearVelocity.x > 0.1f)
    //             marioAnimator.SetTrigger("onSkid");
    //     }

    //     if (Input.GetKeyDown("d") && !faceRightState)
    //     {
    //         faceRightState = true;
    //         marioSprite.flipX = false;

    //         if (marioBody.linearVelocity.x < -0.1f)
    //             marioAnimator.SetTrigger("onSkid");
    //     }

    //     marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.linearVelocity.x));
    // }

    // FixedUpdate is called 50 times a second

    // //FixedUpdate may be called once per frame. See documentation for details.
    // void FixedUpdate()
    // {
    //     if (alive)
    //     {
    //         float moveHorizontal = Input.GetAxisRaw("Horizontal");

    //         if (Mathf.Abs(moveHorizontal) > 0)
    //         {
    //             Vector2 movement = new Vector2(moveHorizontal, 0);
    //             // check if it doesn't go beyond maxSpeed
    //             if (marioBody.linearVelocity.magnitude < maxSpeed)
    //                 marioBody.AddForce(movement * speed);
    //         }

    //         // stop
    //         if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
    //         {
    //             // stop
    //             marioBody.linearVelocity = Vector2.zero;
    //         }

    //         //If spacebar is pressed, we will add an Impulse force upwards.
    //         if (Input.GetKeyDown("space") && onGroundState)
    //         {
    //             marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
    //             onGroundState = false;
    //         }

    //         // update animator state
    //         marioAnimator.SetBool("onGround", onGroundState);

    //     }

    // }
   void FixedUpdate()
    {
        if (alive && moving)
        {
            Move(faceRightState == true ? 1 : -1);
        }
    }

    void Update()
    {
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.linearVelocity.x));
    }

    void FlipMarioSprite(int value)
    {
        if (value == -1 && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.linearVelocity.x > 0.1f)
                marioAnimator.SetTrigger("onSkid");

        }

        else if (value == 1 && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.linearVelocity.x < -0.1f)
                marioAnimator.SetTrigger("onSkid");
        }
    }

    void Move(int value)
    {
        Vector2 movement = new Vector2(value, 0);
        // check if it doesn't go beyond maxSpeed
        if (marioBody.linearVelocity.magnitude < maxSpeed)
            marioBody.AddForce(movement * speed);
    }



    public void MoveCheck(int value)
    {
        if (value == 0)
        {
            moving = false;
            marioBody.linearVelocity = new Vector2(0, marioBody.linearVelocity.y); // Stop instantly
        }
        else
        {
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }


    public void Jump()
    {
        if (alive && onGroundState)
        {
            // jump
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);

        }
    }
    public void JumpHold()
    {
        if (alive && jumpedState)
        {
            // jump higher
            marioBody.AddForce(Vector2.up * upSpeed * 50, ForceMode2D.Force);
            jumpedState = false;

        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        // if (col.gameObject.CompareTag("Ground")) onGroundState = true;

        if ((col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Obstacle")) && !onGroundState)
        {
            onGroundState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Enemy") && alive)
    //     {
    //         // play death animation
    //         marioAnimator.Play("Mario_death");
    //         // marioAudio.PlayOneShot(marioDeath);

    //         marioDeathAudio.PlayOneShot(marioDeathAudio.clip);

    //         Debug.Log("Collided with goomba!");
    //         Time.timeScale = 0.0f;

    //         // gameOverUI.SetActive(true);
    //         gameManager.GameOver();

    //         alive = false;
    //     }
    // }

    void OnTriggerEnter2D(Collider2D other) 
{
    if (other.gameObject.CompareTag("Enemy") && alive)
    {
        Rigidbody2D marioRb = GetComponent<Rigidbody2D>();

        float marioY = transform.position.y;
        float goombaY = other.transform.position.y;

        // Mario on top of Goomba and the velocity is downwards
        if (marioY > goombaY + 0.2f && marioRb.linearVelocity.y < 0) 
        {
            gameManager.IncreaseScore(1); 

            // play goomba death animation
            EnemyMovement goomba = other.gameObject.GetComponent<EnemyMovement>();
            goomba.Stomped(); 

            //Mario bounce
            marioRb.linearVelocity = new Vector2(marioRb.linearVelocity.x, 10f); 
        }

        else
        {
            // play death animation
            marioAnimator.Play("Mario_death");
            // marioAudio.PlayOneShot(marioDeath);

            marioDeathAudio.PlayOneShot(marioDeathAudio.clip);

            Debug.Log("Collided with goomba!");
            Time.timeScale = 0.0f;

            // gameOverUI.SetActive(true);
            gameManager.GameOver();

            alive = false;
        }
    }
}

    // public void RestartButtonCallback(int input)
    // {
    //     Debug.Log("Restart!");

    //     gameOverUI.SetActive(false);
    //     // reset everything
    //     ResetGame();
    //     // resume time
    //     Time.timeScale = 1.0f;
    // }

    // private void ResetGame()
    // {

    //     // reset position
    //     marioBody.transform.position = new Vector3(-3f, -0.72f, 0.0f);
    //     // reset sprite direction
    //     faceRightState = true;
    //     marioSprite.flipX = false;
    //     // reset score
    //     scoreText.text = "Score: 0";
    //     scoreTextOver.text = "Score: 0";
    //     // reset Goomba
    //     foreach (Transform eachChild in enemies.transform)
    //     {
    //         eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
    //     }

    //     // jumpOverGoomba.score = 0;

    //     // reset animation
    //     marioAnimator.SetTrigger("gameRestart");
    //     alive = true;

    //     // reset camera position
    //     gameCamera.position = new Vector3(0, 0, -10);
    // }

    void PlayJumpSound()
    {
        // play jump sound
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    public void GameRestart()
    {
        // reset position
        marioBody.transform.position = new Vector3(-3f, -0.72f, 0.0f);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;

        // reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;

        // reset camera position
        gameCamera.position = new Vector3(0, 0, -10);
    }
}



