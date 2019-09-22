﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") *  Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") *  Time.deltaTime * moveSpeed;
        float newXPos = transform.position.x + deltaX;
        float newYPos = transform.position.y + deltaY;
        transform.position = new Vector2(newXPos, newYPos);
    }
}
