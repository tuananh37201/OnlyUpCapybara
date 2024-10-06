using UCExtension;
using UnityEngine;

public class CheckPlayerInArea : MonoBehaviour
{
    public Transform checkpoint;
    public GameObject player;

    private void Start()
    {
        checkpoint = transform.FindObjectWithTag("Checkpoint").transform;
        if (checkpoint == null)
        {
            Debug.LogError("CheckPoint not found as a child of the GameObject.");
        }

        LoadPlayerPosition();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            SaveCheckpointPosition();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
        }
    }

    private void SaveCheckpointPosition()
    {
        PlayerPrefs.SetFloat("CheckpointX", checkpoint.position.x);
        PlayerPrefs.SetFloat("CheckpointY", checkpoint.position.y);
        PlayerPrefs.SetFloat("CheckpointZ", checkpoint.position.z);
        PlayerPrefs.Save();
    }

    private void LoadPlayerPosition()
    {
        if (PlayerPrefs.HasKey("CheckpointX") && PlayerPrefs.HasKey("CheckpointY") && PlayerPrefs.HasKey("CheckpointZ"))
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                if (player == null)
                {
                    Debug.LogError("Player GameObject not found.");
                    return;
                }
            }

            float x = PlayerPrefs.GetFloat("CheckpointX");
            float y = PlayerPrefs.GetFloat("CheckpointY");
            float z = PlayerPrefs.GetFloat("CheckpointZ");
            player.transform.position = new Vector3(x, y, z);
        }
    }
}