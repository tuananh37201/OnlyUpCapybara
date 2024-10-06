using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public CheckpointData data;

    public float rotationSpeed = 50f;

    // Tạo chuỗi key gồm <tên chuỗi> + <ID> để phân loại các checkpoint đã được trigger
    // VD: checkpoint có ID: Gas station và checkpoint có ID : Island đã trigger thì sẽ lưu vào PlayerPrefs dạng <key> <value>:
    // <trigger_check_point_Gas station> <1>, <trigger_check_point_Island> <1>
    string key => "trigger_check_point_" + data.ID;

    public bool IsCheckpointTriggered
    {
        get
        {
            // Tìm dữ liệu trong PlayerPrefs, nếu ko dữ liệu thì đặt value mặc định là false
            return GetBool(key, false);
        }
        set
        {
            // Tạo dữ liệu trong PlayerPrefs với key và value được truyền vào
            SetBool(key, value);
        }
    }

    public static void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    public static bool GetBool(string key, bool defaultValue)
    {
        return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
    }

    private void Awake()
    {
        if (IsCheckpointTriggered)
        {

        }
    }

    private void Update()
    {
        // Xoay game object theo trục Y
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!IsCheckpointTriggered)
            {
                IsCheckpointTriggered = true;

                AudiosManager.Instance.PlaySFX("Checkpoint");
                VFXManager.Instance.SpawnVFX("Checkpoint", transform.position);

            }
        }
    }
}
