using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class InternetChecker : MonoBehaviour
{
    public bool isInternetConnection = true;

    [SerializeField] private float checkInterval = 10f;
    [SerializeField] private bool isChecking = false;
    [SerializeField] private bool canCheckInternet = true;

    public static InternetChecker Instance { get; private set; }

    public delegate void InternetLoseHandler();
    public static event InternetLoseHandler InternetConnectionLose;

    private Task internetCheckTask;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private async void Start()
    {
        //canCheckInternet = true;
        //await StartCheckingInternetAsync();
    }

    public void StartCheckingInternet()
    {
        canCheckInternet = true;
        _ = StartCheckingInternetAsync();
    }

    //private void Update()
    //{
    //    if (canCheckInternet)
    //    {
    //        TryCheckInternet();
    //    }
    //}

    private async Task StartCheckingInternetAsync()
    {
        while (canCheckInternet)
        {
            if (!isChecking)
            {
                isChecking = true;
                await CheckConnectionAsync();
            }
            await Task.Delay((int)(checkInterval * 1000)); // Wait before the next check
        }
    }

    //private async void TryCheckInternet()
    //{
    //    if (!isChecking)
    //    {
    //        isChecking = true;
    //        await CheckConnectionAsync();
    //    }
    //    await Task.Delay((int)(checkInterval * 1000));
    //}

    private async Task CheckConnectionAsync()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("https://google.com"))
        {
            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                isInternetConnection = true;
                Debug.Log("Internet connection is working correctly");
            }
            else
            {
                HandleNoInternet();
                Debug.Log("No Internet Connection: " + webRequest.error);
            }

            isChecking = false;
        }
    }

    public bool IsCanCheckInternet()
    {
        return canCheckInternet;
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
