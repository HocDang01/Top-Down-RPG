using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioController : Singleton<AudioController>
{
    [Header("Main Settings:")]
    public Slider musicSlider;
    public Slider sfxSlider;

    public Button musicBtn;
    public Button sfxBtn;
    public Image musicIcon;
    public Image sfxIcon;

    public Sprite musicOnSprite; 
    public Sprite musicOffSprite;
    public Sprite sfxOnSprite;   
    public Sprite sfxOffSprite;   

    public AudioSource musicAus;
    public AudioSource sfxAus;

    [Header("Game sounds and musics: ")]
    public AudioClip slimeAttack;
    public AudioClip enemyShoot;
    public AudioClip playerSword;
    public AudioClip playerBow;
    public AudioClip playerStaff;
    public AudioClip win;
    public AudioClip playerHitted;
    public AudioClip enemyHitted;
    public AudioClip playerDie;
    public AudioClip enemyDie;
    public AudioClip eatCoin;
    public AudioClip eatHealth;
    public AudioClip eatStamina;
    public AudioClip[] backgroundMusics;


    protected override void Awake()
    {
        DestroyOnScene0(false);
    }
    private void Start()
    {
        if (musicSlider && sfxSlider)
        {
            musicSlider.value = PlayerPrefs.GetFloat(PrefConsts.MUSIC_VOLUME, 1f);
            sfxSlider.value = PlayerPrefs.GetFloat(PrefConsts.SFX_VOLUME, 1f);

            musicSlider.onValueChanged.AddListener(SetMusicVolume);
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);

            if (musicBtn) musicBtn.onClick.AddListener(ToggleMusic);
            if (sfxBtn) sfxBtn.onClick.AddListener(ToggleSFX);

            UpdateMusicIcon();
            UpdateSfxIcon();
        }
        PlayBackgroundMusic();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        

    }
    public void FindUIElements()
    {
        if (musicSlider == null)
        {
            musicSlider = GameObject.Find(Consts.MUSIC_SLIDER)?.GetComponent<Slider>();
            musicSlider.value = PlayerPrefs.GetFloat(PrefConsts.MUSIC_VOLUME, 1f);
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }
        if (sfxSlider == null)
        {
            sfxSlider = GameObject.Find(Consts.SFX_SLIDER)?.GetComponent<Slider>();
            sfxSlider.value = PlayerPrefs.GetFloat(PrefConsts.SFX_VOLUME, 1f);
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }
        if (musicBtn == null)
        {
            musicBtn = GameObject.Find(Consts.MUSIC_BTN)?.GetComponent<Button>();
            if (musicBtn) musicBtn.onClick.AddListener(ToggleMusic);
        }
        if (sfxBtn == null)
        {
            sfxBtn = GameObject.Find(Consts.SFX_BTN)?.GetComponent<Button>();
            if (sfxBtn) sfxBtn.onClick.AddListener(ToggleSFX);
        }
        if (musicIcon == null)
        {
            musicIcon = GameObject.Find(Consts.MUSIC_BTN)?.GetComponent<Image>();
            UpdateMusicIcon();
        }
        if (sfxIcon == null)
        {
            sfxIcon = GameObject.Find(Consts.SFX_BTN)?.GetComponent<Image>();
            UpdateSfxIcon();
        }
    }
    public void ToggleMusic()
    {
        if (!musicBtn) return;
        if (musicAus.volume > 0)
        {
            SetMusicVolume(0);
            musicSlider.value = 0;
        }
        else if (musicAus.volume <= 0)
        {
            SetMusicVolume(0.1f);
            musicSlider.value = 0.1f;
        }
    }
    public void ToggleSFX()
    {
        if (!sfxBtn) return;
        if (sfxAus.volume > 0)
        {
            SetSFXVolume(0);
            sfxSlider.value = 0;
        }
        else if (sfxAus.volume <= 0)
        {
            SetSFXVolume(0.1f);
            sfxSlider.value = 0.1f;
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicAus.volume = volume;
        PlayerPrefs.SetFloat(PrefConsts.MUSIC_VOLUME, volume);
        UpdateMusicIcon();
    }

    public void SetSFXVolume(float volume)
    {
        sfxAus.volume = volume;
        PlayerPrefs.SetFloat(PrefConsts.SFX_VOLUME, volume);
        UpdateSfxIcon();
    }
    void UpdateMusicIcon()
    {
        musicIcon.sprite = (PlayerPrefs.GetFloat(PrefConsts.MUSIC_VOLUME, 0.1f) > 0) ? musicOnSprite : musicOffSprite;
    }

    void UpdateSfxIcon()
    {
        sfxIcon.sprite = (PlayerPrefs.GetFloat(PrefConsts.SFX_VOLUME, 0.1f) > 0) ? sfxOnSprite : sfxOffSprite;
    }


    public void PlaySound(AudioClip[] clips, AudioSource aus = null)
    {
        if (!aus)
        {
            aus = sfxAus;
        }

        if (clips != null && clips.Length > 0 && aus)
        {
            var randomIdx = Random.Range(0, clips.Length);
            aus.PlayOneShot(clips[randomIdx], PlayerPrefs.GetFloat(PrefConsts.SFX_VOLUME, 0.1f));
        }
    }

    public void PlaySound(AudioClip clip, AudioSource aus = null)
    {
        if (!aus)
        {
            aus = sfxAus;
        }

        if (clip != null && aus)
        {
            aus.PlayOneShot(clip, PlayerPrefs.GetFloat(PrefConsts.SFX_VOLUME, 0.1f));
        }
    }

    public void PlayMusic(AudioClip[] musics, bool loop = true)
    {
        if (musicAus && musics != null && musics.Length > 0)
        {
            var randomIdx = Random.Range(0, musics.Length);

            musicAus.clip = musics[randomIdx];
            musicAus.loop = loop;
            musicAus.volume = PlayerPrefs.GetFloat(PrefConsts.MUSIC_VOLUME, 0.1f);
            musicAus.Play();
        }
    }

    public void PlayMusic(AudioClip music, bool canLoop)
    {
        if (musicAus && music != null)
        {
            musicAus.clip = music;
            musicAus.loop = canLoop;
            musicAus.volume = PlayerPrefs.GetFloat(PrefConsts.MUSIC_VOLUME, 0.1f);
            musicAus.Play();
        }
    }

    public void StopPlayMusic()
    {
        if (musicAus) musicAus.Stop();
    }

    public void PlayBackgroundMusic()
    {
        PlayMusic(backgroundMusics, true);
    }
}
