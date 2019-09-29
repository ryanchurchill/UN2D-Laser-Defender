using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // config

    [Header("Enemy")]
    [SerializeField] int initialHealth = 100;
    [SerializeField] float minTimeBetweenShots = .2f;
    [SerializeField] float maxTimeBetweenShots = 3f;

    [Header("Projectile")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10; // TODO: take from separate config?
    [SerializeField] AudioClip shootingSound;

    [Header("Death")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float deathVFXDuration = 1f;
    [SerializeField] AudioClip deathSound;

    // other
    float secUntilNextShot;
    int health;


    // Start is called before the first frame update
    void Start()
    {
        health = initialHealth;
        ResetShotTimer();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        secUntilNextShot -= Time.deltaTime;
        if (secUntilNextShot <= 0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        AudioSource.PlayClipAtPoint(shootingSound, transform.position);
        GameObject laser = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        ResetShotTimer();
    }

    private void ResetShotTimer()
    {
        secUntilNextShot = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO: should this be in DamageTaker class?
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (damageDealer)
        {
            ProcessHit(damageDealer);
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().increaseScore(initialHealth);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, deathVFXDuration);
        // Udemy recommends playing at camera 
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Destroy(gameObject);
    }
}
