using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] bool isGrounded = false;
    [SerializeField] bool jump = false;

    Rigidbody2D rb;
    public float jumpForce = 6f;
    private AudioManager audioManager;
    private Animator animator;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {     
            if (isGrounded == true)
            {  
                audioManager.PlaySFX(audioManager.buttonClip);
                rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;   
            }     
        }
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

        animator.SetBool("IsJumping", jump);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            audioManager.PlaySFX(audioManager.hitClip);
            GameManager.Instance.GameOver();
        }
    }
}

