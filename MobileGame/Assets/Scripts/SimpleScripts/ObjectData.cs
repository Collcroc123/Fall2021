using UnityEngine;

[CreateAssetMenu]
public class ObjectData : ScriptableObject
{
    public GameObject bullet;
    public float fireRate, reloadSpeed, bulletSpeed;
}