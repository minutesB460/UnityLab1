using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxPowerupController : MonoBehaviour, IPowerupController
{
    public Animator powerupAnimator;
    private Animator questionAnimator;
    public BasePowerup powerup; // reference to this question box's powerup

    void Start()
    {
        questionAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("hit");
        // if (other.gameObject.tag == "Player" && !powerup.hasSpawned)
        // {
            Debug.Log("hit2");

            // show disabled sprite
            // this.GetComponent<Animator>().SetTrigger("spawned");
            // spawn the powerup
            powerupAnimator.SetTrigger("spawned");
            //set question brick animator
            questionAnimator.SetTrigger("Hit");
            powerup.SpawnPowerup();
        // }
    }

    // used by animator
    public void Disable()
    {
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        transform.localPosition = new Vector3(0, 0, 0);
    }

}