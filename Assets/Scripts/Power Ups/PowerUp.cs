using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public static bool starEffectIsActive = false;
    public static bool timeEffectIsActive = false;
    public static bool shotEffectIsActive = false;
    public static bool AntGEffectIsActive = false;


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
            Physics2D.gravity = new Vector2(0, GameManager.NormalGravity);
            Player.instance.fallBird = normalFallBird;
            Player.instance.stableBird = normalStableBird;
            Player.instance.flyBird = normalFlyBird;
            Player.instance.particleAnim.GetComponent<SpriteRenderer>().color = normalColor;
        }
    }
    // deixa o player com nenhum power up
    private void powersOff()
    {
        timeEffectIsActive = false;
        shotEffectIsActive = false;
        starEffectIsActive = false;
        AntGEffectIsActive = false;
        Physics2D.gravity = new Vector2(0, GameManager.NormalGravity);
        changeState(isPowered: false);
    }
    // Ativa os efeitos dos powers up conforme o uso 
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
            case PowerUpVariables.effect.AntGEffect:
                AntGEffectIsActive = true;
                changeState(isPowered: true);
                Physics2D.gravity = new Vector2(0, -GameManager.NormalGravity);
                break;
        }
    }
    private void powerTimeController(ref bool power, ref float timer)
    {
        if (power && currentPower != null)
        {
            if (timer >= currentPower.time)
            {
                timer = 0;
                changeState(isPowered: false);
                power = false;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    private void Update()
    {
        powerTimeController(ref starEffectIsActive, ref timer);
        powerTimeController(ref shotEffectIsActive, ref timer);
        powerTimeController(ref AntGEffectIsActive, ref timer);
    }
}
