using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkForever : MonoBehaviour
{

    public float visibleDuration = 0.8f;  // Time the image stays visible
    public float hiddenDuration = 0.2f;   // Time the image stays hidden
    private Image imageComponent;


    private void OnEnable()
    {
        imageComponent = GetComponent<Image>();
        StartCoroutine(Blink());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            // Show the image
            imageComponent.enabled = true;

            // Wait for the visible duration
            yield return new WaitForSeconds(visibleDuration);

            // Hide the image
            imageComponent.enabled = false;

            // Wait for the hidden duration
            yield return new WaitForSeconds(hiddenDuration);
        }
    }
}

