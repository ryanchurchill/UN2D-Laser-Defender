using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // config
    [Header("Player")]
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float padding = .5f;
    [SerializeField] int health = 200;
    [SerializeField] AudioClip deathSound;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10;
    [SerializeField] float firePaddingSeconds = .1f;
    [SerializeField] AudioClip shootingSound;

    // other
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    Coroutine fireContinuouslyCr;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        MoveFromInput();
        FireFromInput();
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void MoveFromInput()
    {
        float deltaX = Input.GetAxis("Horizontal") *  Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") *  Time.deltaTime * moveSpeed;

        float newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        float newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void FireFromInput()
    {
        if (Input.GetButtonDown("Fire1") && fireContinuouslyCr == null)
        {
            fireContinuouslyCr = StartCoroutine(FireContinuouslyCoroutine());
        } else if (Input.GetButtonUp("Fire1") && fireContinuouslyCr != null)
        {
            StopCoroutine(fireContinuouslyCr);
            fireContinuouslyCr = null;
        }
    }

    private IEnumerator FireContinuouslyCoroutine()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(firePaddingSeconds);
        }
    }

    private void Fire()
    {
        AudioSource.PlayClipAtPoint(shootingSound, transform.position);
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
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
        StartCoroutine(FindObjectOfType<Level>().LoadGameOverWithDelay());
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        //Destroy(gameObject);
    }
}
