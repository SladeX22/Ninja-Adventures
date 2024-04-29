using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager i;

    [SerializeField] GameObject player;

    private void Awake()
    {
        i = this;
    }

    public void AddPlayerXP(float amount)
    {
        PlayerXP playerXP = player.GetComponent<PlayerXP>();
        playerXP.AddXP(amount);

    }
}
