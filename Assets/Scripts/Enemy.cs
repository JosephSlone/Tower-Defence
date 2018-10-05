using System;
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
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        currentHits = MaxHits;
        audioSource = GetComponent<AudioSource>();
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

        if (currentHits <= 0)
        {
            DestroyMySelf();
        }
    }

    private void ShowHitEffect()
    {
        hitExplosion.SetActive(true);
        //StartCoroutine(HideHitEffect());
    }

    IEnumerator HideHitEffect()
    {
        yield return new WaitForSeconds(1f);
        hitExplosion.SetActive(false);

    }

    void DestroyMySelf()
    {
        deathExplosion.SetActive(true);
        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }        
        StartCoroutine(WaitForDeath());
    }

    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }


}
