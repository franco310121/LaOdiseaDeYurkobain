using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private enum State { idle, run, jump };
    private State state = State.idle;
    private Collider2D coll;

    [SerializeField] private LayerMask Ground;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if (hDirection > 0)
        {
            rb.linearVelocity = new Vector2(5, rb.linearVelocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        else if (hDirection < 0)
        {
            rb.linearVelocity = new Vector2(-5, rb.linearVelocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(Ground))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 4f);
            state = State.jump;
        }

        UpdateState();

        anim.SetInteger("State", (int)state);
    }

    private void UpdateState()
    {
        if (state == State.jump)
        {
            if (coll.IsTouchingLayers(Ground) && rb.linearVelocity.y <= 0.1f)
            {

                if (Mathf.Abs(rb.linearVelocity.x) > 0.1f)
                {
                    state = State.run;
                }
                else
                {
                    state = State.idle;
                }
            }
        }
        else if (Mathf.Abs(rb.linearVelocity.x) > 0.1f)
        {
            state = State.run;
        }
        else
        {
            state = State.idle;
        }
    }
}
