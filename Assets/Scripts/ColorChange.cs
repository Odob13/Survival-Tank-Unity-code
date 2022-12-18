using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//change color of the sprite array randomly between all colors every 2 seconds, smooth transition, at the same time all sprites change color

public class ColorChange : MonoBehaviour
{

    public float timeToChange = 2f;
    private float countdown;
    public SpriteRenderer[] spriteRenderer;
    private Color color;

    [System.Obsolete]
    void Start()
    {
        countdown = timeToChange;
        color = spriteRenderer[0].color;
    }

    void Update()
    {
        if (countdown <= 0f)
        {
            color = new Color(Random.value, Random.value, Random.value);
            countdown = timeToChange;
        }

        countdown -= Time.deltaTime;
        for (int i = 0; i < spriteRenderer.Length; i++)
        {
            spriteRenderer[i].color = Color.Lerp(spriteRenderer[i].color, color, Time.deltaTime);
        }
    }
}
