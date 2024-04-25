using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;

    [Header("Player UI Bars")]
    [SerializeField] Image healthBar;
    [SerializeField] Image manaBar;
    [SerializeField] Image xpBar;

    [Header("Stat Panel")]
    [SerializeField] GameObject statsPanel;
    [SerializeField] TextMeshProUGUI statLevel;
    [SerializeField] TextMeshProUGUI statDamage;
    [SerializeField] TextMeshProUGUI statCChance;
    [SerializeField] TextMeshProUGUI statCDamage;
    [SerializeField] TextMeshProUGUI statTotalXP;
    [SerializeField] TextMeshProUGUI statCurrentXP;
    [SerializeField] TextMeshProUGUI statNextLevelXP;

    [Header("Player UI Text")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI manaText;
    [SerializeField] TextMeshProUGUI xpText;

    private void Update()
    {
        UpdatePlayerUI();
    }

    public void ToggleStatsPanel()
    {
        statsPanel.SetActive(!statsPanel.activeSelf);
        if(statsPanel.activeSelf)
        {
            UpdateStatsPanel();
        }
    }

    void UpdateStatsPanel()
    {
        statLevel.text = playerStats.CurrentLevel.ToString();
        statDamage.text = playerStats.TotalDamage.ToString();
        statCChance.text = playerStats.CriticalChance.ToString();
        statCDamage.text = playerStats.CriticalDamage.ToString();
        statTotalXP.text = playerStats.TotalXP.ToString();
        statCurrentXP.text = playerStats.CurrentXP.ToString();
        statNextLevelXP.text = playerStats.NextLevelXP.ToString();
    }

    void UpdatePlayerUI()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, playerStats.CurrentHealth / playerStats.MaxHealth, 10f * Time.deltaTime);
        manaBar.fillAmount = Mathf.Lerp(manaBar.fillAmount, playerStats.CurrentMana / playerStats.MaxMana, 10f * Time.deltaTime);
        xpBar.fillAmount = Mathf.Lerp(xpBar.fillAmount, playerStats.CurrentXP / playerStats.NextLevelXP, 10f * Time.deltaTime);

        levelText.text = $"Level {playerStats.CurrentLevel}";
        healthText.text = $"{playerStats.CurrentHealth} / {playerStats.MaxHealth}";
        manaText.text = $"{playerStats.CurrentMana} / {playerStats.MaxMana}";
        healthText.text = $"{playerStats.CurrentXP} / {playerStats.NextLevelXP}";

    }

}
