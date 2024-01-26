using System;

public static class AudioEvents
{
    //***mjuza***
    public static Action PlayDefeatThemeEvent; // kopiraj: AudioEvents.PlayDefeatThemeEvent?.Invoke(); ==> tamo gdje se popupa defeat panel
    public static Action PlayVictoryThemeEvent; // kopiraj: AudioEvents.PlayVictoryThemeEvent?.Invoke(); ==> tamo gdje se popupa victory panel
    public static Action PlayLevel1ThemeEvent; // kopiraj: AudioEvents.PlayLevel1ThemeEvent?.Invoke(); ==> u level 1
    public static Action PlayLevel2ThemeEvent; // kopiraj: AudioEvents.PlayLevel2ThemeEvent?.Invoke(); ==> u level 2
    public static Action PlayLevel3ThemeEvent; // kopiraj: AudioEvents.PlayLevel3ThemeEvent?.Invoke(); ==> u level 3
    public static Action PlayShopThemeEvent; // kopiraj: AudioEvents.PlayLevel3ThemeEvent?.Invoke(); ==> u shopove
    public static Action PlayBossLevelThemeEvent; // kopiraj: AudioEvents.PlayLevel3ThemeEvent?.Invoke(); ==> u kraken level

    //***UI sounds***
    public static Action PlayPanelPopupSoundEvent; // kopiraj: AudioEvents.PlayPanelPopupSoundEvent?.Invoke(); ==> tamo gdje se popupaju How to play, victory i defeat paneli
    public static Action PlayShopEnterSoundEvent; // kopiraj: AudioEvents.PlayShopEnterSoundEvent?.Invoke(); ==> tamo gdje se popupa shop panel
    public static Action PlayRopeSoundEvent; // JA MORAM OVO DOK NAPRAVIM ANIMACIJU: AudioEvents.PlayRopeSoundEvent?.Invoke(); ==> tamo gdje se popupaju How to play, victory i defeat paneli
    //***INGAME sounds***
    public static Action PlayCannonSoundsEvent; // kopiraj: AudioEvents.PlayCannonSoundsEvent?.Invoke(); ==> svugdje gdje se puca s topovima
    public static Action PlayKrakenAttackSoundsEvent; // kopiraj: AudioEvents.PlayKrakenAttackSoundsEvent?.Invoke(); ==> na svaki kraken attack (u sve tri faze)
    public static Action PlayKrakenStageOneSoundEvent; // kopiraj: AudioEvents.PlayKrakenStageOneSoundEvent?.Invoke(); ==> na pocetak krakenove PRVE faze
    public static Action PlayKrakenStageTwoSoundEvent; // kopiraj: AudioEvents.PlayKrakenStageTwoSoundEvent?.Invoke(); ==> na pocetak krakenove DRUGE faze
    public static Action PlayKrakenStageThreeSoundEvent; // kopiraj: AudioEvents.PlayKrakenStageThreeSoundEvent?.Invoke(); ==> na pocetak krakenove TRECE faze
    public static Action PlayShipCollideSoundsEvent; // kopiraj: AudioEvents.PlayShipCollideSoundsEvent?.Invoke(); ==> kad se enemy brod zaleti u nas 
    public static Action PlayShipDamagedSoundsEvent; // kopiraj: AudioEvents.PlayShiDamagedSoundsEvent?.Invoke(); ==> Svugdje gdje brodovi primaju damage (i njihovi i nas) 


}
