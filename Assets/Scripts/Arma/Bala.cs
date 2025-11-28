using UnityEngine;

public class Bala : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si choca con un enemigo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);

            // Referencia al script del enemigo
            //Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            //if (enemy != null)
            //{
            //    enemy.QuitarVida(20); // Llama al método para quitarle vida
            //}
            // Destruye bala



        }

        

        if (collision.gameObject.CompareTag("Piso"))
        {
            // Si choca con el jugador, no hace nada
            Destroy(gameObject); // Destruye bala
            //return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            
            // Si choca con el jugador, no hace nada
            //Destroy(gameObject); // Destruye bala
            return;
        }



        //Destroy(gameObject); // Destruye bala
    }
}
