using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_BOSS : MonoBehaviour
{
    public float velocidad = 3f;
    public float Vida = 100f;
    public float rangoDeteccion = 1f;
    public float rangoParada = .5f; // nueva distancia mínima: si el jugador está más cerca que esto, el enemigo dispara

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private arma_enemy arma;
    public PlayerSoundController soundController;
    public Transform jugador; // Visible en el inspector, pero se llenará solo

    // Umbral para dejar de moverse cuando ya está casi alineado horizontalmente
    private const float umbralParadaHorizontal = 0.05f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        arma = GetComponentInChildren<arma_enemy>();
        if (jugador == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
                jugador = playerObj.transform;
        }
    }

    void FixedUpdate()
    {
        animator.SetBool("islive", true);
        if (jugador == null) return;

        // Distancia horizontal unicamente ignorando eje Y
        float distanciaHorizontal = Mathf.Abs(jugador.position.x - transform.position.x);
        // la detección usa distancia total (X e Y)
        float distanciaTotal = Vector2.Distance(transform.position, jugador.position); // detección completa

        // Aquí lo limitamos al rango horizontal:
        if (distanciaTotal < rangoDeteccion) // usamos distancia total para la detección y se detiene si es <= rangoParada
        {
            //si esta dentro del rango va a disparars
            if (distanciaTotal < rangoParada)
            {
                animator.SetBool("esta_cerca", true);
                animator.SetBool("caminando", false);
                // Disparar con cadencia controlada
                if (arma != null)
                {
                    arma.ahora_Shoot();
                }


            }
            else
            {
                animator.SetBool("caminando", true);
                animator.SetBool("esta_cerca", false);
                // Determinar direccion horizontal
                float deltaX = jugador.position.x - transform.position.x;

                // Si ya está prácticamente alineado, no se mueve
                if (distanciaHorizontal <= umbralParadaHorizontal)
                {
                    animator.SetBool("caminando", false);
                    return;
                }

                float direccionX = Mathf.Sign(deltaX); // -1 izquierda, 1 derecha
                Vector2 movimiento = new Vector2(direccionX, 0f);
                // Mover enemigo

                rb.MovePosition(rb.position + movimiento * velocidad * Time.fixedDeltaTime);


                //// Voltear sprite segun dirección
                //if (sprite != null)
                //    sprite.flipX = direccionX < 0;
            }

        }
        else
        {

            //Esta en Idle
            animator.SetBool("esta_cerca", false);
            animator.SetBool("caminando", false);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int daño = 20;
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.ReduceHealth(daño);
        }
    }

    public void QuitarVida(int cantidad)
    {
        Vida -= cantidad;
        if (rb != null)
            rb.linearVelocity = Vector2.zero; // Resetea la velocidad
        if (Vida <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        // Desactivar movimiento y colisiones
        enabled = false; // Desactiva el script
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false; // Desactiva la física
        }

        // Activar animación de muerte
        animator.SetBool("islive", false);



        //ESCENA PANTALLA FINAL 

        soundController.playMorir();


        
        // Destruir después de que termine la animación (ajusta el tiempo según tu animación)
        Destroy(gameObject, .7f); // 1 segundo, ajusta según la duración de tu animación
        int indiceActual = SceneManager.GetActiveScene().buildIndex;
        int indiceAnterior = indiceActual + 2;


        SceneManager.LoadScene(indiceAnterior);
    }
}
