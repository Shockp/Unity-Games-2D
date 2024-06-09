using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Reference to the Health component of the player
    [SerializeField] private Health playerHealth;
    // Reference to the total health bar image (background)
    [SerializeField] private Image healthBarTotal;
    // Reference to the current health bar image (foreground)
    [SerializeField] private Image healthBarCurrent;

    private void Start(){
        // Set the initial fill amount of the total health bar
        healthBarTotal.fillAmount = playerHealth.currentHealth / 10f;
    }

    private void Update(){
        // Update the fill amount of the current health bar based on the player's current health
        healthBarCurrent.fillAmount = playerHealth.currentHealth / 10f;
    }
}
