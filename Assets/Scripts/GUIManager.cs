using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    public MenuDialog menuDialog;
    public SettingDialog settingDialog;
    public AboutDialog aboutDialog;

    private void Start()
    {
        menuDialog.Show(true);
        settingDialog.Show(false);
        aboutDialog.Show(false);
    }
    public void BackToMenu()
    {
        if (menuDialog && settingDialog && aboutDialog)
        {
            settingDialog.Show(false);
            aboutDialog.Show(false);
            menuDialog.Show(true);
        }
    }

    public void ToggleSetting()
    {
        if (menuDialog && settingDialog && aboutDialog)
        {
            menuDialog.Show(false);
            aboutDialog.Show(false);
            settingDialog.Show(true);
            AudioController.Instance.FindUIElements();
        }
    }
    public void ToggleAbout()
    {
        if (menuDialog && settingDialog && aboutDialog)
        {
            menuDialog.Show(false);
            settingDialog.Show(false);
            aboutDialog.Show(true);
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Scene1");
        PlayerPrefs.SetString(PrefConsts.SCENE, "Scene1");
        RestartStatus();
    }

    public void Continue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString(PrefConsts.SCENE));
    }
        
    private void RestartStatus()
    {
        PlayerPrefs.SetInt(PrefConsts.COIN_AMOUNT, 0);

        PlayerPrefs.SetInt(PrefConsts.MAX_HEALTH, 3);
        PlayerPrefs.SetInt(PrefConsts.MAX_HEALTH_STATUS, 2);

        PlayerPrefs.SetInt(PrefConsts.SWORD_DAME, 2);
        PlayerPrefs.SetInt(PrefConsts.SWORD_DAME_STATUS, 2);

        PlayerPrefs.SetInt(PrefConsts.BOW_DAME, 1);
        PlayerPrefs.SetInt(PrefConsts.BOW_DAME_STATUS, 2);

        PlayerPrefs.SetInt(PrefConsts.STAFF_DAME, 3);
        PlayerPrefs.SetInt(PrefConsts.STAFF_DAME_STATUS, 2);
    }
}
