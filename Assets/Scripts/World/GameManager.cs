using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;

    public int gunammo = 60;
    public int health = 100;  

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
        
        health = 100;
        gunammo = 60;
        jugadorMuerto = false;

        Debug.Log("GameManager: Estadísticas reseteadas! Health=" + health + " Ammo=" + gunammo);
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
            jugadorMuerto = true;  

            
            Invoke(nameof(CargarSiguienteEscena), 0.5f);
        }
    }

    private void CargarSiguienteEscena()
    {
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
        gunammo = 60;
        jugadorMuerto = false;
    }
}