using UnityEngine;

[CreateAssetMenu]
public class ObjectData : ScriptableObject
{
    public GameObject bullet;
    public Color gunColor;
    public float fireRate, reloadSpeed, bulletSpeed, bulletDamage;
    
    //AI gets current gun damage
    //AI damages itself
    //DONT FORGET GAMEACTIONS!
}