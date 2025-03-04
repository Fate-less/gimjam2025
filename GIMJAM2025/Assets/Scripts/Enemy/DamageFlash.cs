using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Material material;
    public string flashProperty = "_FlashAmount";
    public float flashIntensity = 1f;
    public float flashDuration = 0.1f;
    public int flashCount = 2;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }

    public void Flash()
    {
        if (spriteRenderer != null && material != null)
        {
            StartCoroutine(FlashRoutine());
        }
    }

    private IEnumerator FlashRoutine()
    {
        for (int i = 0; i < flashCount; i++)
        {
            material.SetFloat(flashProperty, flashIntensity);
            yield return new WaitForSeconds(flashDuration);
            material.SetFloat(flashProperty, 0);
            yield return new WaitForSeconds(flashDuration);
        }
    }
}
