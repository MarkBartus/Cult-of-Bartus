using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] public float health = 10;
    [SerializeField] GameObject hitVFX;
    Animator anim;
    public float currentHealth;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        //healthBar.SetMaxHealth(health);

        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        //healthBar.SetHealth(currentHealth);


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void use()
    {
        if (Input.GetKeyDown(KeyCode.Q) && currentHealth < 9 && Currency.Instance.hp > 0)
        {
            Currency.Instance.hp--;
            currentHealth += 2;
            healthBar.SetHealth(currentHealth);
        }
    }

    void Die()
    {
        //anim.SetTrigger("dead");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    public void HitVFX(Vector3 hitPosition)
    {
        GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
        Destroy(hit, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        use();
    }
}