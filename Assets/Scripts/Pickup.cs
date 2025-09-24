using UnityEngine;

public class Pickup : MonoBehaviour {

    [SerializeField] private float speedMultiplier;
    [SerializeField] private float moveForceMultiplier;

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out VehicleController player)) {
            player.ApplySpeedMultiplier(speedMultiplier);
            player.ApplyMoveForceMultiplier(moveForceMultiplier);
            Destroy(gameObject);
        }
    }
}
