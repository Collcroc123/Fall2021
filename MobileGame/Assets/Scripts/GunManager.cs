using System.Collections;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public GunData gun;
    public SpriteRenderer gunSprite;
    public bool isShooting, isEnemy;
    public BoolData canShoot;

    void Start()
    {
        SetGun(gun);
    }

    public void SetGun(GunData newGun) //If crate touched
    {
        gun = newGun;
        gunSprite.material.color = gun.gunColor;
    }

    public void Shoot()
    {
        if (canShoot.value)
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