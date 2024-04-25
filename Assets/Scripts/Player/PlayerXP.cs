using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;

    public void AddXP(float amount)
    {
        playerStats.TotalXP += amount;
        playerStats.CurrentXP += amount;

        while(playerStats.CurrentXP >= playerStats.NextLevelXP)
        {
            playerStats.CurrentXP -= playerStats.NextLevelXP;
            //NextLevel();
        }
    }
}
