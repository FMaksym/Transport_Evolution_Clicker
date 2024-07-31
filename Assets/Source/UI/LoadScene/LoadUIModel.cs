using UnityEngine;
using UnityEngine.UI;

public class LoadUIModel : MonoBehaviour
{
    [Space(2), Header("Game Scene Loading Time")]
    public float LoadingTime;

    [Space(2), Header("Background Scroll Parametres")]
    public float BackgroundScrollX;
    public float BackgroundScrollY;

    [Space(2), Header("Internet Check Manager")]
    public InternetChecker InternetChecker;

    [Space(2), Header("UI Elements")]
    public RawImage BackgroundImage;
    public GameObject noInternetConnectionPanel;
    public Image loadingImage;
}
