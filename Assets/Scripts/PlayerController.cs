//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private KeyCode _left = KeyCode.A;
    [SerializeField] private KeyCode _right = KeyCode.D;
    [SerializeField] private KeyCode _jump = KeyCode.W;
    [SerializeField] private float _maxSpeed = 10.0f;
    [SerializeField] private float _jumpForce = 8.0f;
    [SerializeField] private float _friction = 10.0f;
    [SerializeField] private Vector2 _startingPos;
    [SerializeField] 
    private Rigidbody2D _rb = null;
    private bool _isGrounded = false;
    //Health
    private bool powerup = false;
    public Image healthbar;
    private float healthAmount = 100.0f;
    private Animator _animator = null;
    private float damage = 50.0f;
    //game over
    public CanvasGroup gameOverCanvas;
    public float belowGround = -10f;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private PlayAudio _playAudio;
    [SerializeField] private int coin = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playAudio = GetComponent<PlayAudio>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if(!_rb){
            Debug.Log("Failed to get rb");
        }
        bool isBc = PlayerPrefs.GetInt("BCMode", 0) == 1;
        if(isBc){
            damage = 0f;
        }
        _startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerMovement();
        GroundCheck();
        if(healthAmount <= 0 || transform.position.y < belowGround){
            SceneManager.LoadScene(2);
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
        if(!_isGrounded){
            _animator.SetBool("IsJumping", true);
        }
    }
    void GetPlayerMovement(){
        float moveInput = 0f;
        if(Input.GetKey(_left)){
            moveInput = -1f;
            _rb.linearVelocityX = -1 * _maxSpeed;
            _spriteRenderer.flipX = true;
        }
        if(Input.GetKey(_right)){
            moveInput = 1f;
            _rb.linearVelocityX = _maxSpeed;
            _spriteRenderer.flipX = false;
            _animator.SetBool("IsRunning", true);
        }
        else{
            _rb.linearVelocityX = Mathf.Lerp(_rb.linearVelocityX, 0.0f, Time.deltaTime * _friction);
        }
        _animator.SetFloat("Speed", Mathf.Abs(moveInput * _maxSpeed));
        if (Mathf.Abs(moveInput) > 0) 
        {
            _animator.SetBool("IsRunning", true);
        } else 
        {
        _animator.SetBool("IsRunning", false);
        }
        //jump
        if (Input.GetKeyDown(_jump) && _isGrounded)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
            _isGrounded = false; // Assume not grounded after jump
            _animator.SetBool("IsJumping", true);
            _playAudio.PlayJumpSound();
        }
    }

    void GroundCheck()
    {
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, -1 * Vector2.up);

        if(groundHit){
            float groundDist = Mathf.Abs(groundHit.point.y - transform.position.y);
            if(groundDist < (transform.localScale.y / 2) + 0.1f){
                _isGrounded = true;
                _animator.SetBool("IsJumping", false);

            }
            else{
                _isGrounded = false;
            }
        }
    }

    public void Hurt(){
        _spriteRenderer.color = Color.red;
        Debug.Log("Player Hurt");
        Debug.Log("Health: " + healthAmount);
        takeDamage(damage);
        _playAudio.PlayDamageSound();
        Invoke("FlashRed", 0.2f);
    }

    public void FlashRed(){
        _spriteRenderer.color = Color.white;

    }

    public void takeDamage(float damage){
        healthAmount -= damage;
        healthbar.fillAmount = healthAmount / 100f;
        _maxSpeed = 7f;
        Debug.Log("Health: " + healthAmount);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverCanvas.alpha = 1f;
        gameOverCanvas.interactable = true;
        gameOverCanvas.blocksRaycasts = true;  
    }
    public void ResetStart()
    {
        transform.position = _startingPos;
        _rb.linearVelocity = Vector2.zero;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddCoin(){
        coin++;
        Debug.Log("Coins: " + coin);
        _playAudio.PlayCoinSound();
        if(coin == 3){
            SceneManager.LoadScene(3);

        }
    }
    public void BirdDeath(){
        _playAudio.PlayBirdDeathSound();
    }
    public void EnemyDeath(){
        _playAudio.PlayEnemySound();
    }

}




