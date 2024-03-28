using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    
    public void useMana(float amount)
    {
        if(playerStats.CurrentMana >= amount)
        {
            playerStats.CurrentMana -= amount;
        }
    }

    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            useMana(5);
        }
    }*/
}
