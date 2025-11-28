using UnityEngine;

public class arma_BOSS : MonoBehaviour
{

    public Transform spawnPoint;
    public GameObject bulletPrefab;
    public PlayerSoundController soundController;
    public float shotForce = 80f;
    public float shotRate = 1f; // disparar cada segundo por defecto

    public float nextShotTime = 0f;

    private Transform player;


    //public SpriteRenderer spawnbala; // Para voltear el personaje


    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        // Apuntar siempre hacia el jugador
        if (player != null && spawnPoint != null)
        {
            Vector2 dir = (player.position - spawnPoint.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            spawnPoint.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    // Llamado externamente para disparar si ya pasó el cooldown
    //public void ahora_Shoot()
    //{
    //    if (player == null) return;
    //    if (Time.time < nextShotTime) return;
    //    Shoot();
    //    nextShotTime = Time.time + shotRate;
    //}


    void Shoot()
    {
        soundController.playDisparo();
        if (player == null) return;
        if (spawnPoint == null || bulletPrefab == null) return;

        // Dirección hacia el jugador desde el spawn
        Vector2 direction = (player.position - spawnPoint.position).normalized;

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

        // Destruir la bala despu�s de 1 segundo
        Destroy(newBullet, .5f);


    }
}
