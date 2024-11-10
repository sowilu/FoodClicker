using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Baker")]
    public TextMeshProUGUI priceText;
    public int price = 10;
    public TextMeshProUGUI countText;
    public int count = 0;
    public int cpb = 1;//clicks per baker
    public float bakerSpeed = 2f;

    private Clicker clicker;

    private void Start() 
    {
        clicker = FindObjectOfType<Clicker>();
        InvokeRepeating("Cook", 0, bakerSpeed);
    }


    public void BuyBaker()
    {
        if (clicker.clicks >= price)
        {
            clicker.clicks -= price;
            UiManager.instance.UpdateClicks(clicker.clicks);
            
            count++;
            countText.text = count.ToString();

            price = (int)(price * 1.1f);//price increase 10%;
            priceText.text = $"Price: {price}";
        }
    }

    void Cook()
    {
        clicker.clickVFX.Emit(cpb * count);
        clicker.clicks += cpb * count;
        UiManager.instance.UpdateClicks(clicker.clicks);
    }
}
