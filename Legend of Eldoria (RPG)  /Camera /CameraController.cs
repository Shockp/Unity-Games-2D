using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void LateUpdate() {
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
}
