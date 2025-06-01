using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Power Up", menuName = "New Power Up")]
public class PowerUpVariables : ScriptableObject
{
    public enum effect { starEffect, timeEffect, shotEffect}

    [Header("Basic")]
    public effect type;
    public float time;
    [Header("Customization")]
    public AudioClip sound;
    public Color powerUpColor;
    public Sprite powerUpFallBird;
    public Sprite powerUpStableBird;
    public Sprite powerUpFlyBird;
}
