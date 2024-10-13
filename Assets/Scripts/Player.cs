using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] bool isGrounded = false;
    [SerializeField] bool jump = false;

    Rigidbody2D rb;
    public float jumpForce = 6f;
    private float leftEdge;
    [SerializeField] private AudioManager audioManager;
    private Animator animator;
    private const string IsJumping = "IsJumping";
    public float raycastDistance = 1f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        UpdateLeftEdge();
    }

    private void Update()
    {
        HandleTouchInput();
        CheckBounds();
        UpdateLeftEdge();
    }
    private void FixedUpdate()
    {
        CheckCollision();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (isGrounded == true)
                {
                    Jump();   
                }
            }     
        }
    }

    private void Jump()
    {
        audioManager.PlaySFX(audioManager.buttonClip);
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;  
        
        JumpStatus();
    }

    private void JumpStatus()
    {
        if (isGrounded == false)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }

        animator.SetBool(IsJumping, jump);
    }

    private void UpdateLeftEdge()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector2.zero).x + 2f;
    }
    private void CheckBounds()
    {
        if (Mathf.Abs(this.transform.position.x - leftEdge) > 0.01f)
        {
            this.transform.position = new Vector2(leftEdge, this.transform.position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            JumpStatus();
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            audioManager.PlaySFX(audioManager.hitClip);
            GameManager.Instance.GameOver();
        }

    }
    private void CheckCollision()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.right, raycastDistance);

        if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
        {
            audioManager.PlaySFX(audioManager.hitClip);
            GameManager.Instance.GameOver();
        }
    }
}

