using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugManager;

public class LoadUIViewModel : MonoBehaviour
{
    public LoadUIModel loadUIModel;
    public LoadUIView loadUIView;

    private void OnEnable()
    {
        InternetChecker.InternetConnectionLose += NoInternetConnectionMessage;
    }

    private void Awake()
    {
        loadUIView.Initialize(loadUIModel);
    }

    private async void Start()
    {
        
        loadUIModel.InternetChecker.StartCheckingInternet();
        await loadUIView.LoadGameASync(loadUIModel, "GameScene");
    }

    private void FixedUpdate()
    {
        loadUIView.ScrollBackground(loadUIModel);
    }

    private void NoInternetConnectionMessage()
    {
        loadUIView.ShowLoseInternetConnectionMessage(loadUIModel);
    }

    public void OnClickRestartGame()
    {
        //loadUIModel.InternetChecker.StartCheckingInternet();
        loadUIView.RestartGame();
    }

    private void OnDisable()
    {
        InternetChecker.InternetConnectionLose -= NoInternetConnectionMessage;
    }
}
