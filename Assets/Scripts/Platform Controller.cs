using UnityEngine;

public class PlatformController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float _horGoal = 0.0f;
    [SerializeField] private float _vertGoal = 0.0f;
    [SerializeField] private float _moveTime = 1.0f;
    private Vector2 _startPos;
    private Vector2 _goalPos;
    private Vector2 _prevPos;

    private Rigidbody2D _playerRididbody;
    void Start()
    {
        _startPos = transform.position;
        _goalPos = new Vector2(_startPos.x + _horGoal, _startPos.y + _vertGoal);
        _prevPos = _startPos;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.PingPong(Time.time / _moveTime, 1.0f);

        //Interpolate new pos
        Vector2 curPos = Vector2.Lerp(_startPos, _goalPos, t);
        transform.position = curPos;

        Vector2 platformVelcoity = (curPos - _prevPos) / Time.deltaTime;
        if(_playerRididbody != null)
        {
            Vector2 playerVel = _playerRididbody.linearVelocity;
            playerVel.x = platformVelcoity.x;
            _playerRididbody.linearVelocity = playerVel;
        }
        _prevPos = curPos;
    }

    //Stick Player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _playerRididbody = collision.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    //Unstick Player
    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.GetComponent<Rigidbody2D>() == _playerRididbody)
        {
            if(collision.collider.GetComponent<Rigidbody2D>() == _playerRididbody)
            {
                _playerRididbody = null;
            }
        }
    }
}
