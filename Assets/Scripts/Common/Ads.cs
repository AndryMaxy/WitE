using static PlayerPrefsConstants;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads
{
    private const string ANDROID_GAME_ID = "4041951";
    private const string ANDROID_VIDEO_PLACEMENT_ID = "Interstitial_Android";
    private const int COUNTDOWN = 5;

    public static void Init()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(ANDROID_GAME_ID);
            PlayerPrefs.SetInt(ADS_COUNTDOWN, COUNTDOWN);
        }
    }

    public static void ShowVideo(bool ignoreCountdown)
    {
        Show(ANDROID_VIDEO_PLACEMENT_ID, ignoreCountdown);
    }

    public static void ShowVideo()
    {
        Show(ANDROID_VIDEO_PLACEMENT_ID, false);
    }

    private static void Show(string ads, bool ignoreCountdown)
    {
        int countdouwn = PlayerPrefs.GetInt(ADS_COUNTDOWN, COUNTDOWN) - 1;
        PlayerPrefs.SetInt(ADS_COUNTDOWN, countdouwn);
        if (Advertisement.IsReady() && (ignoreCountdown || countdouwn == 0))
        { 
            Advertisement.Show(ads);
            PlayerPrefs.SetInt(ADS_COUNTDOWN, COUNTDOWN);
        }
    }
}