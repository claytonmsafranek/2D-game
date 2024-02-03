using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public float timeBetweenAttacks;
    public int damage;

    // TODO: come back and get this working to take player logic out of all individual enemy scripts
    // [HideInInspector] 
    // public Transform player;

    //num between 0-100. eg 20=20% change that drops something when enemy dies
    public int pickupChance;
    public GameObject[] pickups;

    // Start is called before the first frame update
    public virtual void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player").transform;

        // print(player);
        // Debug.Log("Player Position: " + player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Enemy health: " + health);

        if (health <= 0)
        {
            int randomNumber = Random.Range(0, 101);
            if (randomNumber < pickupChance)
            {
                //spawn a random pickup at enemy position right before destroy
                GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];
                Instantiate(randomPickup, transform.position, transform.rotation);
            }

            // enemy is dead, destory it
            Destroy(gameObject);
        }
    }


}
