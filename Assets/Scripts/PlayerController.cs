using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private AudioSource playerAudio;
    //public ParticleSystem exposionPatrical;
    public AudioClip scoreSound;
    public AudioClip crushSound;
    public float jumpForce;
    public float gravityModifier;
    public bool gameOver = false;
    public int score;
    public TMP_Text scoreText;
    private float debounceTime = 1.0f; // waiting time in seconds
    private float lastTriggerTime = 0f;
    public GameObject gameOverText;
    private ScoreManager scoreManager;

    private void Awake()
    {
        if (Application.persistentDataPath + "/savefile.json" != null)
        {
            UpdateScoreUI();
            LoadScore();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        SaveScore();
        Debug.Log(Application.persistentDataPath);
        LoadScore();
        UpdateScoreUI();
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
        SaveScore();
        LoadScore();
    }

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
            // Check if enough time has passed since the last trigger
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

                // Update last trigger time
                lastTriggerTime = Time.time;
            }
        }
    }

    [System.Serializable]
    class SaveData
    {
        public int score;
    }

    public void SaveScore()
    {
        Debug.Log("Saving Score: " + score); 

        SaveData data = new SaveData();
        data.score = score; // Saving score

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        Debug.Log("Score saved successfully");
    }
    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path); // Read from JSON file
            SaveData data = JsonUtility.FromJson<SaveData>(json); // Pars JSON

            score = data.score; // Loading saved scores
            scoreText.text = score.ToString();
            Debug.Log("Score loaded: " + score);
        }
        else
        {
            Debug.Log("Save file not found!");
        }
    }
    void OnApplicationQuit()
    {
        SaveScore(); 
    }
    void UpdateScoreUI()
    {
        scoreText.text = score.ToString();
    }
}
