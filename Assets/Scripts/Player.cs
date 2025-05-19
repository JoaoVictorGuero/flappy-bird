using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Player : MonoBehaviour
{
    public static float posicao = 0f;
    public static bool isMoving = false;
    private bool _dead = false;
    private Rigidbody2D _rigidbody2D;
    public SpriteRenderer message;
    public SpriteRenderer gameover;
    private static int _score = 0;
    public static Player instance;
    public TMP_Text scoreText;
    private void Start()
    {
        message.enabled = true;
        gameover.enabled = false;
        
        posicao = 0f;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (!isMoving && !_dead && Input.GetKeyDown(KeyCode.Space))
        {
            message.enabled = false;
            _rigidbody2D.gravityScale = 0f;
            isMoving = true;
        }
        
        if (isMoving && !_dead)
        {
            _rigidbody2D.gravityScale = 0.25f;
            posicao += Time.deltaTime;
            Move();
        }
        else
        { 
            _rigidbody2D.gravityScale = 0f;
            _rigidbody2D.velocity = new Vector2(0f, 0f);
        }
        
        if (_dead && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isMoving = false;
        _dead = true;
        gameover.enabled = true;
        _score = 0;
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        _score += amount;
        scoreText.text = _score.ToString();
    }
    
    private void Move()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody2D.velocity = new Vector2(0f, 1f);
        }
    }
}