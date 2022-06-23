using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed;
    public float rotateAmount;
    public float minX,minY,maxX,maxY;
    public GameObject food,danger;
    float rot;
    public int score=0;
    public Text scoreDisplay;
    Vector3 targetPos;

    void Start()
    {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        scoreDisplay.text = score.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousepos.x < 0)
            {
                rot = rotateAmount;
            }
            else rot = -rotateAmount;

            transform.Rotate(0, 0, rot);
        }
        rb.velocity = (transform.up)* moveSpeed* Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.gameObject.tag == "Food")
        {
            Destroy(collison.gameObject);
            score++;

            if (score!=0 && score % 3 == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    targetPos = GetRandomPosition();
                    Instantiate(food, targetPos, Quaternion.identity);
                }
            }
            if (score != 0 && score % 15 == 0)
            {
                moveSpeed += 5;
                targetPos = GetRandomPosition();
                Instantiate(danger, targetPos, Quaternion.identity);
            }
        }
        else if (collison.gameObject.tag == "Danger")
        {
            SceneManager.LoadScene("Game");
        }
        else if (collison.gameObject.tag == "Side")
        {
            SceneManager.LoadScene("Game");
        }
    }

    Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector3(randomX, randomY , 0);
    }

    public void ExitPressed()
    {
        Application.Quit();
    }
}
