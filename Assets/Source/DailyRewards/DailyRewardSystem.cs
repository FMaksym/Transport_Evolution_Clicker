using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class DailyRewardSystem : MonoBehaviour
{
    public int currentStreak;
    public bool _canClaimReward;
    public bool _canGetTime = true;
    public List<int> rewardList;

    private const string TimeApiUrl = "https://worldtimeapi.org/api/timezone/Europe/Kiev";
    private const int MaxStreak = 7;
    private DateTime lastRewardTime;
    private DateTime currentServerTime;

    public delegate void StreakResetHandler();
    public delegate void ClaimRewardHandler();
    public static event StreakResetHandler StreakReseted;
    public static event ClaimRewardHandler RewardClimed;

    private void Awake()
    {
        currentStreak = PlayerPrefs.GetInt("CurrentStreak", 0);
        if (PlayerPrefs.HasKey("LastRewardTime"))
        {
            lastRewardTime = DateTime.Parse(PlayerPrefs.GetString("LastRewardTime"));
        }
        else
        {
            lastRewardTime = DateTime.Today.AddDays(-2);
        }

        GetServerTimeTask();
    }

    private async void GetServerTimeTask()
    {
        while (_canGetTime)
        {
            await GetServerTime();
            await Task.Delay(1000);
        }
    }

    private async Task GetServerTime()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(TimeApiUrl))
        {
            var asyncOperation = webRequest.SendWebRequest();

            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Ошибка при получении времени: " + webRequest.error);
            }
            else
            {
                string jsonResult = webRequest.downloadHandler.text;
                currentServerTime = ParseTimeFromJson(jsonResult);
                CheckDailyReward(currentServerTime);
            }
        }
    }

    private DateTime ParseTimeFromJson(string json)
    {
        var jsonObj = JsonUtility.FromJson<TimeApiResponse>(json);
        return DateTime.Parse(jsonObj.datetime);
    }

    private void CheckDailyReward(DateTime serverTime)
    {
        if (serverTime.Date > lastRewardTime.Date)
        {
            TimeSpan difference = serverTime.Date - lastRewardTime.Date;
            if (difference.TotalDays >= 2)
            {
                StreakReseted?.Invoke();
                currentStreak = 0;
                _canClaimReward = true;
            }
            else if (serverTime.Date == lastRewardTime.Date.AddDays(1))
            {
                _canClaimReward = true;
            }
            else
            {
                _canClaimReward = false;
            }
        }
    }

    public void CollectReward()
    {
        _canClaimReward = false;
        currentStreak++;
        RewardClimed?.Invoke();

        if (currentStreak == MaxStreak)
        {
            StreakReseted?.Invoke();
            currentStreak = 0;
        }

        lastRewardTime = currentServerTime.Date;

        PlayerPrefs.SetString("LastRewardTime", lastRewardTime.ToString());
        PlayerPrefs.SetInt("CurrentStreak", currentStreak);
        PlayerPrefs.Save();
    }

    public string TimeToNextReward()
    {
        DateTime nextRewardTime = DateTime.Today.AddDays(1).Date;
        TimeSpan remainingTime = nextRewardTime - currentServerTime;

        if (remainingTime < TimeSpan.Zero)
        {
            remainingTime = TimeSpan.Zero;
        }

        return remainingTime.ToString(@"hh\:mm\:ss");
    }

    public bool CanClaimReward()
    {
        return _canClaimReward;
    }

    private void OnDestroy()
    {
        _canGetTime = false;
    }

    [Serializable]
    private class TimeApiResponse
    {
        public string datetime;
    }
}
