using UnityEngine;

public class Bala : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si choca con un enemigo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // Destruye enemigo
        }

        //if (collision.gameObject.CompareTag("Piso"))
        //{
        //    // Si choca con el jugador, no hace nada
        //    Destroy(gameObject); // Destruye bala
        //    //return;
        //}
        if (collision.gameObject.CompareTag("Player"))
        {
            
            // Si choca con el jugador, no hace nada
            //Destroy(gameObject); // Destruye bala
            return;
        }



        //Destroy(gameObject); // Destruye bala
    }
}
