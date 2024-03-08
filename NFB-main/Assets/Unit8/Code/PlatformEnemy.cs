using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEnemy : MonoBehaviour
{
    public int points;

    public GameObject PlatformPlayer;
    private Transform playerPos;
    private Vector2 currentPos;
    public float distance;
    public float speedEnemy;

    protected Rigidbody2D rb;

    private bool isFollowing = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerPos = PlatformPlayer.GetComponent<Transform>();
        currentPos = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speedEnemy * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, currentPos, speedEnemy * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isFollowing = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isFollowing = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        GameObject otherObj = collisionInfo.gameObject;

        if (otherObj.CompareTag("Player"))
        {
            // Reverse direction only if collided with the player
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
