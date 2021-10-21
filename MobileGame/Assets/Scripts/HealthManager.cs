using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public HealthData health;
    public ArrayData heartArray;
    public GameObject heartPrefab, halfHeartPrefab;
    private GameObject heartFrame, halfHeart, lastHeart;
    private int numFrames, numHalfHearts; //tracks how many hearts
    private bool invincible; //true if hit
    public Animator invincibleAnimation; //player flashes
    public IntData lastHeartSlot; //what array slot holds rightmost heart
    
    void Start()
    { //spawns hearts
        health.health = 10;
        health.maxHealth = heartArray.array.Length;
        lastHeartSlot.value = health.health;
        heartArray.Clear();
        numFrames = health.health / 2;
        if (health.health % 2 == 1)
            numFrames++;
        int z = 0;
        for (int i = 0; i < numFrames; i++)
        {
            heartFrame = Instantiate(heartPrefab, new Vector3((i + 1) * 120,-120,0), Quaternion.identity);
            heartFrame.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            if (i < numFrames)
            {
                halfHeart = Instantiate(halfHeartPrefab, new Vector3(50, -50, 0), Quaternion.identity);
                halfHeart.transform.SetParent(heartFrame.transform, false);
            }
            else if (numFrames * 2 == health.health && i == numFrames - 1)
            {
                halfHeart = Instantiate(halfHeartPrefab, new Vector3(50, -50, 0), Quaternion.identity);
                halfHeart.transform.SetParent(heartFrame.transform, false);
            }
            if (heartFrame != null)
            {
                heartArray.array[z] = heartFrame;
                heartFrame = null;
                z++;
            }
            if (halfHeart != null)
            {
                heartArray.array[z] = halfHeart;
                halfHeart = null;
                z++;
            }
            
        }
    }

    private void OnTriggerStay(Collider other)
    { //damages player when touching enemy or their bullets
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            if (!invincible)
            {
                StartCoroutine(Hurt(1));
            }
        }
    }

    IEnumerator Hurt(int damage)
    { //hurts player, plays animations
        invincible = true;
        health.Damage(damage);
        heartArray.GetLast();
        Destroy(heartArray.array[lastHeartSlot.value]);
        invincibleAnimation.SetBool("Invincible", true);
        yield return new WaitForSeconds(3f);
        invincibleAnimation.SetBool("Invincible", false);
        invincible = false;
    }
}