using UnityEngine;

public class Gun2D : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bulletPrefab;
    public Transform crosshair;

    public float shotForce = 15f;
    public float shotRate = 0.5f;

    private float nextShotTime = 0f;


    public SpriteRenderer spawnbala; // Para voltear el personaje



    void Update()
    {
        //spawnbala.flipY = true;
        //seguir mirilla
        Mirar_mirilla();

        // Disparo con click izquierdo
        if (Input.GetButtonDown("Fire1") && Time.time > nextShotTime && GameManager.Instance.gunammo > 0)
        {
            Shoot();
        }


    }

    private void Mirar_mirilla()
    {

        Vector3 dir = crosshair.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        
    }
    void Shoot()
    {
        GameManager.Instance.gunammo--;

        // Posición del mouse en el mundo
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // Dirección desde el arma (NO desde el spawnPoint rotado)
        Vector2 direction = (mousePos - transform.position).normalized;
        //Vector2 direction = (mousePos - spawnPoint.position).normalized;

        // Instanciar bala en el spawnPoint
        GameObject newBullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);

        // Reset z y escala
        Vector3 pos = newBullet.transform.position;
        pos.z = 0f;
        newBullet.transform.position = pos;
        newBullet.transform.localScale = Vector3.one;

        // Rotar la bala para que apunte hacia la dirección del disparo
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        newBullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Aplicar fuerza
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * shotForce, ForceMode2D.Impulse);

        // Tiempo de espera para próximo disparo
        nextShotTime = Time.time + shotRate;

        // Destruir la bala después de 1 segundo
        Destroy(newBullet, .5f);
    }


 
}
