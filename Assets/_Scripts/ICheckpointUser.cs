using UnityEngine;

public interface ICheckpointUser {

    public void SetCheckpoint(Checkpoint checkpoint);
    public Checkpoint GetCheckpoint();
    public void Respawn();
}
