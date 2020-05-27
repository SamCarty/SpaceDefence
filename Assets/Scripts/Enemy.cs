using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] int health = 1;
    [SerializeField] int scoreOnKill = 1;
    
    [Header("Weapons")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 10f;
    
    [Header("Particle System")]
    [SerializeField] GameObject explosionPrefab;

    [Header("Audio")] 
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip laserSound;
    
    Camera camera;
    
    void Start()
    {
        camera = Camera.main;
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    void Update()
    {
        CountDownAndShoot();
    }

    void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    void Fire() {
        if (camera != null) AudioSource.PlayClipAtPoint(laserSound, camera.transform.position);
        
        var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) return;
        ProcessHit(damageDealer);
    }

    void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0) {
            Kill();
            Destroy(gameObject);
        }
    }

    void Kill() {
        // update the score
        FindObjectOfType<CurrentGame>().AddToScore(scoreOnKill);
        
        // play the death sound effect at the camera's position
        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position);
        
        // play the death animation effect at the player's last position
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        ParticleSystem vfx = explosion.GetComponent<ParticleSystem>();
        float waitTime = vfx.main.duration;
        
        // finally, remove our enemy from the world
        Destroy(vfx, waitTime);
    }
}