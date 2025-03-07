using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;
    private Rigidbody2D enemyBody;
    public Vector3 startPosition = new Vector3(0, 0, 0f);

    public Animator goombaAnimator;
    public AudioSource goombaAudio;

    private Collider2D goombaCollider;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        goombaAnimator = GetComponent<Animator>();
        goombaAudio = GetComponent<AudioSource>();
        goombaCollider = GetComponent<Collider2D>();
        // get the starting position
        originalX = transform.position.x;
        ComputeVelocity();
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }
    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);


    }

    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {// move goomba
            Movegoomba();
        }
        else
        {
            // change direction
            moveRight *= -1;
            ComputeVelocity();
            Movegoomba();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
    }


    public void GameRestart()
    {
        transform.localPosition = startPosition;
        originalX = transform.position.x;
        moveRight = -1;
        ComputeVelocity();
        gameObject.SetActive(true); 
        goombaCollider.enabled = true;
    }

    public void Stomped()
    {
        goombaAnimator.Play("Stomp");
        goombaAudio.PlayOneShot(goombaAudio.clip);
        goombaCollider.enabled = false;
        // Destroy(gameObject, 0.5f);
        Invoke("HideGoomba", 0.5f);
    }

    void HideGoomba()
    {
        gameObject.SetActive(false); 
    }


}

