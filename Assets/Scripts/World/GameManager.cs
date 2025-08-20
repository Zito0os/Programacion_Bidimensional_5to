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

}
