using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public GameObject healthBar;
    public Slider slider;
    private Coroutine displayHealthBar;

    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        gameObject.SetActive(true);
        slider.value = health;
        if (displayHealthBar == null)
            displayHealthBar = StartCoroutine(DisplayHealthBar());
        else
        {
            StopCoroutine(displayHealthBar);
            displayHealthBar = StartCoroutine(DisplayHealthBar());
        }
    }

    IEnumerator DisplayHealthBar()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
        displayHealthBar = null;
    }
}
