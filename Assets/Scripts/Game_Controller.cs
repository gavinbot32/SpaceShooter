using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Timers;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game_Controller : MonoBehaviour
{

    Enemy[] enemies;
    Enemy rando;

    //shooting
    public float shootInterval = 3f;
    public float shootSpeed = 2f;
    public GameObject enemyBulletPrefab;
    private float shootTimer;



    public GameObject enemyContainer;
    public float moveInterval = 0.5f;
    public float maxMoveInterval;
    public float minMoveInterval = 0.05f;
    public float moveDistance = 0.1f;
    public float limit;
    private float movingDir = 1;
    private float movingTimer;
    private bool downGate = true;

    public float moveMod = 0.1f;

    private int enemyCount;
    private int maxEnemies = 55;

    private int score;
    private int wave;
    private int lives;
    public int highScore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI liveText;


    // Start is called before the first frame update
    void Start()
    {
        maxMoveInterval = moveInterval;
        if (PlayerPrefs.HasKey("score"))
        {
            score = PlayerPrefs.GetInt("score");
            wave = PlayerPrefs.GetInt("wave");
            lives = PlayerPrefs.GetInt("lives");
        }
        else
        {
            score = 0;
            wave = 1;
            lives = 3;
        }

        if (PlayerPrefs.HasKey("highscore"))
        {
            highScore = PlayerPrefs.GetInt("highscore");
        }
        else
        {
            highScore = 0;
        }

        update_ui();

        shootTimer = shootInterval;
        movingTimer = moveInterval;
  //      downTimer = downInterval;
        enemies = GetComponentsInChildren<Enemy>();
        enemyCount = enemies.Length;
    }

    public void addScore(int points)
    {
        score += points;
        update_ui();
    }

    public void clear_all()
    {
        PlayerPrefs.DeleteKey("wave");
        PlayerPrefs.DeleteKey("score");
        PlayerPrefs.DeleteKey("lives");
    }
    public void player_die()
    {
        lives--;
        update_ui();
        if (lives < 0)
        {

           
            clear_all();
            SceneManager.LoadScene("GameOver");
        }
    }
    public void update_ui()
    {
       scoreText.text = "Score: "+score.ToString();
       waveText.text = "Wave: "+wave.ToString();
        liveText.text = "Lives: "+lives.ToString();
    }

    // Update is called once per frame
    void Update()
    {

            
            enemyCount = enemies.Length;

        if (enemyCount > 0)
        {
            
            

            movingTimer -= Time.deltaTime;
            float diffsetting = 1f - (float)enemyCount / maxEnemies;
            moveInterval = maxMoveInterval - (maxMoveInterval - minMoveInterval) * diffsetting;

            if (movingTimer <= 0)
            {
                movingTimer = moveInterval;



                float rightMost = 0f;
                float leftmost = 0f;
                foreach (Enemy enemy in enemies)
                {
                    if (enemy.transform.position.x > rightMost)
                    {
                        rightMost = enemy.transform.position.x;
                    }
                    if (enemy.transform.position.x < leftmost)
                    {
                        leftmost = enemy.transform.position.x;
                    }
                }
                if (movingDir != 0)
                {

                    if (!downGate)
                        if (rightMost >= limit || leftmost <= -limit)
                        {
                            downGate = true;
               
                            enemyContainer.transform.position = new Vector3(enemyContainer.transform.position.x
                            , enemyContainer.transform.position.y - 0.5f
                            , 0f);
                            movingDir *= -1;
                        }
                        else
                        {
                            enemyContainer.transform.position = new Vector3(enemyContainer.transform.position.x + (movingDir * moveDistance)
                          , enemyContainer.transform.position.y
                          , 0f);
                        }
                    else
                    {
                        enemyContainer.transform.position = new Vector3(enemyContainer.transform.position.x + (movingDir * moveDistance)
                          , enemyContainer.transform.position.y
                          , 0f);
                        downGate = false;
                    }


                }

            }
        }
        else
        {
            wave++;
            save_data();

            SceneManager.LoadScene("Game");
        }
        
        
            
        



        enemies = GetComponentsInChildren<Enemy>();
        shootTimer -= Time.deltaTime;
        if(shootTimer <= 0)
        {
            shootInterval = Random.Range(2, 6);
            shootSpeed = Random.Range(2, 5);
            shootTimer = shootInterval;
            rando = enemies[Random.Range(0, enemies.Length)];
            GameObject enemyBullet = Instantiate(enemyBulletPrefab);
            enemyBullet.transform.SetParent(transform);
            enemyBullet.transform.position = new Vector3(rando.transform.position.x, rando.transform.position.y - 0.25f, rando.transform.position.z);
            enemyBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -shootSpeed);
            Destroy(enemyBullet, 3f);
        }
    }
    public void save_data()
    {
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("wave", wave);
        PlayerPrefs.SetInt("Lives", lives);
    }
}
