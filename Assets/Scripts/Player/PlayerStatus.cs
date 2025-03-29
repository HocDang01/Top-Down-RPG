using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : Singleton<PlayerStatus>
{
    public Sprite actived;
    public Sprite inactived;
    public SettingDialog settingDialog;
    public Button ShopBtn;
    public Button exitShopBtn;

    public Transform healthContent;
    public Transform swordContent;
    public Transform bowContent;
    public Transform staffContent;

    private TMP_Text goldText;
    const string COIN_AMOUNT_TEXT = "Gold Amount Text";

    private int maxStatus = 6;

    protected override void Awake()
    {
        base.Awake();   
    }
    private void Start()
    {
        UpdateActived();
        if (ShopBtn) ShopBtn.onClick.AddListener(OpenShop);
        if (exitShopBtn) exitShopBtn.onClick.AddListener(ExitShop);
    }

    public void UpgradeHealth()
    {
        if (PlayerPrefs.GetInt(PrefConsts.MAX_HEALTH_STATUS, 2) >= maxStatus) return;
        int price = PriceToUpgrade(PlayerPrefs.GetInt(PrefConsts.MAX_HEALTH_STATUS, 2));
        if (price > PlayerPrefs.GetInt(PrefConsts.COIN_AMOUNT, 0))
        {
            Debug.Log("Not enough coin!");
            return;
        }
        PlayerPrefs.SetInt(PrefConsts.COIN_AMOUNT, PlayerPrefs.GetInt(PrefConsts.COIN_AMOUNT, 0) - price);
        UpdateCoin();

        PlayerPrefs.SetInt(PrefConsts.MAX_HEALTH, PlayerPrefs.GetInt(PrefConsts.MAX_HEALTH, 3) + 2);
        PlayerPrefs.SetInt(PrefConsts.MAX_HEALTH_STATUS, PlayerPrefs.GetInt(PrefConsts.MAX_HEALTH_STATUS, 2) + 1);
        UpdateActived();

        if (PlayerHealth.Instance != null)
        {
            PlayerHealth.Instance.MaxHealth += 2;
            PlayerHealth.Instance.HealPlayer(2);
        }

        //Debug.Log("Upgraded Max Health: " + PlayerPrefs.GetInt(PrefConsts.MAX_HEALTH, 3));
    }
    public void UpgradeSword()
    {
        if (PlayerPrefs.GetInt(PrefConsts.SWORD_DAME_STATUS, 2) >= maxStatus) return;

        int price = PriceToUpgrade(PlayerPrefs.GetInt(PrefConsts.SWORD_DAME_STATUS, 2));
        if (price > PlayerPrefs.GetInt(PrefConsts.COIN_AMOUNT, 0))
        {
            Debug.Log("Not enough coin!");
            return;
        }
        PlayerPrefs.SetInt(PrefConsts.COIN_AMOUNT, PlayerPrefs.GetInt(PrefConsts.COIN_AMOUNT, 0) - price);
        UpdateCoin();

        PlayerPrefs.SetInt(PrefConsts.SWORD_DAME, PlayerPrefs.GetInt(PrefConsts.SWORD_DAME, 2) + 1);
        PlayerPrefs.SetInt(PrefConsts.SWORD_DAME_STATUS, PlayerPrefs.GetInt(PrefConsts.SWORD_DAME_STATUS, 2) + 1);
        UpdateActived();

        //Debug.Log("Upgraded Sword Damage: " + PlayerPrefs.GetInt(PrefConsts.SWORD_DAME, 2));
    }

    public void UpgradeBow()
    {
        if (PlayerPrefs.GetInt(PrefConsts.BOW_DAME_STATUS, 2) >= maxStatus) return;

        int price = PriceToUpgrade(PlayerPrefs.GetInt(PrefConsts.BOW_DAME_STATUS, 2));
        if (price > PlayerPrefs.GetInt(PrefConsts.COIN_AMOUNT, 0))
        {
            Debug.Log("Not enough coin!");
            return;
        }
        PlayerPrefs.SetInt(PrefConsts.COIN_AMOUNT, PlayerPrefs.GetInt(PrefConsts.COIN_AMOUNT, 0) - price);
        UpdateCoin();

        PlayerPrefs.SetInt(PrefConsts.BOW_DAME, PlayerPrefs.GetInt(PrefConsts.BOW_DAME, 1) + 1);
        PlayerPrefs.SetInt(PrefConsts.BOW_DAME_STATUS, PlayerPrefs.GetInt(PrefConsts.BOW_DAME_STATUS, 2) + 1);
        UpdateActived();

        //Debug.Log("Upgraded Bow Damage: " + PlayerPrefs.GetInt(PrefConsts.BOW_DAME, 1));
    }

    public void UpgradeStaff()
    {
        if (PlayerPrefs.GetInt(PrefConsts.STAFF_DAME_STATUS, 2) >= maxStatus) return;

        int price = PriceToUpgrade(PlayerPrefs.GetInt(PrefConsts.STAFF_DAME_STATUS, 2));
        if (price > PlayerPrefs.GetInt(PrefConsts.COIN_AMOUNT, 0))
        {
            Debug.Log("Not enough coin!");
            return;
        }
        PlayerPrefs.SetInt(PrefConsts.COIN_AMOUNT, PlayerPrefs.GetInt(PrefConsts.COIN_AMOUNT, 0) - price);
        UpdateCoin();

        PlayerPrefs.SetInt(PrefConsts.STAFF_DAME, PlayerPrefs.GetInt(PrefConsts.STAFF_DAME, 3) + 1);
        PlayerPrefs.SetInt(PrefConsts.STAFF_DAME_STATUS, PlayerPrefs.GetInt(PrefConsts.STAFF_DAME_STATUS, 2) + 1);
        UpdateActived();

        //Debug.Log("Upgraded Staff Damage: " + PlayerPrefs.GetInt(PrefConsts.STAFF_DAME, 3));
    }
    private int PriceToUpgrade(int status)
    {
        switch (status)
        {
            case 2: return 2;
            case 3: return 5;
            case 4: return 10;
            case 5: return 20;
            default: return 0;
        }
    }
    private void UpdateCoin()
    {
        if (goldText == null)
        {
            goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }
        if (goldText) goldText.text = PlayerPrefs.GetInt(PrefConsts.COIN_AMOUNT, 0).ToString("D3");
    }
    public void UpdateActived()
    {
        if (healthContent)
        {
            for (int i = 0; i < PlayerPrefs.GetInt(PrefConsts.MAX_HEALTH_STATUS, 2); i++)
            {
                healthContent.GetChild(i).GetComponent<Image>().sprite = actived;
            }
            for (int i = PlayerPrefs.GetInt(PrefConsts.MAX_HEALTH_STATUS, 2); i < maxStatus; i++)
            {
                healthContent.GetChild(i).GetComponent<Image>().sprite = inactived;
            }
        }
        if (swordContent)
        {
            for (int i = 0; i < PlayerPrefs.GetInt(PrefConsts.SWORD_DAME_STATUS, 2); i++)
            {
                swordContent.GetChild(i).GetComponent<Image>().sprite = actived;
            }
            for (int i = PlayerPrefs.GetInt(PrefConsts.SWORD_DAME_STATUS, 2); i < maxStatus; i++)
            {
                swordContent.GetChild(i).GetComponent<Image>().sprite = inactived;
            }
        }

        if (bowContent)
        {
            for (int i = 0; i < PlayerPrefs.GetInt(PrefConsts.BOW_DAME_STATUS, 2); i++)
            {
                bowContent.GetChild(i).GetComponent<Image>().sprite = actived;
            }
            for (int i = PlayerPrefs.GetInt(PrefConsts.BOW_DAME_STATUS, 2); i < maxStatus; i++)
            {
                bowContent.GetChild(i).GetComponent<Image>().sprite = inactived;
            }
        }

        if (staffContent)
        {
            for (int i = 0; i < PlayerPrefs.GetInt(PrefConsts.STAFF_DAME_STATUS, 2); i++)
            {
                staffContent.GetChild(i).GetComponent<Image>().sprite = actived;
            }
            for (int i = PlayerPrefs.GetInt(PrefConsts.STAFF_DAME_STATUS, 2); i < maxStatus; i++)
            {
                staffContent.GetChild(i).GetComponent<Image>().sprite = inactived;
            }
        }
           

       
    }
    public void Exit()
    {
        if (settingDialog) settingDialog.Show(false);
    }
    public void OpenShop()
    {
        if(settingDialog)
        {
            Time.timeScale = 0f;
            settingDialog.Show(true);
            UpdateActived();
        }
    }
    public void ExitShop()
    {
        if (settingDialog)
        {
            Time.timeScale = 1f;
            settingDialog.Show(false);
        }
    }
}
