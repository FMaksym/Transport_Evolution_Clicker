using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadUIModel : MonoBehaviour
{
    public float LoadingTime;

    public float BackgroundScrollX;
    public float BackgroundScrollY;

    public InternetChecker InternetChecker;

    public RawImage BackgroundImage;
    public GameObject noInternetConnectionPanel;
    public Image loadingImage;
}
