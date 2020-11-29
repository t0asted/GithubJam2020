using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Data/Item/Raw Material")]
public class SO_Raw : ScriptableObject
{
    public Enum_Items ResourceName;
    public Image Sprite;
}