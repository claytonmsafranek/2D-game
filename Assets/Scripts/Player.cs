using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;

    public int health;

    private Rigidbody2D rb;
    private Vector2 moveAmount;
    private Animator animator;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // TODO: instantiate however many instances of heart sprites as number of lives defined in inspector
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;

        // checkl if player is moving or not
        if (moveInput != Vector2.zero)
        {
            // player is moving, set animation to running
            animator.SetBool("isRunning", true);
        }
        else
        {
            // player is not moving since moveinput == (0,0), set animation to idle
            animator.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    public void TakeDamage(int amount)
    {
        //decrement health and update UI
        health -= amount;
        UpdateHealthUI(health);

        Debug.Log("Player health: " + health);

        if (health <= 0)
        {
            // enemy is dead, destory it
            Destroy(gameObject);
        }
    }

    public void ChangeWeapon(Weapon weaponToEquip)
    {
        //destroy game obejct with weapon tag and instantiate the new one right where the previous weapon was
        //there might be a better way to get child componenets, idk
        Transform oldWeaponPosition = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).transform;
        //destroy old weapon
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        //instantiate new weapon at exact same everything as previous weapon
        Instantiate(weaponToEquip, oldWeaponPosition.position, oldWeaponPosition.rotation, oldWeaponPosition);

    }

    private void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void Heal(int healAmount)
    {
        // TODO: make this dynamic and not hardcode 5 in here
        // make sure our health does not exceed whatever we set it as 
        if ((health + healAmount) > 5)
        {
            health = 5;
        }
        else
        {
            health += healAmount;
        }

        // update the UI after healing
        UpdateHealthUI(health);
    }

}
