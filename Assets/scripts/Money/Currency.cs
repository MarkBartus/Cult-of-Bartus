using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    public static Currency Instance { get; private set; }

    public PlayerMovement PM;

    void Awake()
    {
        if (Instance == null) { Instance = this; } else if (Instance != this) { Destroy(this); }
        DontDestroyOnLoad(gameObject);
    }

    public int gold;


    [SerializeField] public TextMeshProUGUI currency;

    [SerializeField] public TextMeshProUGUI hpPotions;

    [SerializeField] public TextMeshProUGUI counter;

    public int hp;

    public int currentAmount;

    public int damages = 1;

    // Start is called before the first frame update
    void Start()
    {
        PM = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        currency.text = gold.ToString();
        //hpPotions.text = hp.ToString();
        counter.text = currentAmount.ToString();

        if (hp > 3)
        {
            hp = 3;
        }
        else if (hp < 0)
        {
            hp = 0;
        }

        if (gold < 0)
        {
            gold = 0;
        }


    }

    public void Buy()
    {
        if (gold >= 100 && hp < 3)
        {
            hp += 1;
            gold -= 100;
        }

    }
    public void BuyDamage()
    {
        if (gold >= 300 && damages >=4)
        {
            PM.damageAmount++;
            damages += 1;
            gold -= 300;
        }

    }

}