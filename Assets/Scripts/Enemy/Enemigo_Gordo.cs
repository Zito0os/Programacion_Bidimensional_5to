using UnityEngine;
using System.Collections;

public class EnemigoGordo : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float rangoAtaque = 1.8f;
    [SerializeField] private float fuerzaEmpuje = 8f;
    [SerializeField] private float dañoGolpe = 10f;
    [SerializeField] private float tiempoEntreGolpes = 0.4f;   // ← Cuanto más bajo, más rápido ataca

    // ← AQUÍ ESTABA EL ERROR: faltaba la variable vida
    [SerializeField] private float vida = 250f;

    private Transform jugador;
    private Animator animator;
    private float ultimoGolpe = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        jugador = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (jugador == null) return;

        float distancia = Vector2.Distance(transform.position, jugador.position);

        // Voltear sprite
        if (jugador.position.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

        // ATAQUE CONSTANTE mientras esté dentro del rango
        if (distancia <= rangoAtaque)
        {
            animator.SetBool("atacando", true);

            if (Time.time >= ultimoGolpe + tiempoEntreGolpes)
            {
                Golpear();
                ultimoGolpe = Time.time;
            }
        }
        else
        {
            animator.SetBool("atacando", false);
        }
    }

    void Golpear()
    {
        // Daño
        GameManager.Instance.ReduceHealth((int)dañoGolpe);

        // Empuje
        Rigidbody2D rbJugador = jugador.GetComponent<Rigidbody2D>();
        if (rbJugador != null)
        {
            Vector2 direccion = (jugador.position - transform.position).normalized;
            rbJugador.AddForce(direccion * fuerzaEmpuje, ForceMode2D.Impulse);
        }

        // Opcional: sonido, partículas, pantalla shake…
    }

    // ← FUNCIÓN PARA RECIBIR DAÑO (desde la bala)
    public void QuitarVida(float cantidad)
    {
        vida -= cantidad;

        // Opcional: flash rojo
        StartCoroutine(FlashRojo());

        if (vida <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        animator.SetBool("muerto", true);
        animator.SetBool("islive", false);
        enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        Destroy(gameObject, 2f);
    }

    // Flash rojo al recibir daño (queda brutal)
    IEnumerator FlashRojo()
    {
        GetComponent<SpriteRenderer>().color = new Color(1f, 0.3f, 0.3f);
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoAtaque);
    }
}