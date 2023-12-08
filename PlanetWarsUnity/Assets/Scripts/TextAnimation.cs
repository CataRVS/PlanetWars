using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBlink : MonoBehaviour
{
    [SerializeField] float scaleFactor = 1.2f;
    [SerializeField] float blinkSpeed = 0.1f;
    private Vector3 originalScale;
    
    private void OnEnable()
    {
        originalScale = transform.localScale;
        StartCoroutine(BlinkText());
    }

    private IEnumerator BlinkText()
    {
        WaitForSeconds wait = new WaitForSeconds(blinkSpeed);

        while (true)
        {
            // Increase font size
            transform.localScale = originalScale * scaleFactor;
            yield return wait;

            // Decrease font size
            transform.localScale = originalScale;
            yield return wait;
        }
    }
}

