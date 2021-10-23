using UnityEngine;

[CreateAssetMenu]
public class GunData : ScriptableObject
{
    public GameObject bullet; //bullet to spawn
    public Color gunColor; //color of gun
    public float fireRate, bulletSpeed, ammo, reloadSpeed; //gun & bullet attributes
    public int bulletDamage; //bullet damage
    public Material bulletTexture; //bullet color
    public ArrayData gunshot; //gunshot sounds
    //public AudioClip reloadSound; //reload sound

    /* TO DO:
    DONT FORGET GAMEACTIONS!
    Spawn enemies (random enemy, position, and amount)
    Spawn items
    Detect items left in rooms (icons on minimap)
    */
}