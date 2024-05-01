using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(20);
            Debug.Log("Player has taken 20 damage");
        }

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    public void Heal (int heal)
    {
        currentHealth += heal;
        healthBar.SetHealth(currentHealth);

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
