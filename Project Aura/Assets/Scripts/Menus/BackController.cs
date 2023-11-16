using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackController : MonoBehaviour
{
    [SerializeField] private Vector2 velocityMovement;

    private Vector2 offset;

    private Material material;

    private Rigidbody2D playerRb;

    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        offset =(playerRb.velocity.x/10)* velocityMovement * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
