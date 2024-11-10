using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("Baker")]
    public ShopButton bakerButton;
    public float bakerPrice = 10;
    public int bakerCount = 0;
    public int cpb = 1; // Clicks per baker
    public float cookTime = 1; // Time to cook 1 click

    [Header("Upgrades")]
    public ShopButton upgradeButton;
    public float upgradePrice = 100;
    public int upgradeCount = 0;


    private Clicker clicker;

    private void Start() 
    {
        bakerCount = PlayerPrefs.GetInt("bakerCount", 0);
        bakerPrice = PlayerPrefs.GetFloat("bakerPrice", 10);
        bakerButton.UpdateText((int)Mathf.Ceil(bakerPrice), bakerCount);
        upgradeCount = PlayerPrefs.GetInt("upgradeCount", 0);
        upgradePrice = PlayerPrefs.GetFloat("upgradePrice", 100);
        upgradeButton.UpdateText((int)Mathf.Ceil(upgradePrice), upgradeCount);

        if(upgradeCount >= 3)
        {
            upgradeButton.GetComponent<Button>().interactable = false;
        }

        clicker = FindObjectOfType<Clicker>();
        InvokeRepeating("Cook", 0, cookTime);
    }

    public void BuyBaker()
    {
        var realPrice = (int)Mathf.Ceil(bakerPrice);
        if (clicker.clicks >= realPrice)
        {
            clicker.clicks -= realPrice;
            UiManager.instance.UpdateClicks(clicker.clicks);

            bakerPrice *= 1.15f; // 15% increase
            realPrice = (int)Mathf.Ceil(bakerPrice);
            bakerButton.UpdateText(realPrice, ++bakerCount);
        }
    }

    public void Cook()
    {
        var particleCount = Mathf.Min(bakerCount * cpb, 100);
        clicker.clickParticles.Emit(particleCount);
        
        clicker.clicks += bakerCount * cpb;
        UiManager.instance.UpdateClicks(clicker.clicks);
    }

    private void OnApplicationPause(bool pauseStatus) 
    {
        if(pauseStatus)
        {
            Save();
        }
    }

    private void OnApplicationQuit() 
    {
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetInt("bakerCount", bakerCount);
        PlayerPrefs.SetFloat("bakerPrice", bakerPrice);
        PlayerPrefs.SetInt("upgradeCount", upgradeCount);
        PlayerPrefs.SetFloat("upgradePrice", upgradePrice);
        PlayerPrefs.Save();
    }

    public void BuyUpgrade()
    {
        var realPrice = (int)Mathf.Ceil(upgradePrice);
        if (clicker.clicks >= realPrice)
        {
            clicker.clicks -= realPrice;
            UiManager.instance.UpdateClicks(clicker.clicks);

            upgradePrice *= 2; // 100% increase
            realPrice = (int)Mathf.Ceil(upgradePrice);
            upgradeButton.UpdateText(realPrice, ++upgradeCount);

            clicker.UpgradeClicker();

            if(upgradeCount >= 3)
            {
                upgradeButton.GetComponent<Button>().interactable = false;
            }
        }
    }
}
