using System.Collections;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public GunData gun;
    public SpriteRenderer gunSprite;
    public bool isShooting, isEnemy;
    
    void Start()
    {
        SetGun();
    }

    void SetGun() //If crate touched
    {
        //gun = GetComponent<GunData>().gun; //Get other gun
        gunSprite.material.color = gun.gunColor;
    }

    public void Shoot()
    {
        StartCoroutine(ShootCoro());
    }

    IEnumerator ShootCoro()
    {
        isShooting = true;
        Bullet bullet = Instantiate(gun.bullet, gameObject.transform.position, gameObject.transform.rotation).GetComponent<Bullet>();
        bullet.gun = gun;
        bullet.bulletSpawn = gameObject;
        if (isEnemy)
            bullet.tag = "EnemyBullet";
        yield return new WaitForSeconds(gun.fireRate);
        isShooting = false;
    }
}