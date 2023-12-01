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

    void OnEnable()
    {
        originalScale = transform.localScale;

        // Comienza la corutina para el efecto de agrandamiento y empequeñecimiento continuo.
        StartCoroutine(BlinkText());
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            // Agranda el texto.
            transform.localScale = originalScale * scaleFactor;
            yield return new WaitForSeconds(blinkSpeed);

            // Empequeñece el texto.
            transform.localScale = originalScale;
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
}

