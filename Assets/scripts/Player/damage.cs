using UnityEngine;

public class damage : MonoBehaviour
{
    public PlayerMovement pm;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();      

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy" && pm.attacking == true && pm.acd == false)
        {
            gameObject.GetComponent<HealthSystem>().eTakeDamage(1);
        }
    }
}
