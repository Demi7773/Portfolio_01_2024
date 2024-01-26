using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private const string MUSIC_VOLUME_KEY = "musicVolume";
    private const string SFX_VOLUME_KEY = "sfxVolume";

    [Header("UI Sliders")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Audio Sources")]
    [SerializeField] List<AudioSource> musicAudioSources = new List<AudioSource>();
    [SerializeField] AudioSource sfxAudioSource;

    [Header("Music")] // main menu pocinje svirat u startu i gasi se na svakom gumbu za level. u toj metodi prvo zaustaviti audiosource //OBAVLJENO
                      // mjuze za levele namjestit na level buttone(s kratkim delayem od sekundu u korutini). u svakoj toj metodi pripojiti i seaAndSeagulls + shipCreakTheme//POLUOBAVLJENO
                      // u tim metodama prvo zaustaviti audiosource //OBAVLJENO
                      // mozda samo na popup victory i defeat screena prvo stopat music audiosource, pa playat te teme,te na njihove sve buttone ih zaustavit i pocet playat main menu.//OBAVLJENO
                      //shop muzika se pali na shop button, a gasi na gumb return u shopu - tad takodjer pocinje main menu theme. //OBAVLJENO
                      // boss muzika se pali na gumb od lvl 9. // OBAVLJENO


    [SerializeField] private AudioClip mainMenuTheme;
    [SerializeField] private AudioClip level1Theme;
    [SerializeField] private AudioClip level2Theme;
    [SerializeField] private AudioClip level3Theme;
    [SerializeField] private AudioClip level4Theme;
    [SerializeField] private AudioClip level5Theme;
    [SerializeField] private AudioClip level6Theme;
    [SerializeField] private AudioClip seaAndSeagullsTheme;
    [SerializeField] private AudioClip shipCreakTheme;
    [SerializeField] private AudioClip shopTheme;
    [SerializeField] private AudioClip bossLevelTheme;
    [SerializeField] private AudioClip victoryTheme;
    [SerializeField] private AudioClip defeatTheme;

    [Header("UI Sounds")]
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioClip buyItemSound;
    [SerializeField] private AudioClip panelPopUpSound;
    [SerializeField] private AudioClip rewardSound;
    [SerializeField] private AudioClip ropeSound;
    [SerializeField] private AudioClip shipRepairSound;
    [SerializeField] private AudioClip shopEnterSound;
    [SerializeField] private List<AudioClip> barunGreetList = new List<AudioClip>();

    [Header("InGame Sounds")]
    [SerializeField] private List<AudioClip> cannonSoundList = new List<AudioClip>();
    [SerializeField] private List<AudioClip> krakenAttackSoundList = new List<AudioClip>();
    [SerializeField] private AudioClip krakenLevelEnterSound;
    [SerializeField] private AudioClip krakenStageOneSound;
    [SerializeField] private AudioClip krakenStageTwoSound;
    [SerializeField] private AudioClip krakenStageThreeSound;
    [SerializeField] private List<AudioClip> shipCollideSoundList = new List<AudioClip>();
    [SerializeField] private List<AudioClip> shipDamagedSoundList = new List<AudioClip>();

    [Header("Settings")]
    [SerializeField] private float _themesDelay = 1f;

    private Tween _musicAudioSourceTween = default;


    private void OnEnable()
    {
        AudioEvents.PlayVictoryThemeEvent += OnPlayVictoryThemeEvent;
        AudioEvents.PlayDefeatThemeEvent += OnPlayDefeatThemeEvent;
        AudioEvents.PlayPanelPopupSoundEvent += OnPlayPanelPopupSound;
        AudioEvents.PlayRopeSoundEvent += OnPlayRopeSound;
        AudioEvents.PlayShopEnterSoundEvent += OnPlayShopEnterSoundEvent;
        AudioEvents.PlayCannonSoundsEvent += OnPlayCannonSoundsEvent;
        AudioEvents.PlayKrakenAttackSoundsEvent += OnPlayKrakenAttackSoundsEvent;
        AudioEvents.PlayKrakenStageOneSoundEvent += OnPlayKrakenStageOneSoundEvent;
        AudioEvents.PlayKrakenStageTwoSoundEvent += OnPlayKrakenStageTwoSoundEvent;
        AudioEvents.PlayKrakenStageThreeSoundEvent += OnPlayKrakenStageThreeSoundEvent;
        AudioEvents.PlayShipCollideSoundsEvent += OnPlayShipCollideSoundsEvent;
        AudioEvents.PlayShipDamagedSoundsEvent += OnPlayShipDamagedSoundsEvent;
        AudioEvents.PlayLevel1ThemeEvent += OnPlayLevel1Theme;
        AudioEvents.PlayLevel2ThemeEvent += OnPlayLevel2Theme;
        AudioEvents.PlayLevel3ThemeEvent += OnPlayLevel3Theme;
        AudioEvents.PlayShopThemeEvent += PlayShopTheme;
        AudioEvents.PlayBossLevelThemeEvent += PlayShopTheme;

    }

    private void OnDisable()
    {
        AudioEvents.PlayVictoryThemeEvent -= OnPlayVictoryThemeEvent;
        AudioEvents.PlayDefeatThemeEvent -= OnPlayDefeatThemeEvent;
        AudioEvents.PlayPanelPopupSoundEvent -= OnPlayPanelPopupSound;
        AudioEvents.PlayRopeSoundEvent -= OnPlayRopeSound;
        AudioEvents.PlayShopEnterSoundEvent -= OnPlayShopEnterSoundEvent;
        AudioEvents.PlayCannonSoundsEvent -= OnPlayCannonSoundsEvent;
        AudioEvents.PlayKrakenAttackSoundsEvent -= OnPlayKrakenAttackSoundsEvent;
        AudioEvents.PlayKrakenStageOneSoundEvent -= OnPlayKrakenStageOneSoundEvent;
        AudioEvents.PlayKrakenStageTwoSoundEvent -= OnPlayKrakenStageTwoSoundEvent;
        AudioEvents.PlayKrakenStageThreeSoundEvent -= OnPlayKrakenStageThreeSoundEvent;
        AudioEvents.PlayShipCollideSoundsEvent -= OnPlayShipCollideSoundsEvent;
        AudioEvents.PlayShipDamagedSoundsEvent -= OnPlayBossLevelTheme;
        AudioEvents.PlayLevel1ThemeEvent -= OnPlayLevel1Theme;
        AudioEvents.PlayLevel2ThemeEvent -= OnPlayLevel2Theme;
        AudioEvents.PlayLevel3ThemeEvent -= OnPlayLevel3Theme;
        AudioEvents.PlayShopThemeEvent -= PlayShopTheme;
        AudioEvents.PlayBossLevelThemeEvent -= PlayShopTheme;
    }




    private void Awake()
    {
        SetMusicVolume(PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f)); //default value 1f ce postaviti prefs key (ako jos ne postoji) na default value aka 1f u ovom slucaju. To znaci da pri prvom pokretnju igre sprema 1f na glasnocu
        SetSFXVolume(PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f));
    }

    private void Start()
    {
        PlayMainMenuTheme();
    }

    public void SetMusicVolume(float volume)
    {
        foreach (var audioSource in musicAudioSources)
        {
            audioSource.volume = volume;
        }
        musicSlider.value = volume;

        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
    }
    public void SetSFXVolume(float volume)
    {
        sfxAudioSource.volume = volume;
        sfxSlider.value = volume;
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
    }

    #region MusicThemes  
    public void PlayMainMenuTheme()
    {
        PlayMusicTheme(mainMenuTheme);
    }

    public void OnPlayLevel1Theme()
    {
        PlayMusicTheme(level1Theme, hasSeaWavesAndShit: true);
    }

    public void OnPlayLevel2Theme()
    {
        PlayMusicTheme(level2Theme, hasSeaWavesAndShit: true);
    }

    public void OnPlayLevel3Theme()
    {
        PlayMusicTheme(level3Theme, hasSeaWavesAndShit: true);
    }

    public void PlayLevel4Theme()
    {
        PlayMusicTheme(level4Theme, hasSeaWavesAndShit: true);
    }

    public void PlayLevel5Theme()
    {
        PlayMusicTheme(level5Theme, hasSeaWavesAndShit: true);
    }

    public void PlayLevel6Theme()
    {
        PlayMusicTheme(level6Theme, hasSeaWavesAndShit: true);
    }

    public void PlayShopTheme()
    {
        PlayMusicTheme(shopTheme);
    }

    public void OnPlayBossLevelTheme()
    {
        PlayMusicTheme(bossLevelTheme, hasSeaWavesAndShit: true);
    }

    private void OnPlayVictoryThemeEvent()
    {
        PlayMusicTheme(victoryTheme);
    }

    private void OnPlayDefeatThemeEvent()
    {
        PlayMusicTheme(defeatTheme);
    }

    private void StopAllMusicAudioSources()
    {
        foreach (var audioSource in musicAudioSources)
        {
            audioSource.Stop();
        }
    }

    private void PlayThemes(AudioClip firstClip, AudioClip secondClip = null, AudioClip thirdClip = null) //ova metoda se ne koristi, al nek ostane ak ce slucajno zatrebat
    {
        StopAllMusicAudioSources();

        _musicAudioSourceTween?.Kill();
        _musicAudioSourceTween = DOVirtual.DelayedCall(_themesDelay, () =>
        {
            musicAudioSources[0].clip = firstClip;
            musicAudioSources[0].Play();

            if (secondClip != null)
            {
                musicAudioSources[1].clip = secondClip;
                musicAudioSources[1].Play();
            }

            if (thirdClip != null)
            {
                musicAudioSources[2].clip = thirdClip;
                musicAudioSources[2].Play();
            }
        });
    }

    private void PlayMusicTheme(AudioClip firstCLip, bool hasSeaWavesAndShit = false)
    {
        StopAllMusicAudioSources();

        _musicAudioSourceTween?.Kill();
        _musicAudioSourceTween = DOVirtual.DelayedCall(_themesDelay, () =>
        {
            musicAudioSources[0].clip = firstCLip;
            musicAudioSources[0].Play();

            if (hasSeaWavesAndShit)
            {
                musicAudioSources[1].clip = seaAndSeagullsTheme;
                musicAudioSources[1].Play();

                musicAudioSources[2].clip = shipCreakTheme;
                musicAudioSources[2].Play();
            }
        });
    }
    #endregion

    #region UISounds
    public void PlayButtonSound()
    {
        sfxAudioSource.PlayOneShot(buttonSound);
        //Debug.Log("Button Sound Played");
    }

    public void PlayBuyItemSound()
    {
        sfxAudioSource.PlayOneShot(buyItemSound);
    }

    public void OnPlayPanelPopupSound()
    {
        sfxAudioSource.PlayOneShot(panelPopUpSound);
    }

    public void PlayRewardSound()
    {
        sfxAudioSource.PlayOneShot(rewardSound);
    }

    public void OnPlayRopeSound()
    {
        sfxAudioSource.PlayOneShot(ropeSound);
    }

    public void PlayShipRepairSound()
    {
        sfxAudioSource.PlayOneShot(shipRepairSound);
    }

    public void OnPlayShopEnterSoundEvent()
    {
        sfxAudioSource.PlayOneShot(shopEnterSound);

        int randomIndex = Random.Range(0, barunGreetList.Count);
        AudioClip randomClip = barunGreetList[randomIndex];
        sfxAudioSource.PlayOneShot(randomClip);
    }
    #endregion

    #region INGAMESounds
    public void OnPlayCannonSoundsEvent()
    {
        int randomIndex = Random.Range(0, cannonSoundList.Count);
        AudioClip randomClip = cannonSoundList[randomIndex];
        sfxAudioSource.PlayOneShot(randomClip);
    }

    public void OnPlayKrakenAttackSoundsEvent()
    {
        int randomIndex = Random.Range(0, krakenAttackSoundList.Count);
        AudioClip randomClip = krakenAttackSoundList[randomIndex];
        sfxAudioSource.PlayOneShot(randomClip);
    }

    public void PlayKrakenLevelEnterSound()
    {
        sfxAudioSource.PlayOneShot(krakenLevelEnterSound);
    }

    private void OnPlayKrakenStageOneSoundEvent()
    {
        sfxAudioSource.PlayOneShot(krakenStageOneSound);
    }

    private void OnPlayKrakenStageTwoSoundEvent()
    {
        sfxAudioSource.PlayOneShot(krakenStageTwoSound);
    }

    private void OnPlayKrakenStageThreeSoundEvent()
    {
        sfxAudioSource.PlayOneShot(krakenStageThreeSound);
    }

    private void OnPlayShipCollideSoundsEvent()
    {
        int randomIndex = Random.Range(0, shipCollideSoundList.Count);
        AudioClip randomClip = shipCollideSoundList[randomIndex];
        sfxAudioSource.PlayOneShot(randomClip);
    }

    private void OnPlayShipDamagedSoundsEvent()
    {
        int randomIndex = Random.Range(0, shipDamagedSoundList.Count);
        AudioClip randomClip = shipDamagedSoundList[randomIndex];
        sfxAudioSource.PlayOneShot(randomClip);
    }
    #endregion


}
