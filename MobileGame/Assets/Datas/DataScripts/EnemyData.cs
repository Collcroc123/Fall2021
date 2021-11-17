using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public SpriteRenderer enemyTexture; //enemy's texture
    public GameObject bullet; //bullet to spawn
    public Color gunColor; //color of gun
    public float fireRate, bulletSpeed, bulletDamage, ammo, reloadSpeed; //gun & bullet attributes
    public Material bulletTexture; //bullet color
    public ArrayData gunshot; //gunshot sounds
    public int score; //how many points worth
}
