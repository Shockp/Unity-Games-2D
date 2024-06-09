using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed; // Player movement speed
    private Rigidbody2D playerRB;
    private Animator playerAnimator;
    private SpriteRenderer sprite;

    private float moveX;
    private float moveY;
    private Vector2 movement;

    private void Awake() {
        // Initialize components
        playerRB = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
        playerAnimator = (Animator)GetComponent(typeof(Animator));
        sprite = (SpriteRenderer)GetComponent(typeof(SpriteRenderer));
    }

    private void Update() {
        // Capture player input
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        // Calculate movement
        movement = new Vector2(moveX, moveY);
        playerRB.velocity = movement * speed;

        // Update animation
        playerMoveAnimation(moveX, moveY, playerAnimator, sprite);
    }

    private void playerMoveAnimation(float horizontal, float vertical, Animator animator, SpriteRenderer spriteRenderer) {
        if(horizontal != 0 || vertical != 0) {
            animator.SetBool("Moving", true);
            spriteRenderer.flipX = horizontal < 0;
        } else {
            animator.SetBool("Moving", false);
        }
    }
}
