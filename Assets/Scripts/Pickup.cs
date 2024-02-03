using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Weapon weaponToEquip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if this pickup object collides with player
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().ChangeWeapon(weaponToEquip);
            Debug.Log("destroying: " + gameObject);
            Destroy(gameObject); //this destroyes the pickup
        }
    }
}
