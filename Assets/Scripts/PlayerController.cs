using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private AudioSource playerAudio;
    public AudioClip scoreSound;
    public AudioClip crushSound;
    public float jumpForce;
    public float gravityModifier;
    public bool isLowEnough;
    public bool gameOver = false;
    public int score;
    public TMP_Text scoreText;
    private float debounceTime = 1.0f; // ����� �������� � ��������
    private float lastTriggerTime = 0f;
    public GameObject gameOverText;
    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Time.timeScale = 0f;
            gameOverText.SetActive(true);
            playerAudio.PlayOneShot(crushSound, 1.0f);
            Debug.Log("Game Over!");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Score"))
        {
            // ���������, ������ �� ���������� ������� � ���������� ��������
            if (Time.time - lastTriggerTime >= debounceTime)
            {
                score += 1;
                if (scoreManager != null)
                {
                    scoreManager.UpdateScore(scoreManager.score);
                }
                else
                {
                    Debug.LogError("ScoreManager not found!");
                }

                scoreManager.UpdateScore(scoreManager.score);
                scoreText.text = score.ToString();
                playerAudio.PlayOneShot(scoreSound, 1.0f);
                Debug.Log("Score: " + score);

                // ��������� ����� ���������� ��������
                lastTriggerTime = Time.time;
            }
        }
    }
}
