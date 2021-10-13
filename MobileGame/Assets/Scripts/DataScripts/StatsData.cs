using UnityEngine;

[CreateAssetMenu]
public class StatsData : ScriptableObject
{
    public int highScore, enemiesKilled, bossesKilled, deaths, levelsCompleted, gamesCompleted, bulletsFired, gunsCollected;
    public float damageTaken, healthGained;
}
