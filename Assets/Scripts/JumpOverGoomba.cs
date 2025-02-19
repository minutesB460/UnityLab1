using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JumpOverGoomba : MonoBehaviour
{

    public Transform enemyLocation;
    // public TextMeshProUGUI scoreText;

    // public TextMeshProUGUI scoreTextOver;
    private bool onGroundState;

    [System.NonSerialized]
    // public int score = 0; // we don't want this to show up in the inspector

    private bool countScoreState = false;
    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;
    // Start is called before the first frame update
    GameManager gameManager;
    void Start()
    {
         gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    void FixedUpdate()
    {
        // Debug.Log("On Ground: " + onGroundState);
        // mario jumps
        if (Input.GetKeyDown("space") && onGroundState)
        {
            onGroundState = false;
            countScoreState = true;

        }

        // // when jumping, and Goomba is near Mario and we haven't registered our score
        // if (!onGroundState && countScoreState)
        // {
        //     // Debug.Log(Mathf.Abs(transform.position.x - enemyLocation.position.x));

        //     if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
        //     {
        //         countScoreState = false;
        //         // score++;
        //         // scoreText.text = "Score: " + score.ToString();
        //         // scoreTextOver.text = "Score: " + score.ToString();
        //         gameManager.IncreaseScore(1); //
        //         // Debug.Log(score);
        //     }
        // }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState = true;
    }

    // void OnCollisionExit2D(Collision2D col)
    // {
    //     if (col.gameObject.CompareTag("Ground"))
    //     {
    //         onGroundState = false;
    //         Debug.Log("Not on Ground");
    //     }
    // }


    private bool onGroundCheck()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask))
        {
            // Debug.Log("on ground");
            return true;
        }
        else
        {
            // Debug.Log("not on ground");
            return false;
        }
    }


    // helper
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }

}


