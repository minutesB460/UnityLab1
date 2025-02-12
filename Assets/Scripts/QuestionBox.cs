using System.Collections;
using UnityEngine;

public class QuestionBox : MonoBehaviour
{

    public AudioClip coinSound;
    private Animator questionAnimator;
    private bool isUsed = false;

    private SpringJoint2D springJoint;

    void Start()
    {
        questionAnimator = GetComponent<Animator>();
        springJoint = GetComponent<SpringJoint2D>();
        springJoint.frequency = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isUsed && collision.gameObject.CompareTag("Player"))
        {
            Vector2 hitDirection = collision.GetContact(0).normal;
            Debug.Log("Hit Direction Y: " + hitDirection.y);

            if (hitDirection.y > 0)
            {
                questionAnimator.SetTrigger("Hit");
                AudioSource.PlayClipAtPoint(coinSound, transform.position);

                springJoint.frequency = 500;

                Invoke(nameof(ResetSpring), 0.2f);
            }
        }
    }

    void ResetSpring()
    {
        springJoint.enabled = false;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        isUsed = true;
    }

}
