using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed;

    public Vector3 Direction { get; set; }


    void Update()
    {
        transform.Translate(Direction * (projectileSpeed * Time.deltaTime));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        print("Triggered");
    }
}
