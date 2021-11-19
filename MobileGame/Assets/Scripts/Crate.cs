using UnityEngine;

public class Crate : MonoBehaviour
{
    public GameObject crate;
    private SpriteRenderer sprite;
    public bool isHeart;
    public GunData[] gunsArray;
    public GunData gun;
    public Sprite heartSprite, gunSprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        //gun = ScriptableObject.CreateInstance<GunData>();
        if (Random.Range(0f, 1f) >= 0.5f)
        {
            isHeart = true;
            sprite.sprite = heartSprite;
        }
        else
        {
            gun = gunsArray[Random.Range(0, 5)];
            sprite.sprite = gunSprite;
            sprite.color = gun.gunColor;
        }
    }

    public void ReplaceGun(GunData oldGun)
    {
        gun = oldGun;
        sprite.color = gun.gunColor;
    }
}