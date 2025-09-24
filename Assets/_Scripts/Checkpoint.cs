using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour {

    [Header("Visual")]
    [SerializeField] private bool showLockedVisual;
    [SerializeField] Renderer debugVisual;
    [SerializeField] Material lockedMaterial;
    [SerializeField] Material unlockedMaterial;

    [Header("Finish")]
    [SerializeField] private bool isFinish;
    [SerializeField] RectTransform finishVisual;

    [SerializeField] Transform spawnPoint;

    private void Awake() {
        debugVisual.enabled = showLockedVisual;
        if (finishVisual != null) {
            finishVisual.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out ICheckpointUser checkpointUser)) {
            checkpointUser.SetCheckpoint(this);
        }
    }

    public Vector3 GetSpawnPointPosition() {
        return spawnPoint.position;
    }

    public Transform GetSpawnPointTransform() {
        return spawnPoint;
    }

    public void Unlock() {
        debugVisual.material = unlockedMaterial;
        debugVisual.enabled = true;
        if (isFinish && finishVisual != null) {
            finishVisual.gameObject.SetActive(true);
        }
    }

    public void Lock() {
        if (showLockedVisual) {
            debugVisual.material = lockedMaterial;
        } else {
            debugVisual.enabled = false;
        }
    }

    public void Enable() {
        gameObject.SetActive(true);
    }

    public void Disable() {
        gameObject.SetActive(false);
    }
}
