using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    Vector2 dir = Vector2.right;
    List<Transform> Tail = new List<Transform>();
    bool ate = false;
    bool gameOver = false;
    bool gamePause = false;
    public GameObject tailPrefab;
    public GameObject GameOverCanvas;
    public GameObject PauseCanvas;
    public AudioSource ScoreSound;
    public AudioSource GameOverSound;
    public AudioSource PauseSound;
    public AudioSource UnpauseSound;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Move", 0.3f, 0.3f);
        Time.timeScale = 1;
     
    }

    // Update is called once per frame
    void Update()
    {
        MovementControl();
        if (gameOver == true)
        {
            Restart();
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                PauseCanvas.SetActive(true);
                PauseSound.Play();
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                PauseCanvas.SetActive(false);
                UnpauseSound.Play();
            }
        }
        
    }

    void Move ()
    {
        Vector2 v = transform.position;
        
        
        transform.Translate(dir);
        
        if (ate)
        {
            GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);
            Tail.Insert(0, g.transform);
            ate = false;
        }
        
        
        else if (Tail.Count > 0)
        {
            Tail.Last().position = v;
            Tail.Insert(0, Tail.Last());
            Tail.RemoveAt(Tail.Count - 1);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "FoodPrefab")
        {
            ate = true;
            Destroy(coll.gameObject);
            //ScoreSound.Play();
            ScoreManagerSnake.instance.AddScore();
        }

        else if (coll.gameObject.tag == "Obstacle")
        {
            Debug.Log("Game Over");
            gameOver = true;
            GameOver();
            
        }
                
    }

    private void MovementControl()
    {
        //if (Input.GetKey(KeyCode.RightArrow))
        //    dir = Vector2.right;
        //else if (Input.GetKey(KeyCode.DownArrow))
        //    dir = Vector2.down;
        //else if (Input.GetKey(KeyCode.LeftArrow))
        //    dir = Vector2.left;
        //else if (Input.GetKey(KeyCode.UpArrow))
        //    dir = Vector2.up;

        if (hyper1.instance.buttonPin2State == 0)
        {
            dir = Vector2.right;
            Debug.Log("Right");
        }
        else if (hyper1.instance.buttonPin1State == 0)
        {
            dir = Vector2.down;
            Debug.Log("Down");
        }
        else if (hyper1.instance.buttonPin4State == 0)
            dir = Vector2.left;
        else if (hyper1.instance.buttonPin3State == 0)
            dir = Vector2.up;   
    }

    private void GameOver()
    {
        GameOverCanvas.SetActive(true);
        Time.timeScale = 0;
        GameOverSound.Play();
    }

    public void Restart()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}
