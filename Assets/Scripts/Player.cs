using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Player : MonoBehaviour
{
    // Inspector private
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
    [Header("Shot")]
    [SerializeField] private Transform shotSpawnerTrans;
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private float shotSpeed;
    [Header("Advanced")]
    public GameObject particleAnim;

    // Static 

    public static Player instance;
    public static float position = 0f;
    public static bool isMoving = false;

    // Private
    private Rigidbody2D _rigidbody2D;
    private AudioSource _audioCom;
    private SpriteRenderer _spriteRenderer;

    private bool _dead = false;
    //verifica se o player foi instanciado
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {        
        position = 0f;

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
            if (!PowerUp.timeEffectIsActive) position += Time.deltaTime;
            if(PowerUp.shotEffectIsActive) Shot();
            Move();
        }
        
        if (_dead && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }  
    // lida com a colisão do player com obstaculos 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (PowerUp.starEffectIsActive)
            {
                collision.transform.parent.gameObject.GetComponent<PipeSystem>().kill();
                return;
            }
        }
        if (_dead) _audioCom.PlayOneShot(dieSound,0.5f);
        else Death();
    }

    /**
    * @brief Lida com a movimentação do jogador.
    * 
    * Impulsiona-o verticalmente, calcula ângulo de inclinação,
    * ativa efeitos sonoros e visuais.
    */
    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody2D.velocity =PowerUp.AntGEffectIsActive ? Vector2.down * yVelocity: Vector2.up * yVelocity;
            _audioCom.PlayOneShot(clickSound , 0.5f);
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

    /**
    * @brief Responsável por spawnar e configurar uma bala.
    * 
    * 'Instancia' por meio da piscina de objetos (Object pool) um
    * tiro, posicionando-o e configurando suas físicas para se 
    * comportar como tal.
    */
    private void Shot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject shot = ObjectPooler_manager.Instance.SpawnFromPool("shot", shotSpawnerTrans.position, shotSpawnerTrans.rotation);
            shot.GetComponent<Rigidbody2D>().velocity = Vector2.right * shotSpeed;
            _audioCom.PlayOneShot(shotSound, 0.5f);
        }
    }

    // Lida com procedimento de morte do Player.
    private void Death()
    {
        _rigidbody2D.gravityScale = 1;
        isMoving = false;
        _dead = true;
        GameManager.instance.gameOver();
        _audioCom.PlayOneShot(hitSound, 0.5f);

    }
}