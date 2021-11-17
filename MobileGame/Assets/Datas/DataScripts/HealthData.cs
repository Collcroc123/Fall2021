using UnityEngine;

[CreateAssetMenu]
public class HealthData : ScriptableObject
{
    public int health, maxHealth = 10;
    public StatsData stats;
    

    public void Heal(int amount)//, int excess
    {
        health += amount;
        if (health > maxHealth)
        {
            //excess = health - maxHealth;
            health = maxHealth;
        }
        stats.healthGained += amount;
    }

    public void Damage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            //die?
            stats.deaths++;
        }
        stats.damageTaken += amount;
    }
}