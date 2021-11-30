using System.Collections;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public GunData gun;
    public SpriteRenderer gunSprite;
    public bool isShooting, isEnemy;
    public BoolData canShoot;
    public ArrayData gunArray;

    void Start()
    {
        SetGun(gun);
    }

    public void SetGun(GunData newGun) //If crate touched
    {
        gun = newGun;
        if (isEnemy)
        {
            gun = gunArray.guns[Random.Range(0, 5)];
        }
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
        if (isEnemy)
            yield return new WaitForSeconds(1f);
        Bullet bullet = Instantiate(gun.bullet, gameObject.transform.position, gameObject.transform.rotation).GetComponent<Bullet>();
        bullet.gun = gun;
        bullet.bulletSpawn = gameObject;
        if (isEnemy)
            bullet.tag = "EnemyBullet";
        yield return new WaitForSeconds(gun.fireRate);
        isShooting = false;
    }
}