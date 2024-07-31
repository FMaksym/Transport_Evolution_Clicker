using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class InternetChecker : MonoBehaviour
{
    public bool isInternetConnection = true;

    [SerializeField] private float checkInterval = 10f;
    [SerializeField] private bool isChecking = false;
    [SerializeField] private bool canCheckInternet = true;

    public delegate void InternetLoseHandler();
    public static event InternetLoseHandler InternetConnectionLose;

    public void StartCheckingInternet()
    {
        canCheckInternet = true;
        _ = StartCheckingInternetAsync();
    }

    public bool IsCanCheckInternet()
    {
        return canCheckInternet;
    }

    private async Task StartCheckingInternetAsync()
    {
        while (canCheckInternet)
        {
            if (!isChecking)
            {
                isChecking = true;
                await CheckConnectionAsync();
            }
            await Task.Delay((int)(checkInterval * 1000));
        }
    }

    private async Task CheckConnectionAsync()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("https://google.com"))
        {
            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                isInternetConnection = true;
            }
            else
            {
                HandleNoInternet();
            }

            isChecking = false;
        }
    }

    private void HandleNoInternet()
    {
        canCheckInternet = false;
        isInternetConnection = false;
        InternetConnectionLose?.Invoke();
    }

    private void OnDestroy()
    {
        canCheckInternet = false;
    }
}
