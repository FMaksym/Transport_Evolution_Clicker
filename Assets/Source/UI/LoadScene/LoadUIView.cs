using DG.Tweening;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadUIView : MonoBehaviour
{
    public void Initialize(LoadUIModel loadUIModel)
    {
        if (loadUIModel.InternetChecker == null) { loadUIModel.InternetChecker = FindObjectOfType<InternetChecker>(); }
    }

    public async Task LoadGameASync(LoadUIModel loadUIModel, string sceneLoadName)
    {
        RotateLoadingImage(loadUIModel);

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneLoadName);
        loadOperation.allowSceneActivation = false;

        Debug.Log("AsyncLoad Start");


        while (!loadOperation.isDone)
        {
            if (loadUIModel.InternetChecker.isInternetConnection)
            {
                if (loadOperation.progress >= 0.9f)
                {
                    await WaitAndLoadScene(loadUIModel);
                    loadUIModel.loadingImage.DOKill();
                    loadOperation.allowSceneActivation = true;
                }
            }

            await Task.Yield();
        }
    }

    private async Task WaitAndLoadScene(LoadUIModel loadUIModel)
    {
        await Task.Delay((int) loadUIModel.LoadingTime * 1000);
    }

    public void ScrollBackground(LoadUIModel loadUIModel)
    {
        if (loadUIModel.BackgroundImage.uvRect.x < 1 && loadUIModel.BackgroundImage.uvRect.y < 1)
        {
            loadUIModel.BackgroundImage.uvRect = new Rect(loadUIModel.BackgroundImage.uvRect.position +
            new Vector2(loadUIModel.BackgroundScrollX, loadUIModel.BackgroundScrollY) * Time.fixedDeltaTime,
            loadUIModel.BackgroundImage.uvRect.size);
        }
        else
        {
            loadUIModel.BackgroundImage.uvRect = new Rect(Vector2.zero, loadUIModel.BackgroundImage.uvRect.size);
        }
    }

    public void ShowLoseInternetConnectionMessage(LoadUIModel loadUIModel)
    {
        loadUIModel.noInternetConnectionPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private void RotateLoadingImage(LoadUIModel loadUIModel)
    {
        // Вращать изображение по оси Z на 360 градусов за 1 секунду, и повторять это бесконечно
        loadUIModel.loadingImage.rectTransform.DORotate(new Vector3(0, 0, 360), 2f, RotateMode.FastBeyond360)
                                  .SetEase(Ease.Linear)
                                  .SetLoops(-1, LoopType.Restart);
    }
}
