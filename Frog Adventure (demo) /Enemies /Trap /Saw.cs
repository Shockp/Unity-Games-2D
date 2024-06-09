using UnityEngine;

public class SideMove : MonoBehaviour
{
    // Distance the object moves left and right from its initial position
    [SerializeField] private float movementDistance;
    // Speed of movement
    [SerializeField] private float speed;
    // Damage inflicted on the player if they collide with this object
    [SerializeField] private float damage;

    // Flag indicating if the object is moving left
    private bool movingLeft;
    // Left edge limit of movement
    private float leftEdge;
    // Right edge limit of movement
    private float rightEdge;

    private void Awake() {
        // Calculate left and right edge positions based on initial position and movement distance
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update() {
        // Move left if currently moving left and not at left edge
        if (movingLeft){
            if (transform.position.x > leftEdge){
                // Move the object to the left
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            } else {
                // Change direction to move right
                movingLeft = false;
            }
        } else {
            // Move right if currently moving right and not at right edge
            if (transform.position.x < rightEdge){
                // Move the object to the right
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            } else {
                // Change direction to move left
                movingLeft = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        // Check if colliding object is tagged as "Player"
        if (collision.CompareTag("Player")){
            // Get the Health component from the player
            Health health = collision.GetComponent<Health>();
            if (health != null){
                // Apply damage to the player's health
                health.TakeDamage(damage);
            }
        }
    }
}
