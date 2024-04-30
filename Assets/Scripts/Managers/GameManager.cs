using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager i;
    [SerializeField] PlayerData playerData;
    public PlayerData PlayerData
    {
        get { return playerData; }
    }


    private void Awake()
    {
        i = this;
    }

    public void AddPlayerXP(float amount)
    {
        PlayerXP playerXP = playerData.GetComponent<PlayerXP>();
        playerXP.AddXP(amount);
    }

    


}
