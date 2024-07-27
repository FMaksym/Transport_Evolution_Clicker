using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardModel : MonoBehaviour
{
    public CurrencyManager CurrencyManager;
    public DailyRewardSystem DailyRewardSystem;

    //public List<TMP_Text> rewardTextList;

    public GameObject DailyRewardPanel;
    public GameObject ClimeRewardButton;
    public GameObject UnactiveClimeRewardButton;
    public TMP_Text TimerText;

    public List<GameObject> RewardCheakmarkList;
}
