using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Speed at which the camera smooths its movement
    [SerializeField] private float speed;
    // The current X position the camera should move to
    private float currentPosX;
    // Velocity vector used by SmoothDamp to track the current velocity
    private Vector3 velocity = Vector3.zero;

    private void Update() {
        // Smoothly move the camera towards the new X position
        // Keeps the current Y and Z positions of the camera
        transform.position = Vector3.SmoothDamp(
            transform.position, 
            new Vector3(currentPosX, transform.position.y, transform.position.z), 
            ref velocity, 
            speed
        );
    }

    // Method to move the camera to a new room
    // _newRoom: Transform of the new room to move the camera to
    // fixCamera: Additional adjustment in the X axis to position the camera correctly
    public void MoveToNewRoom(Transform _newRoom, float fixCamera) {
        // Update the current X position to the new room's X position plus the adjustment
        currentPosX = _newRoom.position.x + fixCamera;
    }
}
