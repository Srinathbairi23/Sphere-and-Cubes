using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SphereMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] GameManager gameManager;
    [SerializeField] float speedMultiplier;
    int score;
    string lastcube;
    int streakcount = 1;
    [SerializeField] Text scoreText;
    [SerializeField] Text streakText;
    [SerializeField] Text finalScoreDisplay;
    [SerializeField] List<GameObject> collidedcube = new List<GameObject>();

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rb.AddForce(Vector3.forward * y * speedMultiplier);
        rb.AddForce(Vector3.right * x * speedMultiplier);

        if (gameManager.gameEnd)
        {
            //if time ends show the score to the player.
            finalScoreDisplay.text = "Your Score is : " + score;

            if (score > PlayerPrefs.GetInt("highscore"))
            {
                PlayerPrefs.SetInt("highscore", score);
            }
            rb.isKinematic = true;
            Invoke("MainScene", 5f);        
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //for Red cube.
        if (collision.collider.tag == "Red")
        {
            if (lastcube == "Red")
            {
                //increasing the streak multiplier if the last cube color is same as the current cube.
                streakcount++;
                streakText.text = streakcount.ToString() + " X 15" ; 
                score += streakcount * 15;
            }
            else
            {
                //setting streak to 0 if the last cube color different from the current one.
                streakText.text = null;
                streakcount = 1;
                score += 15;
            }

            //setting this cube as last cube.
            lastcube = "Red";

            //turning the collided cube color to black so player can identify easily which are left to collide.
            collision.collider.GetComponent<Renderer>().material.color = Color.black;

            collidedcube.Add(collision.gameObject);

        }

        //For blue cube.
        if (collision.collider.tag == "Blue")
        {
            if (lastcube == "Blue")
            {
                streakcount++;
                score += streakcount * 20;
                streakText.text = streakcount.ToString() + " X 20";
            }
            else
            {
                streakText.text = null;
                streakcount = 1;
                score += 20;
            }
            lastcube = "Blue";

            //turning the collided cube color to black so player can identify easily which are left to collide.
            collision.collider.GetComponent<Renderer>().material.color = Color.black;

            collidedcube.Add(collision.gameObject);

        }
        //changing tag so player cant gain score by hitting the same cube again and again.
        collision.collider.tag = "Cube";

        //changing score.
        scoreText.text = "Score: " + score.ToString();

        //if player collided all of the cubes then show the score adding time also.
        if(collidedcube.Count == 10)
        {
            int finalScore = score + gameManager.timeleft;
            gameManager.freezetimer = true;
            scoreText.text = "Score: " + finalScore.ToString();
            finalScoreDisplay.text = "Your Score is : " + finalScore;

            if (score > PlayerPrefs.GetInt("highscore"))
            {
                PlayerPrefs.SetInt("highscore", finalScore);
            }
            DisableCube();
            Invoke("MainScene", 5f);
        }
    }

    void MainScene()
    {
        SceneManager.LoadScene(0);
    }

    //disables all cube once player hit all of them.
    void DisableCube()
    {
        foreach(GameObject cube in collidedcube)
        {
            cube.SetActive(false);
        }
    }

}

