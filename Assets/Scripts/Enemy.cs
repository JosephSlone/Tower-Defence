﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    [SerializeField] float MaxHits = 20f;
    [SerializeField] GameObject hitExplosion;
    [SerializeField] GameObject deathExplosion;

    [SerializeField] Image healthBar;

    float currentHits;

	// Use this for initialization
	void Start () {
        currentHits = MaxHits;

    }

    private void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        ShowHitEffect();
        currentHits -= 1;
        healthBar.fillAmount = currentHits / MaxHits;
        print("I was hit!");

        if (currentHits <= 0)
        {
            DestroyMySelf();
        }
    }

    private void ShowHitEffect()
    {
        hitExplosion.SetActive(true);
        StartCoroutine(HideHitEffect());
    }

    IEnumerator HideHitEffect()
    {
        yield return new WaitForSeconds(0.25f);
        hitExplosion.SetActive(false);

    }

    void DestroyMySelf()
    {
        deathExplosion.SetActive(true);
        StartCoroutine(WaitForDeath());

    }

    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }


}
