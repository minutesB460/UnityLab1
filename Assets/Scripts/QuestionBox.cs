using System.Collections;
using UnityEngine;

public class QuestionBox : MonoBehaviour
{
    public GameObject coinPrefab;
    public AudioClip coinSound;
    public AudioSource audioSource;   
    private Animator questionAnimator;
    private bool istriggered = false;

    private SpringJoint2D springJoint;

    GameManager gameManager;

    void Start()
    {
        questionAnimator = GetComponent<Animator>();
        springJoint = GetComponent<SpringJoint2D>();
        springJoint.frequency = 0;
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!istriggered && collision.gameObject.CompareTag("Player"))
        {
            Vector2 hitDirection = collision.GetContact(0).normal;

            if (hitDirection.y > 0)
            {
                questionAnimator.SetTrigger("Hit");
                // AudioSource.PlayClipAtPoint(coinSound, transform.position);
                audioSource.PlayOneShot(coinSound);

                //Increase the score 
                gameManager.IncreaseScore(1); 


                springJoint.frequency = 10;

                Invoke(nameof(ResetSpring), 0.2f);

                GameObject coin = Instantiate(coinPrefab, transform.position + Vector3.up * 2f, Quaternion.identity);
                Animator coinAnimator = coin.GetComponent<Animator>();
                coinAnimator.SetTrigger("Hit");

                Rigidbody2D coinRb = coin.GetComponent<Rigidbody2D>();
                if (coinRb != null)
                {
                    coinRb.linearVelocity = new Vector2(0, 5f);  
                }

                Destroy(coin, 2f);
   
            }
        }
    }

    void ResetSpring()
    {
        springJoint.enabled = false;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        istriggered = true;
    }

   public void RestoreBox()
{
    istriggered = false;
    springJoint.enabled = true;
    questionAnimator.SetTrigger("Reset"); 
}

}
