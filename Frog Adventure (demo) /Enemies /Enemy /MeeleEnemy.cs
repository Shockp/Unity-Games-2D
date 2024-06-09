using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    // Serialized fields for easy adjustment in the Unity Inspector
    [SerializeField] private float attackCD; // Cooldown duration between attacks
    [SerializeField] private float range; // Range within which the enemy can detect the player
    [SerializeField] private int damage; // Damage dealt to the player

    [Header ("Collider Parameters")]
    [SerializeField] private float colliderDistance; // Distance from the enemy's collider to check for the player
    [SerializeField] private BoxCollider2D boxCollider; // Collider used to detect the player

    [Header ("Layer Parameters")]
    [SerializeField] private LayerMask playerLayer; // LayerMask to identify the player layer
    private float cooldownTimer = Mathf.Infinity; // Timer to track cooldown, initialized to infinity

    // Private variables
    private Animator anim; // Animator component to control animations
    private Health playerHealth; // Reference to the player's Health component

    private EnemyPatrol enemyPatrol;

    // Called when the script instance is being loaded
    private void Awake() {
        anim = GetComponent<Animator>(); // Get the Animator component attached to this GameObject
        enemyPatrol = GetComponentInParent<EnemyPatrol>(); // Get the EnemyPatrol component from the parent script
    }

    // Called once per frame
    private void Update() {
        cooldownTimer += Time.deltaTime; // Increment the cooldown timer by the time passed since the last frame

        // Check if the player is in sight and the cooldown has expired
        if(PlayerInSight()){
            if(cooldownTimer >= attackCD){
                cooldownTimer = 0; // Reset the cooldown timer
                anim.SetTrigger("meleeAttack"); // Trigger the melee attack animation
            }
        }

        if(enemyPatrol != null){
            enemyPatrol.enabled = !PlayerInSight(); // Stop patrolling when the player is not in sight
        }
    }

    // Method to check if the player is in sight using a BoxCast
    private bool PlayerInSight(){
        // Calculate the size and position for the BoxCast
        Vector3 changeSize = new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z);
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            changeSize, 
            0, 
            Vector2.left, 
            0, 
            playerLayer
        );

        // If the BoxCast hits a player, get the player's Health component
        if(hit.collider != null){
            playerHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null; // Return true if a player was hit
    }

    // Draw the detection box in the Unity Editor for visualization
    private void OnDrawGizmos() {
        // Calculate the size and position for the Gizmo
        Vector3 changeSize = new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z);
        Gizmos.color = Color.red; // Set Gizmo color to red
        Gizmos.DrawWireCube(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            changeSize
        ); // Draw a wireframe cube in the Editor to visualize the detection range
    }

    // Method to deal damage to the player
    private void DamagePlayer(){
        // Check if the player is in sight before dealing damage
        if(PlayerInSight()){
            playerHealth.TakeDamage(damage); // Deal damage to the player
        }
    }
}
