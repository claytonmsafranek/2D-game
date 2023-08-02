using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public float timeBetweenAttacks;
    public int damage;

    // [HideInInspector] 
    // public Transform player;

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
            // enemy is dead, destory it
            Destroy(gameObject);
        }
    }


}
