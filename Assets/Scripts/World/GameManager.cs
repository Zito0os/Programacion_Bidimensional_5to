using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;
    [SerializeField] private float tiempoEspera = 2f;
    public int gunammo = 32;
    public int health = 100;
    private float tiempoInicio;
    private bool jugadorMuerto = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        ammoText = GameObject.FindWithTag("AmmoText")?.GetComponent<TextMeshProUGUI>();
        healthText = GameObject.FindWithTag("HealthText")?.GetComponent<TextMeshProUGUI>();


        UpdateUI();


    }

    private void UpdateUI()
    {
        if (ammoText != null) ammoText.text = gunammo.ToString();
        if (healthText != null) healthText.text = health.ToString();
    }

    private void Update()
    {
        if (ammoText != null) ammoText.text = gunammo.ToString();
        if (healthText != null) healthText.text = health.ToString();


    }

    public void ReduceHealth(int amount)
    {
        if (jugadorMuerto || health <= 0) return;

        health -= amount;

        if (health <= 0)
        {
            health = 0;


            Invoke(nameof(CargarSiguienteEscena), 0.5f);
        }
    }

    private void CargarSiguienteEscena()
    {
        health = 100;
        gunammo = 32;
        int indiceActual = SceneManager.GetActiveScene().buildIndex;
        int indiceSiguiente = indiceActual + 1;

        if (indiceSiguiente >= SceneManager.sceneCountInBuildSettings)
        {
            indiceSiguiente = 0;
        }

        SceneManager.LoadScene(indiceSiguiente);
    }


    public void ResetearEstadisticas()
    {
        health = 100;
        gunammo = 32;
        jugadorMuerto = false;
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
        if (gunammo > 32)
        {
            gunammo = 32; // Evitar valores mayores a 20 (se lo puse asi pq una pistolita no tiene 60 balas lol)
        }
    }
}