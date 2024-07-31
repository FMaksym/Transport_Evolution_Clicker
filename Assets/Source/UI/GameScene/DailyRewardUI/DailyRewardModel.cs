using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyRewardModel : MonoBehaviour
{
    public CurrencyManager CurrencyManager;
    public DailyRewardSystem DailyRewardSystem;
    public GameObject DailyRewardPanel;
    public GameObject ClimeRewardButton;
    public GameObject UnactiveClimeRewardButton;
    public TMP_Text TimerText;
    public List<GameObject> RewardCheakmarkList;
}
