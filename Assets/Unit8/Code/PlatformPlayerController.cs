using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlayerController : MonoBehaviour
{
    [SerializeField] protected float runforce = 25f;
    [SerializeField] protected float jumpforce = 100f;
    [SerializeField] protected DoorSwitch winSwitch;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    
    protected Rigidbody2D rb;
    protected PlatformerScore score;

    public bool isPlayerGrounded
    {
        get
        {
            return Mathf.Abs(rb.velocity.y) < Mathf.Epsilon;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        score = FindObjectOfType<PlatformerScore>();
    }

    // Update is called once per frame
    void Update()
    {

        if (winSwitch != null && winSwitch.IsActive)
        {
            Debug.Log("You Win!");
            FindObjectOfType<SceneLoader>()?.GoToNextScene();
            Destroy(gameObject);
            return;
        }

        if (isDashing)
        {
            return;
        }
        if (Input.GetKey(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        bool isPlayerGrounded = Mathf.Abs(rb.velocity.y) < Mathf.Epsilon;

        Vector2 moveDir = ReadMovement();
        Vector2 forceDir = new Vector2(moveDir.x * runforce, 0f);

        if (Mathf.Abs(moveDir.x) > Mathf.Epsilon)
        {
            transform.localScale = new Vector3(moveDir.x, 1f, 1f);
        }

        if (isPlayerGrounded && moveDir.y > 0f)
        {
            forceDir.y = jumpforce;
        }

        rb.AddRelativeForce(forceDir);

        if (isDashing)
        {
            return;
        }
    }

    protected Vector2 ReadMovement()
    {
        Vector2 moveDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection.x--;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection.x++;
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection.y++;
        }

        return moveDirection;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObj = other.gameObject;

       if (otherObj.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("You Died!");
            score.AddDeath();
            FindObjectOfType<PlatformLevelManager>()?.HandleDeath();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        GameObject otherObj = collisionInfo.gameObject;

        if (otherObj.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (otherObj.CompareTag("SideKillEnemy")) // Check if the enemy can be killed by hitting its sides
            {
                // Calculate the direction of collision
                Vector2 direction = otherObj.transform.position - transform.position;

                // Check if the collision occurs on the sides
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    score.AddPoints(otherObj.GetComponent<PlatformEnemy>().points);
                    Destroy(otherObj);
                    rb.AddRelativeForce(Vector2.up * jumpforce / 2f);
                }
                else
                {
                    Die();
                }
            }
            else // Handle enemies that can be killed by jumping on top
            {
                if (!otherObj.CompareTag("Projectile") &&
                    transform.position.y > otherObj.transform.position.y &&
                    rb.velocity.y < Mathf.Epsilon)
                {
                    score.AddPoints(otherObj.GetComponent<PlatformEnemy>().points);
                    Destroy(otherObj);
                    rb.AddRelativeForce(Vector2.up * jumpforce / 2f);
                }
                else
                {
                    Die();
                }
            }
        }
    }

    protected void Die()
    {
        score.AddDeath();
        FindObjectOfType<PlatformLevelManager>()?.HandleDeath();
        Destroy(gameObject);
    }
    
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}

