using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    // Starting health value of the object
    [SerializeField] private float startingHealth;
    // Current health value of the object
    public float currentHealth { get; private set; }
    // Reference to the Animator component
    private Animator anim;
    // Flag indicating if the object is dead
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    public event Action OnDeath;

    private void Awake()
    {
        // Initialize current health to starting health
        currentHealth = startingHealth;
        // Get the Animator component attached to the same GameObject
        anim = GetComponent<Animator>();
        // Get the SpriteRenderer component attached to the same GameObject
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Method to apply damage to the object
    public void TakeDamage(float _damage)
    {
        // Decrease current health by the damage value, clamped between 0 and starting health
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        // If the object still has health remaining
        if (currentHealth > 0)
        {
            // Trigger the "hit" animation
            anim.SetTrigger("hit");
            // Start the invulnerability coroutine
            StartCoroutine(Invulnerability());
        }
        else
        { // If the object has no health remaining
            if (!dead)
            {
                // Trigger the "die" animation
                anim.SetTrigger("die");
                // Disable the PlayerMoving script if attached
                // Assumes that the PlayerMoving script is attached to the same GameObject
                if(GetComponent<PlayerMoving>() != null){
                    GetComponent<PlayerMoving>().enabled = false;
                }
                // Set the dead flag to true

                if(GetComponentInParent<EnemyPatrol>() != null){
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                }

                if(GetComponent<MeleeEnemy>() != null){
                    GetComponent<MeleeEnemy>().enabled = false;
                }
            
                dead = true;
            }
        }
    }

    // Method to increase the object's health
    public void IncreaseHealth(int healthValue)
    {
        // Increase current health by the health value, clamped between 0 and starting health
        currentHealth = Mathf.Clamp(currentHealth + healthValue, 0, startingHealth);
    }

    private IEnumerator Invulnerability()
    {
        // Ignore collisions between the Player layer (layer 8) and Enemy layer (layer 9) temporarily
        Physics2D.IgnoreLayerCollision(8, 9, true);

        // Flash the sprite to indicate invulnerability
        for (int i = 0; i < numberOfFlashes; i++)
        {
            // Set the sprite color to red with half alpha
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            // Set the sprite color back to white
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        // Restore collision between Player layer (layer 8) and Enemy layer (layer 9)
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
}
