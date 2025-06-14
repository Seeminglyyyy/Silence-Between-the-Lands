using UnityEngine;

public class Player_Movement_ : MonoBehaviour
{
    public float speed = 8;
    public int facingDirection = 1;

    public Rigidbody2D rb;
    public Animator anim;

    public Player_Combat player_Combat;

    private void Update()
    {
        if(Input.GetButtonDown("Slash"))
        {
            player_Combat.Attack();
        }

    }

    // FixedUpdate is called 50 times per second.
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(horizontal > 0 && transform.localScale.x < 0 ||
            horizontal < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        anim.SetFloat("horizontal", Mathf.Abs(horizontal));
        anim.SetFloat("vertical", Mathf.Abs(vertical));


        rb.linearVelocity = new Vector2(horizontal, vertical) * speed;


    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

}

