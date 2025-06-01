using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Player : MonoBehaviour
{
    [Header("Basic")]
    public Sprite fallBird;
    public Sprite stableBird;
    public Sprite flyBird;
    [Header("Speeds")]
    [SerializeField] private float yVelocity = 2;
    [SerializeField] private float upRotation = 90f;
    [SerializeField] private float downRotation = -90f;
    [SerializeField] private float smoothRotation = 5f;
    [Header("Sound")]
    [SerializeField] private AudioClip clickSound;
    [SerializeField]private AudioClip hitSound;
    [SerializeField] private AudioClip dieSound;
    [Header("Advanced")]
    public GameObject particleAnim;

    public static Player instance;
    public static float posicao = 0f;
    public static bool isMoving = false;

    private Rigidbody2D _rigidbody2D;
    private AudioSource _audioCom;
    private SpriteRenderer _spriteRenderer;

    private bool _dead = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {        
        posicao = 0f;

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0f;
        _rigidbody2D.velocity = new Vector2(0, 0);

        _audioCom = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = stableBird;
    }

    private void Update()
    {

        if (!isMoving && !_dead && Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.instance.startGame(isStarting: true);
            _rigidbody2D.gravityScale = 0f;
            isMoving = true;
        }
        
        if (isMoving && !_dead)
        {
            _rigidbody2D.gravityScale = 0.25f;
            posicao += Time.deltaTime;
            Move();
        }
        
        if (_dead && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (PowerUp.starEffectIsActive)
            {
                collision.transform.parent.gameObject.SetActive(false);
                GameManager.instance.effectParticle.transform.position = transform.position;
                GameManager.instance.effectParticle.SetActive(true);
                return;
            }
        }
        if (_dead) _audioCom.PlayOneShot(dieSound);

        death();
    }
    
    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody2D.velocity = Vector2.up * yVelocity;
            _audioCom.PlayOneShot(clickSound);
            particleAnim.transform.position = new Vector2(transform.position.x - 0.1f, transform.position.y + 0.075f);
            particleAnim.GetComponent<Animator>().SetTrigger("fly");
        }

        if(_rigidbody2D.velocity.y == 0)
            _spriteRenderer.sprite = stableBird;
        else
            _spriteRenderer.sprite = _rigidbody2D.velocity.y > 0 ? flyBird : fallBird;

        float angle = Mathf.Lerp(downRotation, upRotation, (_rigidbody2D.velocity.y + 5f) / 10f);
        angle = Mathf.Clamp(angle, downRotation, upRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * smoothRotation);
    }

    private void death()
    {
        _rigidbody2D.gravityScale = 1;
        isMoving = false;
        _dead = true;
        GameManager.instance.gameOver();
        _audioCom.PlayOneShot(hitSound);

    }
}