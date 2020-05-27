using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config params
    [Header("Player")]
    [SerializeField] private float health = 300f;
    [SerializeField] private float moveSpeed = 10f;
    
    [Header("Weapons")]
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float laserSpeed = 10f;
    [SerializeField] private float laserFiringPeriod = 0.1f;
    
    [Header("Padding")]
    [SerializeField] private float xPadding = .5f;
    [SerializeField] private float yPadding = .5f;

    [Header("Particle System")]
    [SerializeField] GameObject explosionPrefab;
    
    [Header("Audio")] 
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip laserSound;
    
    // Local variables
    private Coroutine firingCoroutine;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    Camera camera;

    void Start()
    {
        SetUpMoveBoundaries();
        camera = Camera.main;
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        if (gameCamera != null)
        {
            xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
            xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
            yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
            yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;
        }
    }

    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        var currentPosition = transform.position;
        
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        
        var newXPos = Mathf.Clamp(currentPosition.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(currentPosition.y + deltaY, yMin, yMax);
        
        currentPosition = new Vector2(newXPos, newYPos);
        transform.position = currentPosition;
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true) {
            if (camera != null) AudioSource.PlayClipAtPoint(laserSound, camera.transform.position);
            
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            yield return new WaitForSeconds(laserFiringPeriod);
        }
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
        if (health <= 0)
        {
            Kill();
            Destroy(gameObject);
        }
    }
    
    private void Kill() {
        // play the death sound effect at the camera's position
        AudioSource.PlayClipAtPoint(explosionSound, camera.transform.position);
        
        // play the death animation effect at the player's last position
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        ParticleSystem vfx = explosion.GetComponent<ParticleSystem>();
        float waitTime = vfx.main.duration;
        
        // change the scene to the game over scene
        FindObjectOfType<LevelLoader>().LoadGameOver();
        
        // finally, remove our player from the world
        Destroy(vfx, waitTime);
    }
}
