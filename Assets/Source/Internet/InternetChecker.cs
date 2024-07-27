using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class InternetChecker : MonoBehaviour
{
    [SerializeField] private float checkInterval = 5f; // Интервал проверки в секундах
    private bool isChecking = false;

    private void Start()
    {
        StartCoroutine(CheckInternetConnection());
    }

    private IEnumerator CheckInternetConnection()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);
            if (!isChecking)
            {
                isChecking = true;
                StartCoroutine(CheckConnection());
            }
        }
    }

    private IEnumerator CheckConnection()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("http://google.com"))
        {
            yield return webRequest;
            isChecking = false;
            if (webRequest.error != null)
            {
                HandleNoInternet();
            }
        }
    }

    private void HandleNoInternet()
    {
        // Приостановка игры
        Time.timeScale = 0;

        // Переход на сцену загрузки
        SceneManager.LoadScene("LoadingScene"); // Имя сцены загрузки
    }
}
