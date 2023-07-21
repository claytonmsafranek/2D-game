using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabEnemy : Enemy
{
    [SerializeField] float stopDistance;
    [SerializeField] float attackSpeed;

    private float attackTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                // continue to move towards player
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                if (Time.time >= attackTime)
                {
                    // start the attack sequence where it jumps at player
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }
        }
        else
        {
            Debug.LogError("Player is null");
        }
    }

    // Attack Coroutine
    IEnumerator Attack()
    {
        // deal damage to player
        player.GetComponent<Player>().TakeDamage(damage);

        // position of enemy before leaping towards player
        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);

            yield return null;
        }
    }


}
