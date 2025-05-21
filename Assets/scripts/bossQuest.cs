using Enemy;
using UnityEngine;

public class bossQuest : MonoBehaviour
{
    public GameObject rewardWindow;
    public bool bossKilled = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void bossDead()
    {
        if(bossKilled == true)
        {
            rewardWindow.SetActive(true);
        }
        else
        {
            return;
        }
    }
}
