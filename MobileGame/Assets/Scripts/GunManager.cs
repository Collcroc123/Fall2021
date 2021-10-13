using UnityEngine;

public class GunManager : MonoBehaviour
{
    public GunData gun;
    public SpriteRenderer gunSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        SetGun();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetGun() //If crate is shot
    {
        //Spawn current gun on the ground in crate
        //gun = GetComponent<GunData>(); //Get other gun
        gunSprite.material.color = gun.gunColor;
        //sets gun info
    }
}
