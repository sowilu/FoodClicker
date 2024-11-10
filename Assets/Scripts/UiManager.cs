using TMPro;
using UnityEngine;

//singleton pattern - only one instance of this class can exist
//can be accessed from any other script
public class UiManager : MonoBehaviour
{
    //step 1: create a static instance of this class
    public static UiManager instance;
    public TextMeshProUGUI clickText;
    public TextMeshProUGUI cpsText;

    //step 2: method thats executed earlier than Start()
    private void Awake()
    {
        //step 3: check if ui manager does not exists yet
        if(instance == null)
        {
            //step 4: set this instance as the ui manager
            instance = this;
        }
        else
        {
            //step 5: destroy this instance if another ui manager exists
            Destroy(gameObject);
        }
    }

    public void UpdateClicks(int clicks)
    {
        clickText.text = clicks.ToString();
    }

    public void UpdateCps(int cps)
    {
        cpsText.text = $"{cps} cps";
    }   
}
