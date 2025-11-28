using UnityEngine;


public class Bala : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si choca con un enemigo
        if (collision.gameObject.CompareTag("Enemy"))
        {


            // Referencia al script del enemigo
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.QuitarVida(20); // Llama al método para quitarle vida
            }
            Destroy(gameObject); // Destruye bala



        }

        if (collision.gameObject.CompareTag("Gordo"))
        {


            // Referencia al script del enemigo
            EnemigoGordo gordo = collision.gameObject.GetComponent<EnemigoGordo>();
            if (gordo != null)
            {
                gordo.QuitarVida(20); // Llama al método para quitarle vida
            }
            Destroy(gameObject); // Destruye bala



        }
        if (collision.gameObject.CompareTag("Picos"))
        {


            // Referencia al script del enemigo
            Enemy_picos picos = collision.gameObject.GetComponent<Enemy_picos>();
            if (picos != null)
            {
                picos.QuitarVida(20); // Llama al método para quitarle vida
            }
            Destroy(gameObject); // Destruye bala



        }
        if (collision.gameObject.CompareTag("BOSS"))
        {


            // Referencia al script del enemigo
            Enemy_BOSS enemy = collision.gameObject.GetComponent<Enemy_BOSS>();
            if (enemy != null)
            {
                enemy.QuitarVida(20); // Llama al método para quitarle vida
            }
            Destroy(gameObject); // Destruye bala



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