using Enemy;
using UnityEngine;

public class bossQuest : MonoBehaviour
{
    public GameObject rewardWindow;
    public GameObject questWindow;
    public bool bossKilled = false;
    public GameObject boss;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bossKilled == true)
        {
            Destroy(boss);
        }
    }
    public void bossDead()
    {
        if(bossKilled == true)
        {
            rewardWindow.SetActive(true);
            questWindow.SetActive(false);
        }
    }
}
