using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //entender mejo el game manager
    //SINGELTON


    public static GameManager Instance { get; private set; }


    //public Text ammoText; // UI Text to display ammo count
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;

    public int gunammo = 12;
    public int health = 12;

    private void Awake()
    {
        Instance = this;
    }


    private void Update()
    {
        ammoText.text = gunammo.ToString();
        healthText.text = health.ToString();
    }
    public void ReduceHealth(int amount)
    {
        health -= amount;
        if (health < 0)
        {
            health = 0; // Evitar valores negativos
        }
    }
    public void AddHealth(int amount)
    {
        health += amount;
        if (health > 100)
        {
            health = 100; // Evitar valores mayores a 100
        }
    }

    public void AddAmmo(int amount)
    {
        gunammo += amount;
        if (gunammo > 20)
        {
            gunammo = 20; // Evitar valores mayores a 20 (se lo puse asi pq una pistolita no tiene 60 balas lol)
        }
    }

}
