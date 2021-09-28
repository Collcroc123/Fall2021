using UnityEngine;

[CreateAssetMenu]
public class ObjectData : ScriptableObject
{
    public GameObject bullet;
    public Color gunColor;
    public float fireRate, reloadSpeed, bulletSpeed, bulletDamage, ammo;
    
    /* TO DO:
    Bullet gets current gun from player
    Bullet applies damage to enemy it collides with
    AI gets current gun damage
    AI damages itself
    DONT FORGET GAMEACTIONS!
    Spawn enemies (random enemy, position, and amount)
    Spawn furniture and items
    Detect items left in rooms (icons on minimap)
    */
}