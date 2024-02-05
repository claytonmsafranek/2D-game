using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    Player playerScript;
    public int healAmount;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // did we colide with a player, if so heal 
        if (collision.tag == "Player")
        {
            // call heal funciton from player script and heal however much a health drop is worth then destroy pickup
            playerScript.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
