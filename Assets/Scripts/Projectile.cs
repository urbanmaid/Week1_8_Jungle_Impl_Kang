using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D theRb;
    public float projectileSpeed;
    //private SpriteRenderer rend;
    public float damage;
    
    [SerializeField] private GameObject projectileParticle;

    protected virtual void Start()
    {
        //rend = GetComponent<SpriteRenderer>();
        theRb = GetComponent<Rigidbody2D>();
        theRb.linearVelocity = transform.right * projectileSpeed;
        
        Invoke(nameof(OnBecameInvisible), 6f);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Player Projectile") && collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            enemy.Damage(damage);
            SpawnParticles();
            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("Enemy Projectile") && collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.DamagePlayer(damage);
            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("Missile Projectile") && collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            SpawnParticles();
            enemy.Damage(damage);
        }
    }

    private void SpawnParticles()
    {
        var particles = Instantiate(projectileParticle, transform.position, Quaternion.identity);
    }
}
