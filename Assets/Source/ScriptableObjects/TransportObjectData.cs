using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewTransport", menuName = "Transport Object Data", order = 1)]
public class TransportObjectData : ScriptableObject
{
    public string Name;
    public int MaxHealth;
    public Sprite Image;
}
