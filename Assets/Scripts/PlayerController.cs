using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float dashSpeed = 10f;
    public float crouchSpeed = 2.5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded = true;
    private bool isDashing = false;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        bool isCrouching = Input.GetKey(KeyCode.C);
        bool isDashingInput = Input.GetKey(KeyCode.LeftShift);

        float moveSpeed = walkSpeed;
        if (isDashingInput && !isCrouching)
        {
            moveSpeed = dashSpeed;
            isDashing = true;
        }
        else if (isCrouching)
        {
            moveSpeed = crouchSpeed;
            isDashing = false;
        }
        else
        {
            isDashing = false;
        }

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        animator.SetBool("isWalking", moveInput != 0 && !isDashing && !isCrouching);
        animator.SetBool("isAttacking", Input.GetMouseButton(0));
        animator.SetBool("isCrouching", isCrouching);
        animator.SetBool("isDashing", isDashing);
        animator.SetBool("isJumping", !isGrounded);
        animator.SetBool("isDefending", Input.GetMouseButton(1));

        if (isCrouching)
        {
            GetComponent<BoxCollider2D>().size = new Vector2(1, 0.5f);
        }
        else
        {
            GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
        }

        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Danger"))
        {
            GameManager.instance.ShowGameOver();
        }
    }

    public void OnJumpStart()
    {
        Debug.Log("Jump started!");
    }
}
