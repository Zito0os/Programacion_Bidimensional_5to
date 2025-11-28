using UnityEngine;
using System.Collections;

public class EnemigoGordo : MonoBehaviour
{

    public float velocidad = 3f;
    public float Vida = 100f;
    public float rangoDeteccion = 1f;
    public float rangoParada = .5f; // nueva distancia mínima: si el jugador está más cerca que esto, el enemigo dispara
    public PlayerSoundController playerSoundController;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private arma_enemy arma;

    public float nextShotTime = 0f;

    public float intervalo_ataque = .4f;
    public int dañoGolpe = 10;

    public Transform jugador; // Visible en el inspector, pero se llenará solo

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
            jugador = playerObj.transform;

    }

    void FixedUpdate()
    {
        animator.SetBool("islive", true);
        if (jugador == null) return;

        // Distancia horizontal unicamente ignorando eje Y
        float distanciaHorizontal = Mathf.Abs(jugador.position.x - transform.position.x);
        // la detección usa distancia total (X e Y)
        float distanciaTotal = Vector2.Distance(transform.position, jugador.position); // detección completa

        int dis_total = (int)distanciaTotal;

        // Aquí lo limitamos al rango horizontal:
        if (dis_total < rangoDeteccion) // usamos distancia total para la detección y se detiene si es <= rangoParada
        {
            //si esta dentro del rango va a disparars
            if (dis_total < rangoParada)
            {
                animator.SetBool("atacando", true);
                animator.SetBool("camina", false);
                // Disparar con cadencia controlada

                atacar();

            }
            else
            {
                animator.SetBool("camina", true);
                animator.SetBool("atacando", false);
                // Determinar direccion horizontal
                float deltaX = jugador.position.x - transform.position.x;

                // Si ya está prácticamente alineado, no se mueve
                //if (distanciaHorizontal <= umbralParadaHorizontal)
                //{
                //    animator.SetBool("caminando", false);
                //    return;
                //}

                float direccionX = Mathf.Sign(deltaX); // -1 izquierda, 1 derecha
                Vector2 movimiento = new Vector2(direccionX, 0f);
                // Mover enemigo

                rb.MovePosition(rb.position + movimiento * velocidad * Time.fixedDeltaTime);


                // Voltear sprite segun dirección
                if (sprite != null)
                    sprite.flipX = direccionX < 0;
            }

        }
        else
        {

            //Esta en Idle
            animator.SetBool("atacando", false);
            animator.SetBool("camina", false);

        }
    }



    private void atacar()
    {
        if (Time.time < nextShotTime) return;
        ahora_si_atacar();
        nextShotTime = Time.time + intervalo_ataque;
    }
    private void ahora_si_atacar()
    {
        playerSoundController.playDisparo();
        GameManager.Instance.ReduceHealth(dañoGolpe);

    }

    public void QuitarVida(float cantidad)
    {
        Vida -= cantidad;
        if (Vida <= 0)
        {
            Morir();
        }
       
    }

    private void Morir()
    {
        // Desactivar movimiento y colisiones
        enabled = false;
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }
        
        // Activar animación de muerte
        animator.SetBool("muerto", true);
        animator.SetBool("islive", false);
        playerSoundController.playMorir();
        // Destruir después de que termine la animación
        Destroy(gameObject, 1.3f);
    }


}