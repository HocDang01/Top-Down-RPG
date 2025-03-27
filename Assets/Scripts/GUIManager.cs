using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    public MenuDialog menuDialog;
    public SettingDialog settingDialog;

    private void Start()
    {
        menuDialog.Show(true);
        settingDialog.Show(false);
    }
    public void BackToMenu()
    {
        if (menuDialog && settingDialog)
        {
            settingDialog.Close();
            menuDialog.Show(true);
        }
    }

    public void ToggleSetting()
    {
        if (menuDialog && settingDialog)
        {
            menuDialog.Close();
            settingDialog.Show(true);
            AudioController.Instance.FindUIElements();
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Scene1");
        PlayerPrefs.SetString(PrefConsts.SCENE, "Scene1");
    }

    public void Continue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString(PrefConsts.SCENE));
    }
}
