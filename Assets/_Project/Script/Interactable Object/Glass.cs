using System.Collections;
using UnityEngine;

public class Glass : MonoBehaviour, IInteractable
{
    public float fadeDuration = 1f;
    public float inactiveTime = 5f;

    private Color originalColor;
    private Renderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<Renderer>();
        originalColor = spriteRenderer.material.color; // Lưu lại màu ban đầu

    }

    public void Interact(GameObject player)
    {
        StartCoroutine(FadeAndDisappear());
    }

    private IEnumerator FadeAndDisappear()
    {
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            spriteRenderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        gameObject.SetActive(false);

        // VFX
        VFXManager.Instance.SpawnVFX("Glass", transform.position);

        Invoke("Reactivate", inactiveTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Interact(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Interact(collision.gameObject);
        }
    }

    private void Reactivate()
    {
        gameObject.SetActive(true); // Kích hoạt lại đối tượng
        spriteRenderer.material.color = originalColor; // Khôi phục màu ban đầu
    }
}
