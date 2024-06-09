using UnityEngine;

public class Door : MonoBehaviour
{
    // Transform of the previous room
    [SerializeField] private Transform previousRoom;
    // Transform of the next room
    [SerializeField] private Transform nextRoom;
    // Reference to the camera controller
    [SerializeField] private CameraController mainCamera;
    
    // Method called when another 2D collider enters the trigger attached to this GameObject
    private void OnTriggerEnter2D(Collider2D collision) {
        // Check if the incoming collider has the tag "Player"
        if (collision.CompareTag("Player")) {
            // If the player's X position is less than the door's X position
            if (collision.transform.position.x < transform.position.x) {
                // Move the camera to the next room
                mainCamera.MoveToNewRoom(nextRoom, 1.95f);
            } else {
                // Move the camera to the previous room
                mainCamera.MoveToNewRoom(previousRoom, 1.25f);
            }
        }
    }
}
