using UnityEngine;

public class brickbox : MonoBehaviour
{
    public GameObject coinPrefab;  
    public AudioClip coinSound;    
    private bool istriggered = false;

    private SpringJoint2D springJoint;

    void Start()
    {
        springJoint = GetComponent<SpringJoint2D>();
        springJoint.frequency = 0; 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (!istriggered && collision.gameObject.CompareTag("Player"))
        {
            Vector2 hitDirection = collision.GetContact(0).normal;
            Debug.Log("Hit Direction Y: " + hitDirection.y);

            // plyaer hits the box from below 
            if (hitDirection.y > 0)
            {
                AudioSource.PlayClipAtPoint(coinSound, transform.position);

                springJoint.frequency = 10; 

                Invoke(nameof(ResetSpring), 0.2f); 

                // Spawn the coin
                GameObject coin = Instantiate(coinPrefab, transform.position + Vector3.up * 2f, Quaternion.identity);
                Animator coinAnimator = coin.GetComponent<Animator>();

                coinAnimator.SetTrigger("Hit");//trigger the animation

                Rigidbody2D coinRb = coin.GetComponent<Rigidbody2D>();
                if (coinRb != null)
                {
                    coinRb.linearVelocity = new Vector2(0, 5f);  
                }

                Destroy(coin, 2f);
            }
            else
            {
                // prevent bounce from other direction
                springJoint.enabled = false;
            }
        }
    }

    void ResetSpring()
    {
        istriggered= true;  
    }
}
