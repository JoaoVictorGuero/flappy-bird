using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public static bool starEffectIsActive = false;
    public static bool timeEffectIsActive = false;
    public static bool shotEffectIsActive = false;


    [Header("Normal Player State")]
    [SerializeField] private Color normalColor;
    [SerializeField] private Sprite normalFallBird;
    [SerializeField] private Sprite normalStableBird;
    [SerializeField] private Sprite normalFlyBird;

    public static PowerUp instance;

    [HideInInspector] public PowerUpVariables currentPower;
    private float timer = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        powersOff();
    }
    public void changeState(bool isPowered = false)
    {
        if (isPowered && currentPower != null)
        {
            Player.instance.fallBird = currentPower.powerUpFallBird;
            Player.instance.stableBird = currentPower.powerUpStableBird;
            Player.instance.flyBird = currentPower.powerUpFlyBird;
            Player.instance.particleAnim.GetComponent<SpriteRenderer>().color = currentPower.powerUpColor;

        }
        else
        {
            Player.instance.fallBird = normalFallBird;
            Player.instance.stableBird = normalStableBird;
            Player.instance.flyBird = normalFlyBird;
            Player.instance.particleAnim.GetComponent<SpriteRenderer>().color = normalColor;
        }
    }
    private void powersOff()
    {
        timeEffectIsActive = false;
        shotEffectIsActive = false;
        starEffectIsActive = false;
        changeState(isPowered: false);
    }
    public void effect(PowerUpVariables power)
    {
        powersOff();
        currentPower = power;
        switch (currentPower.type)
        {
            case PowerUpVariables.effect.starEffect:
                starEffectIsActive = true;
                changeState(isPowered: true);
                break;
            case PowerUpVariables.effect.timeEffect:
                timeEffectIsActive = true;
                GameManager.instance.resetSpeed();
                timeEffectIsActive = false;
                break;
            case PowerUpVariables.effect.shotEffect:
                shotEffectIsActive = true;
                changeState(isPowered: true);
                break;
        }
    }

    private void Update()
    {
        if (starEffectIsActive && currentPower != null)
        {
            if (timer >= currentPower.time)
            {
                timer = 0;
                changeState(isPowered: false);
                starEffectIsActive = false;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        if (shotEffectIsActive && currentPower != null)
        {
            if (timer >= currentPower.time)
            {
                timer = 0;
                changeState(isPowered: false);
                shotEffectIsActive = false;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}
