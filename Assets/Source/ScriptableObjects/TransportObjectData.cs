using UnityEngine;

[CreateAssetMenu(fileName = "NewTransport", menuName = "Transport Object Data", order = 1)]
public class TransportObjectData : ScriptableObject
{
    public string Name;
    public int MaxHealth;
    public Sprite Image;
}
