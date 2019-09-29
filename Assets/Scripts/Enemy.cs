using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // config
    [SerializeField] float health = 100;
    [SerializeField] float minTimeBetweenShots = .2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10; // TODO: take from separate config?
    [SerializeField] GameObject deathVFX;
    [SerializeField] float deathVFXDuration = 1f;

    // other
    [SerializeField] float secUntilNextShot;


    // Start is called before the first frame update
    void Start()
    {
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
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, deathVFXDuration);
    }
}
