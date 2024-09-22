using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    public float jumpForce;
    public float gravityModifier;
    public bool isLowEnough;
    public bool gameOver = false;
    public int score;
    public TMP_Text scoreText;
    private float debounceTime = 1.0f; // ����� �������� � ��������
    private float lastTriggerTime = 0f;
    public GameObject gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        Physics.gravity *= gravityModifier;
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
                scoreText.text = score.ToString();
                Debug.Log("Score: " + score);

                // ��������� ����� ���������� ��������
                lastTriggerTime = Time.time;
            }
        }
    }
}
