using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        int daño = 20;
        // Si choca con un enemigo
        if (collision.gameObject.CompareTag("Player"))
        {

            GameManager.Instance.ReduceHealth(daño);


        }

        
    }
}
