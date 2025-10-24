using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{

    public float velocidad = 3f;
    public float rangoDeteccion = 15f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;


    public Transform jugador; // Visible en el inspector, pero se llenará solo




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        if (jugador == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
                jugador = playerObj.transform;
        }
    }

    void FixedUpdate()
    {
        if (jugador == null) return;

        float distancia = Vector2.Distance(transform.position, jugador.position);

        // Solo sigue si está dentro del rango
        if (distancia < rangoDeteccion)
        {
            Vector2 direccion = (jugador.position - transform.position).normalized;
            rb.MovePosition(rb.position + direccion * velocidad * Time.fixedDeltaTime);

            // Activar animación de caminar
            animator.SetBool("caminando", true);

            // Voltear sprite según dirección
            if (sprite != null)
                sprite.flipX = direccion.x < 0;
        }
        else
        {
            // Detener animación
            animator.SetBool("caminando", false);
        }

    }



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
