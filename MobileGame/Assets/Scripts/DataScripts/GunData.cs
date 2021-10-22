using UnityEngine;

[CreateAssetMenu]
public class GunData : ScriptableObject
{
    public GameObject bullet; //bullet to spawn
    public Color gunColor; //color of gun
    public float fireRate, bulletSpeed, bulletDamage, ammo, reloadSpeed; //gun & bullet attributes
    public Material bulletTexture; //bullet color
    public ArrayData gunshot; //gunshot sounds
    //public AudioClip reloadSound; //reload sound

    /* TO DO:
    x Bullet gets current gun from player
    AI damages itself
    DONT FORGET GAMEACTIONS!
    Spawn enemies (random enemy, position, and amount)
    Spawn furniture and items
    Detect items left in rooms (icons on minimap)
    */
}