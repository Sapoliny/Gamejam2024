using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageIndicator : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshPro textMeshPro;
    private float fadeDuration = 0.9f; // Time to fade out
    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        // Start fading the text and destroy the object afterwards
        StartCoroutine(FadeOutAndDestroy());
    }

    public void Show(float damage,Transform damageTransform)
    {
        transform.position = new Vector2(damageTransform.position.x + 1 + Random.Range(-0.25f,0.25f), damageTransform.position.y + 1.5f + Random.Range(-0.25f, 0.25f));
        textMeshPro.text = Mathf.Round(damage).ToString();
        StartCoroutine(FadeOutAndDestroy());
    }

    IEnumerator FadeOutAndDestroy()
    {
        float startAlpha = textMeshPro.color.a;
        Color objectStartColor = objectRenderer ? objectRenderer.material.color : Color.white;
        float elapsedTime = 0f;

        // Gradually fade out the alpha
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 0, elapsedTime / fadeDuration);

            // Update text color with the new alpha value
            Color newTextColor = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, newAlpha);
            textMeshPro.color = newTextColor;

            // If there's a Renderer, fade the object's alpha too
            if (objectRenderer)
            {
                Color newObjectColor = new Color(objectStartColor.r, objectStartColor.g, objectStartColor.b, newAlpha);
                objectRenderer.material.color = newObjectColor;
            }

            yield return null;
        }

        // Ensure both the text and object are fully transparent after fading
        textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, 0);

        if (objectRenderer)
        {
            objectRenderer.material.color = new Color(objectStartColor.r, objectStartColor.g, objectStartColor.b, 0);
        }

        // Destroy the GameObject after fade is complete
        Destroy(gameObject);
    }
}
