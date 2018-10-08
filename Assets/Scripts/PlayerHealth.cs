using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] int maxHealth = 30;
    [SerializeField] Text healthText;

    int currentHealth;
    

    private void Start()
    {
        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString();
    }



    private void OnTriggerEnter(Collider other)
    {
        currentHealth -= 1;
        healthText.text = currentHealth.ToString();
    }

}
