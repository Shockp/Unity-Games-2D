using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravityOnWall;
    [SerializeField] private float wallJumpPower;
    [SerializeField] private Vector2 transformRight;
    [SerializeField] private Vector2 transformLeft;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider2D;
    private float wallJumpCD;
    private float horizontalInput;

    private void Awake() {
        // Create a variable for the component RigidBody2D
        body = GetComponent<Rigidbody2D>();
        if(body == null){
            Debug.LogError("Rigidbody2D component missing.");
            return;
        }

        // Create a variable for the component Animator
        anim = GetComponent<Animator>();
        if(anim == null){
            Debug.LogError("Animator component missing.");
            return;
        }

        // Create a variable for the component BoxCollider2D
        boxCollider2D = GetComponent<BoxCollider2D>();
        if(boxCollider2D == null){
            Debug.LogError("BoxCollider2D component missing.");
            return;
        }
    }

    private void Update() {
        // Get input for moving horizontally
        horizontalInput = Input.GetAxis("Horizontal");
        
        // Flip player when moving right/left
        if(horizontalInput > 0.01f){
            transform.localScale = transformRight;
        } else if(horizontalInput < -0.01f){
            transform.localScale = transformLeft;
        }        

        // Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
        
        // Wall jump logic
        if(wallJumpCD > 0.2f){
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if(onWall() && !isGrounded()){
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            } else {
                body.gravityScale = 1.15f;
            }
        } else {
            wallJumpCD += Time.deltaTime;
        }

        // Handle jumping with both spacebar and W key
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
            Jump();
        }
    }

    // Method to handle jumping
    private void Jump(){
        if(isGrounded()){
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");   
        } else if(onWall() && !isGrounded()){
            wallJumpCD = 0;
            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * gravityOnWall, wallJumpPower);
        }
    }

    // Method to check if the player is on the ground
    private bool isGrounded(){
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit2D.collider != null;
    }

    // Method to check if the player is on the wall
    private bool onWall(){
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit2D.collider != null;
    }

    // Method to check if the player can attack
    public bool canAttack(){
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
