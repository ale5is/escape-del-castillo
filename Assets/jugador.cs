using UnityEngine;

public class jugador : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 7f;

    [Header("Componentes")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator anim;

    private bool isGrounded;
    private float movimiento;

    void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (sprite == null)
            sprite = GetComponent<SpriteRenderer>();

        if (anim == null)
            anim = GetComponent<Animator>();
    }

    void Update()
    {
        movimiento = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            Saltar();

        ActualizarAnimaciones();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movimiento * speed, rb.velocity.y);
    }

    void Saltar()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void ActualizarAnimaciones()
    {
        if (anim == null)
            return;

        bool moviendo = movimiento != 0;

        anim.SetBool("saltar", !isGrounded);
        anim.SetBool("correr", moviendo && isGrounded);
        anim.SetBool("quieto", !moviendo && isGrounded);

        if (sprite != null)
        {
            if (movimiento > 0)
                sprite.flipX = false;
            else if (movimiento < 0)
                sprite.flipX = true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}