using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public float normalSpeed;
    public float health;

    private Rigidbody2D rb;


    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private Animator anim;
    public VectorValue pos;

    private void Start()
    {
        speed = 0f;
        transform.position = pos.initialValue;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        if (speed != 0f)
        {
            anim.SetBool("isRunning", true);
        }
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (isGrounded == true)
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }
    }
    public void OnJumpButtonDown()
    {
        if (isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("takeOf");
        }
    }

    public void OnLeftButtonDown()
    {
        if (speed >= 0f)
        {
            speed = -normalSpeed;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    public void OnRightButtonDown()
    {
        if (speed <= 0f)
        {
            speed = normalSpeed;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void OnButtonUp()
    {
        speed = 0f;
        anim.SetBool("isRunning", false);
    }
}