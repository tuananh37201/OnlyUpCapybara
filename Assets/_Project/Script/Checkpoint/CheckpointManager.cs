using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class CheckpointManager : MonoBehaviour
{
    private GameObject[] checkpointObjects;

    public Stack<string> triggeredCheckpointsData = new Stack<string>();
    private List<CheckpointData> allCheckpointData = new List<CheckpointData>();
    public List<string> sortedCheckpointId = new List<string>();

    void Awake()
    {
        checkpointObjects = GameObject.FindGameObjectsWithTag("Checkpoint");
        LoadCheckpointData();
        LoadAllCheckpointData();
        SortCheckpointID();

    }

    private void LoadCheckpointData()
    {
        foreach (var checkpoint in checkpointObjects)
        {
            var checkpointID = checkpoint.GetComponent<Checkpoint>().data.ID;
            var checkpointKey = "trigger_check_point_" + checkpointID;
            if (PlayerPrefs.HasKey(checkpointKey) && !triggeredCheckpointsData.Contains(checkpointKey))
            {
                triggeredCheckpointsData.Push(checkpointKey);
            }
        }
    }

    private void LoadAllCheckpointData()
    {
        CheckpointData[] checkpointDataArray = Resources.LoadAll<CheckpointData>("CheckpointData");
        foreach (var checkpointData in checkpointDataArray)
        {
            allCheckpointData.Add(checkpointData);
        }
    }

    private void SortCheckpointID()
    {
        // Tạo danh sách tạm thời để lưu trữ các checkpoint đã sắp xếp
        var sortedCheckpoints = allCheckpointData.OrderBy(checkpointData => checkpointData.SortPriority).ToList();

        // Xóa danh sách ID đã sắp xếp trước đó
        sortedCheckpointId.Clear();

        // Thêm các ID đã sắp xếp vào danh sách ID đã sắp xếp
        foreach (var checkpointData in sortedCheckpoints)
        {
            sortedCheckpointId.Add(checkpointData.ID);
        }
    }
}