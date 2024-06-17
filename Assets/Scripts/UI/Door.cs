using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject landing;
    [SerializeField] bool exit;

    private void OnCollisionEnter2D(Collision2D pl)
    {
        if(pl.gameObject.name != "Player")
            return;

        var ltp = landing.transform.position;
        
        if(exit)
            pl.transform.position = new Vector2(ltp.x, ltp.y + 1.25f);
        else
            pl.transform.position = new Vector2(ltp.x, ltp.y - 1.25f);
    }
}
