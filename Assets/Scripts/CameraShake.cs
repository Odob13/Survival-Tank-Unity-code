using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public void ShakeCamera(float duration, float magnitude)
    {
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y + y, transform.localPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = new Vector3(0,0, -10);
    }

    public void FadeIn(GameObject sprite, float duration)
    {
        StartCoroutine(FadeInCoroutine(sprite, duration));
    }

    IEnumerator FadeInCoroutine(GameObject sprite, float duration)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float alpha = (elapsed / duration);

            sprite.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, alpha);

            elapsed += Time.deltaTime;

            yield return null;
        }

        FadeOut(sprite, duration);
    }

    public void FadeOut(GameObject sprite, float duration)
    {
        StartCoroutine(FadeOutCoroutine(sprite, duration));
    }

    IEnumerator FadeOutCoroutine(GameObject sprite, float duration)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float alpha = 1 - (elapsed / duration);

            sprite.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, alpha);

            elapsed += Time.deltaTime;

            yield return null;
        }
    }

}
 
