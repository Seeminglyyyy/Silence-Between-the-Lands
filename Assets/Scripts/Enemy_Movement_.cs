using UnityEngine;

public class Enemy_Movement_ : MonoBehaviour
{
    [Header("Chase/Attack Settings")]
    public float speed;
    public float attackRange = 1.5f;
    public int damage = 1;   // ← how much HP to knock off
    public float attackCooldown = 1.0f; // ← seconds between hits

    private bool isChasing;
    private float lastAttackTime;
    private int facingDirection = -1;

    private Rigidbody2D rb;
    private Transform player;
    private Player_Health playerHealth;  // ← reference to their health script
    public Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Initialize so we can attack immediately on first encounter
        lastAttackTime = -attackCooldown;
    }

    void Update()
    {
        if (isChasing && player != null)
        {
            float dist = Vector2.Distance(transform.position, player.position);

            // In attack range?
            if (dist <= attackRange)
            {
                // Have we waited long enough since our last hit?
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    // Stop moving
                    rb.velocity = Vector2.zero;

                    // Fire the animation
                    anim.SetTrigger("Enemy_Attacking");

                    // Actually deal the damage
                    if (playerHealth != null)
                        playerHealth.ChangeHealth(-1);

                    // Remember when we hit so we cooldown
                    lastAttackTime = Time.time;
                }

                return; // skip the chase block this frame
            }

            // …otherwise chase as before…
            if ((player.position.x > transform.position.x && facingDirection == -1) ||
                (player.position.x < transform.position.x && facingDirection == 1))
            {
                Flip();
            }

            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * speed;

            // drive walk anim
            anim.SetFloat("horizontal", Mathf.Abs(rb.velocity.x));
            anim.SetFloat("vertical", Mathf.Abs(rb.velocity.y));
        }
        else
        {
            // idle
            anim.SetFloat("horizontal", 0f);
            anim.SetFloat("vertical", 0f);
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(
            transform.localScale.x * -1,
            transform.localScale.y,
            transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
            playerHealth = player.GetComponent<Player_Health>();  // cache their health
            isChasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
            isChasing = false;
        }
    }
}
