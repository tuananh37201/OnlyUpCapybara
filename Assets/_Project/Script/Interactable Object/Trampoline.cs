using DG.Tweening;
using UnityEngine;

public class Trampoline : MonoBehaviour, IInteractable
{
    public float bounceForce = 20f;
    public float scaleFactor = 1.5f; // Hệ số phóng to
    public float duration = 0.5f; // Thời gian để phóng to và thu nhỏ
    public float delay = 0.5f; // Thời gian trễ trước khi phóng to
    public float collisionDelay = 1f; // Thời gian trễ giữa các lần kiểm tra va chạm

    private Vector3 originalScale;
    private bool canCollide = true;

    void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canCollide && other.CompareTag("Player"))
        {
            canCollide = false; // Ngăn chặn va chạm tiếp theo
            Interact(other.gameObject);
            DOVirtual.DelayedCall(delay, () =>
            {
                transform.DOScale(originalScale * scaleFactor, duration / 2)
                    .OnComplete(() => transform.DOScale(originalScale, duration / 2));
            });

            // Đặt lại canCollide sau một khoảng thời gian
            DOVirtual.DelayedCall(collisionDelay, () => canCollide = true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canCollide && collision.collider.CompareTag("Player"))
        {
            ContactPoint contact = collision.contacts[0];
            if (contact.point.y > transform.position.y)
            {
                canCollide = false; // Ngăn chặn va chạm tiếp theo
                Interact(collision.gameObject);
                DOVirtual.DelayedCall(delay, () =>
                {
                    transform.DOScale(originalScale * scaleFactor, duration / 2)
                        .OnComplete(() => transform.DOScale(originalScale, duration / 2));
                });

                // Đặt lại canCollide sau một khoảng thời gian
                DOVirtual.DelayedCall(collisionDelay, () => canCollide = true);
            }
        }
    }

    public void Interact(GameObject player)
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }
    }
}
