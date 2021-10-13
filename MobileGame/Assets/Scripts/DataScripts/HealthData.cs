using UnityEngine;

[CreateAssetMenu]
public class HealthData : ScriptableObject
{
    public float health, maxHealth = 10f;
    public StatsData stats;

    void ChangeHealth(float amount)
    {
        health += amount;
        if (health > maxHealth)
            health = maxHealth;
        
        else if (health <= 0)
        {
            //die?
            stats.deaths++;
        }

        if (amount < 0)
            stats.damageTaken += amount;
        else if (amount > 0)
            stats.healthGained += amount;
    }
}