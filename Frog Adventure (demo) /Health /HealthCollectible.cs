using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    // Amount of health to increase when collected
    [SerializeField] private int healthValue;

    private void OnTriggerEnter2D(Collider2D collision) {
        // Check if the collider that triggered the event is tagged as "Player"
        if(collision.CompareTag("Player")){
            // Get the Health component attached to the player
            Health health = collision.GetComponent<Health>();
            if(health != null){
                // Increase the player's health
                health.IncreaseHealth(healthValue);
                // Destroy the health collectible object
                Destroy(gameObject);
            }
        }
    }
}
