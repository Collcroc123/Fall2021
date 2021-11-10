using UnityEngine;

[CreateAssetMenu]
public class StatsData : ScriptableObject
{
    public int currentScore;
    public int highScore, enemiesKilled, bossesKilled, deaths, levelsCompleted, gamesCompleted, bulletsFired, cratesCollected;
    public float damageTaken, healthGained, damageDealt;

    void GameEnd()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            currentScore = 0;
        }
    }
}
