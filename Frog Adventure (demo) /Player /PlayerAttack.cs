using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCD;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] snowballs;
    private float cdTimer = Mathf.Infinity;
    private PlayerMoving playerMoving;

    private void Awake() {
        playerMoving = GetComponent<PlayerMoving>();
    }

    private void Update() {
        // Check for attack input and cooldown
        if(Input.GetMouseButton(0) && cdTimer > attackCD){
            Attack();
        }

        cdTimer += Time.deltaTime;
    }

    // Method to handle the attack action
    private void Attack(){
        cdTimer = 0;

        int snowballIndex = FindSnowball();
        snowballs[snowballIndex].transform.position = firePoint.position;
        snowballs[snowballIndex].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    // Method to find an inactive snowball
    private int FindSnowball(){
        for(int i = 0; i < snowballs.Length; i++){
            if(!snowballs[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }
}
