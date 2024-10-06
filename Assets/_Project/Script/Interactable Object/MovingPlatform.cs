using System.Collections;
using UnityEngine;


public class MovingPlatform : MonoBehaviour
{
    public Vector3 targetPosition; // Vị trí mà game object sẽ di chuyển đến
    public float moveDuration = 1f; // Thời gian di chuyển đến vị trí đích
    public float waitDuration = 1f; // Thời gian dừng lại ở mỗi vị trí

    private Vector3 startPosition; // Vị trí ban đầu của game object

    void Start()
    {
        startPosition = transform.position; // Lưu vị trí ban đầu
        StartCoroutine(MoveRoutine()); // Bắt đầu coroutine
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            // Di chuyển đến vị trí đích
            yield return StartCoroutine(MoveToPosition(targetPosition));
            // Dừng lại ở vị trí đích
            yield return new WaitForSeconds(waitDuration);
            // Di chuyển về vị trí ban đầu
            yield return StartCoroutine(MoveToPosition(startPosition));
            // Dừng lại ở vị trí ban đầu
            yield return new WaitForSeconds(waitDuration);
        }
    }

    IEnumerator MoveToPosition(Vector3 target)
    {
        Vector3 initialPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, target, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target; // Đảm bảo vị trí cuối cùng là chính xác
    }
}
