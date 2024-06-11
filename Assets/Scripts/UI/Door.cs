using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] float landingX;
    [SerializeField] float landingY;
    private Vector2 landingPosition;

    private void Awake()
    {
        landingPosition = new Vector2(landingX, landingY);
    }

    private void OnCollisionEnter2D(Collision2D pl)
    {
        if(pl.gameObject.name != "Player")
            return;

        pl.gameObject.transform.position = landingPosition;
    }
}
