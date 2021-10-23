using UnityEngine;

public class GunManager : MonoBehaviour
{
    public GunData gun;
    public SpriteRenderer gunSprite;
    
    void Start()
    {
        SetGun();
    }

    void SetGun() //If crate is shot
    {
        //Spawn current gun on the ground in crate
        //gun = GetComponent<GunData>(); //Get other gun
        gunSprite.material.color = gun.gunColor;
        //sets gun info
    }
}