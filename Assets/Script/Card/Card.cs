using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Cards/Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public Sprite cardImage;
    public string description;
}
