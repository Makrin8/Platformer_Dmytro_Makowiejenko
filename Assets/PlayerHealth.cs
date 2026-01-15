using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Gracz otrzymał obrażenia! Pozostało życia: {currentHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log("Gracz zginął!");
        }
    }
}