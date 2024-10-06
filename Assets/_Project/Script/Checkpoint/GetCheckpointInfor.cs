using UnityEngine;

public class GetCheckpointInfor : MonoBehaviour
{
    public Vector3 checkpointPosition;
    public bool isCheckpointTriggered;

    void Start()
    {
        checkpointPosition = transform.position;
    }
    private void Update()
    {
        isCheckpointTriggered = GetComponent<Checkpoint>().IsCheckpointTriggered;
    }
}
