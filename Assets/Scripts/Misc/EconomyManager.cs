using UnityEngine;
using TMPro;

public class EconomyManager : Singleton<EconomyManager>
{
    private TMP_Text goldText;
    //private int currentGold = 0;
    const string COIN_AMOUNT_TEXT = "Gold Amount Text";

    private void Start()
    {
        if (goldText == null)
        {
            goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }
        if(goldText) goldText.text = PlayerPrefs.GetInt(PrefConsts.COIN_AMOUNT, 0).ToString("D3");
    }

    public void UpdateCurrentGold()
    {
        PlayerPrefs.SetInt(PrefConsts.COIN_AMOUNT, PlayerPrefs.GetInt(PrefConsts.COIN_AMOUNT, 0) + 1);
        if (goldText == null)
        {
            goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }
        if(goldText) goldText.text = PlayerPrefs.GetInt(PrefConsts.COIN_AMOUNT, 0).ToString("D3");
    }
    
}
