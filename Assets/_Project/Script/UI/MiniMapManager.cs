using UnityEngine;
using UnityEngine.UI;

public class MiniMapManager : MonoBehaviour
{
    private GameObject player;
    public Button[] checkpointButtons;
    public GetCheckpointInfor[] checkpoints;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");


    }

    private void Update()
    {
        for (int i = 0; i < checkpointButtons.Length; i++)
        {
            int index = checkpointButtons.Length - 1 - i; // Đảo ngược thứ tự

            if (checkpointButtons[i] != null)
            {
                checkpointButtons[i].onClick.AddListener(() => MovePlayerToCheckpoint(index));
            }
        }
    }

    void MovePlayerToCheckpoint(int index)
    {
        if (index < checkpoints.Length)
        {
            if (checkpoints[index].isCheckpointTriggered)
            {
                player.transform.position = checkpoints[index].checkpointPosition;
            }
        }

    }
}