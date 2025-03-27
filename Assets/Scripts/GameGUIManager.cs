using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGUIManager : Singleton<GameGUIManager>
{
    public Button pauseBtn;
    public Button resumeBtn;
    public Button restartBtn;
    public Button homeBtn;
    public SettingDialog settingDialog;

    protected override void Awake()
    {
        DestroyOnScene0(false);
    }

    private void Start()
    {

        if (pauseBtn) pauseBtn.onClick.AddListener(Pause);
        if (resumeBtn) resumeBtn.onClick.AddListener(Resume);
        if (restartBtn) restartBtn.onClick.AddListener(Restart);
        if (homeBtn) homeBtn.onClick.AddListener(Home);
        FindUIElements();
    }


    public void FindUIElements()
    {
        if (pauseBtn == null)
        {
            pauseBtn = GameObject.Find(Consts.PAUSE_BTN)?.GetComponent<Button>();
            if (pauseBtn) pauseBtn.onClick.AddListener(Pause);
        }
        if (resumeBtn == null)
        {
            resumeBtn = GameObject.Find(Consts.RESUME_BTN)?.GetComponent<Button>();
            if(resumeBtn) resumeBtn.onClick.AddListener(Resume);
        }
        if (restartBtn == null)
        {
            restartBtn = GameObject.Find(Consts.RESTART_BTN)?.GetComponent<Button>();
            if (restartBtn) restartBtn.onClick.AddListener(Restart);
        }
        if (homeBtn == null)
        {
            homeBtn = GameObject.Find(Consts.HOME_BTN)?.GetComponent<Button>();
            if (homeBtn) homeBtn.onClick.AddListener(Home);
        }
        if (settingDialog == null)
        {
            settingDialog = GameObject.Find(Consts.SETTING_DIALOG)?.GetComponent<SettingDialog>();
        }
    }

    public void Pause()
    {
        if (settingDialog)
        {
            Time.timeScale = 0f;
            settingDialog.Show(true);
            AudioController.Instance.FindUIElements();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        if (settingDialog)
        {
            settingDialog.Show(false);
        }
    }
    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scene0");
        if (settingDialog)
        {
            settingDialog.Show(false);
        }
    }
    public void Restart()
    {
        Resume();
        SceneManager.LoadScene(PlayerPrefs.GetString(PrefConsts.SCENE));
    }
}
