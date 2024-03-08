using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    public bool isSliding = false;
   
    public PlatformPlayerController PL;

    public Rigidbody2D rigidBody;

    public Animator anim;

    public CapsuleCollider2D regularColl;
    public CapsuleCollider2D slideColl;

    public float slideSpeed = 5f; 

    private void update()
    {
        if (Input.GetKey(KeyCode.Space))
            preformSlide();
    }
    private void preformSlide()
    {
        isSliding = true;

        anim.SetBool("IsSlide", true);

        regularColl.enabled = false;
        slideColl.enabled = true;

        StartCoroutine("stopSlide");
    }

    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(0.8f);
        anim.Play("Idle");
        anim.SetBool("IsSlide", false);
        regularColl.enabled = true;
        slideColl.enabled = false;
        isSliding = false; 
    }
}