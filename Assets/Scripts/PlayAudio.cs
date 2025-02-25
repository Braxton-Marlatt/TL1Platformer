using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _damageSound;
    [SerializeField] private AudioSource _jumpSound;
    [SerializeField] private AudioSource _coinSound;
    [SerializeField] private AudioSource _birddeathSound;
    [SerializeField] private AudioSource _enemySound;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayDamageSound()
    {
        if (_damageSound != null)
        {
            _damageSound.Play();
        }
    }

    public void PlayJumpSound()
    {
        if (_jumpSound != null)
        {
            _jumpSound.Play();
        }
    }
    public void PlayCoinSound()
    {
        if (_coinSound != null)
        {
            _coinSound.Play();
        }
    }
    public void PlayBirdDeathSound()
    {
        if (_birddeathSound != null)
        {
            _birddeathSound.Play();
        }
    }  
    public void PlayEnemySound()
    {
        if (_enemySound != null)
        {
            _enemySound.Play();
        }
    } 

}
