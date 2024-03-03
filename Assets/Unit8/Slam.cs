using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slam : MonoBehaviour
{
    [SerializeField] private float dropForce = 5f;
    [SerializeField] private float stopTime = 0.5f;
    [SerializeField] private float gravityScale = 1f;

    private PlatformPlayerController cc;

    private Rigidbody2D rb;

    private bool doSlam = false;

    private void Awake()
    {
        cc = GetComponent<PlatformPlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            if (!cc.isPlayerGrounded)
            {
                doSlam = true; 
            }
        }
    }

    private void FixedUpdate()
    {
        if(doSlam)
        {
            SlamAttack();
        }
        doSlam = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.contacts[0].normal.y >+ 0.5)
        {
            CompleteSlam();
        }
    }

    private void SlamAttack()
    {
        cc.enabled = false; 
        StopAndSpin();
        StartCoroutine("DropAndSmash");
    }

    private void StopAndSpin()
    {
        ClearForces();
        rb.gravityScale = 0;
    }

    private IEnumerator DropAndSmash()
    {
        yield return new WaitForSeconds(stopTime);
        rb.AddForce(Vector2.down * dropForce, ForceMode2D.Impulse);
    }

    private void CompleteSlam()
    {
        rb.gravityScale = gravityScale;
        cc.enabled = true;
    }

    private void ClearForces()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }
}
