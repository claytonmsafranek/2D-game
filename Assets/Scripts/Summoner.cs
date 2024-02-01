using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;

    [SerializeField] float attackSpeed;
    [SerializeField] float stopDistance;

    // this should be something small
    [SerializeField] float distanceToTargetPosition;

    [SerializeField] float timeBetweenSummons;

    [SerializeField] Enemy enemyToSummon;

    private Vector2 targetPosition;
    private Animator anim;
    private float summonTime;

    private Transform player;

    private float attackTime;

    // Start is called before the first frame update
    public override void Start()
    {
        // calls start method in base class (to find player)
        // base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);

        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, targetPosition) > distanceToTargetPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                anim.SetBool("isRunning", true);
            }
            else
            {
                // reached the random target position
                anim.SetBool("isRunning", false);

                if (Time.time >= summonTime)
                {
                    summonTime = Time.time + timeBetweenSummons;
                    anim.SetTrigger("summon");
                }
            }

            // run attack coroutine
            if (Vector2.Distance(transform.position, player.position) < stopDistance)
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
            Debug.Log("Summoner.cs: Player is dead/null!");
            GameObject[] enemiesStillStanding = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemiesStillStanding)
            {
                Destroy(enemy);
            }
            Debug.Log("Summoner.cs: GAME OVER!");
        }



    }

    public void Summon()
    {
        if (player != null)
        {
            Instantiate(enemyToSummon, transform.position, transform.rotation);
        }
    }

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
