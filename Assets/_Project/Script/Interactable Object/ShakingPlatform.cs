using System.Collections;
using UnityEngine;

public class ShakingPlatform : MonoBehaviour
{
    public float shakeDuration = 2.0f;
    public float inactiveDuration = 3.0f;
    public Vector3 shakeAmount = new Vector3(0.1f, 0f, 0.1f);

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Rigidbody rb;

    private bool hasInteracted = false;  // Flag

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    public void Interact()
    {
        if (!hasInteracted)
        {
            hasInteracted = true;
            StartCoroutine(ShakingAndFall());
        }
    }

    private IEnumerator ShakingAndFall()
    {
        for (float t = 0; t < shakeDuration; t += Time.deltaTime)
        {
            transform.position = originalPosition + Vector3.Scale(Random.insideUnitSphere, shakeAmount);
            yield return null;
        }
        rb.useGravity = true;
        rb.isKinematic = false;

        Invoke(nameof(Reactivate), inactiveDuration);
    }

    private void Reactivate()
    {
        gameObject.SetActive(true);
        transform.SetPositionAndRotation(originalPosition, originalRotation);
        rb.useGravity = false;
        rb.isKinematic = true;
        hasInteracted = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
            VFXManager.Instance.SpawnVFX("FallingRock", transform.position);
        }

        if (collision.collider.CompareTag("Player"))
        {
            Interact();
        }
    }
}
