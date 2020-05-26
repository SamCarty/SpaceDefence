using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private float health = 100f;
    
    [Header("Weapons")]
    [SerializeField] private float shotCounter;
    [SerializeField] private float minTimeBetweenShots = 0.2f;
    [SerializeField] private float maxTimeBetweenShots = 3f;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float laserSpeed = 10f;
    
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

    private void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire() {
        if (camera != null) AudioSource.PlayClipAtPoint(laserSound, camera.transform.position);
        
        var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) return;
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0) {
            PlayDeathAnimation();
            Destroy(gameObject);
        }
    }

    private void PlayDeathAnimation() {
        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position);
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        ParticleSystem vfx = explosion.GetComponent<ParticleSystem>();
        float waitTime = vfx.main.duration;
        Destroy(vfx, waitTime);
    }
}