﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unitylities;


public class HealthBarCutFallDown : MonoBehaviour
{

    private RectTransform rectTransform;
    private float fallDownTimer;
    private float fadeTimer;
    private Image image;
    private Color color;

    private void Awake()
    {
        rectTransform = transform.GetComponent<RectTransform>();
        image = transform.GetComponent<Image>();
        color = image.color;
        fallDownTimer = .6f;
        fadeTimer = .5f;
    }

    private void Update()
    {
        fallDownTimer -= Time.deltaTime;
        if (fallDownTimer < 0)
        {
            float fallSpeed = 100f;
            rectTransform.anchoredPosition += Vector2.down * fallSpeed * Time.deltaTime;

            fadeTimer -= Time.deltaTime;
            if (fadeTimer < 0)
            {
                float alphaFadeSpeed = 5f;
                color.a -= alphaFadeSpeed * Time.deltaTime;
                image.color = color;

                if (color.a <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
