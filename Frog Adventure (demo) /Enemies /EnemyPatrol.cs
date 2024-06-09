using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge; // Left boundary of the patrol area
    [SerializeField] private Transform rightEdge; // Right boundary of the patrol area

    [Header("Enemy")]
    [SerializeField] private Transform enemy; // Reference to the enemy transform

    [Header("Movement Parameters")]
    [SerializeField] private float speed; // Patrol movement speed
    private Vector3 initScale; // Initial scale of the enemy
    private bool movingLeft; // Flag to determine if the enemy is moving left

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration; // Duration for which the enemy stays idle
    private float idleTimer; // Timer to track idle time

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim; // Reference to the enemy's Animator component

    // Called when the script instance is being loaded
    private void Awake() {
        initScale = enemy.localScale; // Store the initial scale of the enemy
    }

    // Called when the object becomes disabled or inactive
    private void OnDisable() {
        anim.SetBool("moving", false); // Ensure the moving animation is stopped
    }

    // Called once per frame
    private void Update() {
        // Check the direction of movement and move the enemy accordingly
        if (movingLeft) {
            if (enemy.position.x >= leftEdge.position.x) {
                MoveInDirection(-1); // Move left
            } else {
                DirectionChange(); // Change direction when the left edge is reached
            }
        } else {
            if (enemy.position.x <= rightEdge.position.x) {
                MoveInDirection(1); // Move right
            } else {
                DirectionChange(); // Change direction when the right edge is reached
            }
        }
    }

    // Method to handle direction change and idle behavior
    private void DirectionChange() {
        anim.SetBool("moving", false); // Stop moving animation
        idleTimer += Time.deltaTime; // Increment idle timer

        // Change direction after the idle duration
        if (idleTimer > idleDuration) {
            movingLeft = !movingLeft; // Toggle the direction
        }
    }

    // Method to move the enemy in a specific direction
    private void MoveInDirection(int direction) {
        idleTimer = 0; // Reset the idle timer
        anim.SetBool("moving", true); // Start moving animation

        // Flip the enemy's sprite based on the direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);

        // Move the enemy
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y, enemy.position.z);
    }
}
