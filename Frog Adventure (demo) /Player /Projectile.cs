using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed; // Speed of the projectile
    private float direction;
    private bool hit;
    private CircleCollider2D circleCollider; // Changed from BoxCollider2D to CircleCollider2D

    private void Awake() {
        // Get the CircleCollider2D component
        circleCollider = GetComponent<CircleCollider2D>();
        if (circleCollider == null) {
            Debug.LogError("CircleCollider2D component missing.");
        }
    }

    private void Update() {
        if (hit) return;

        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        hit = true;
        circleCollider.enabled = false;
        // Assuming the projectile should be deactivated upon collision
        Invoke("Deactivate", 0.01f);

        // Perfom damage to the enemies
        if(other.CompareTag("Trap")){
            other.GetComponent<Health>().TakeDamage(1);
        }
    }

    public void SetDirection(float _direction) {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        circleCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction) {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate() {
        gameObject.SetActive(false);
    }
}
