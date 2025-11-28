using UnityEngine;

public class BALA_BOSS : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si choca con un player
        if (collision.gameObject.CompareTag("Player"))
        {

            int daño = 15;
            GameManager.Instance.ReduceHealth(daño);
            Destroy(gameObject); // Destruye bala


        }

        if (collision.gameObject.CompareTag("Piso"))
        {
            // Si choca con el jugador, no hace nada
            Destroy(gameObject); // Destruye bala
            //return;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {

            // Si choca con el jugador, no hace nada
            //Destroy(gameObject); // Destruye bala
            return;
        }


    }
}
