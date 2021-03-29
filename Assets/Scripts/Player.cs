using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Animator anim;

    public float boxCount = 0;
    private float speed = 0;
    private float score = 0;
    private float totalScore;

    public handle handle;
    public Text scoreText;
    public Text GameOverScoreText;
    public Text totalScoreText;
    public GameObject trail;
    public Slider slider;
    public ParticleSystem particle;
    public GameObject[] cubeCount;
    public UIControl ui;

    private Touch touch;
    private float touchSpeed = 0.01f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        totalScore = PlayerPrefs.GetFloat("totalScore");

    }


    void Update()
    {
        slider.value = transform.position.x;

        // THIS LINE CONTROLS THE WALK ANIMATION, IF PLAYER HAS NO ANY BOXES HE SHOULD BE WALK
        if (boxCount == 0)
        {
            anim.SetBool("zero", true);
        }
        else 
        {
            anim.SetBool("zero", false);
        }

        //swipe controls
        if (Input.touchCount > 0)
        {
            if (score == 0)
            {
                anim.SetTrigger("canMove");
                speed = -7;
                ui.closeFingerAnim();
            }

            // if there is more than 1 finger this line set it to 1 finger
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y,
                    transform.position.z + touch.deltaPosition.x * touchSpeed* -1);
            }
        }

        // Moves the player in X 
        transform.position = new Vector3(transform.position.x + (-1 * speed * Time.deltaTime), transform.position.y, transform.position.z);
    }







    private void OnTriggerEnter(Collider other)
    {

        // THIS LINE CONTROLS THE WALK ANIMATION, IF PLAYER HAS NO ANY BOXES HE SHOULD BE WALK
        if(boxCount == 0)
        {
            anim.SetBool("zero", true);
        }
        else
        {
            anim.SetBool("zero", false);
        }

        ///////////////////////// BLUE BOX  ////////////////////////////

        // if player takes box increase boxCount and instantiate new box parent object of player
        if (other.gameObject.CompareTag("box") && other.isTrigger)
        {
            UIControl.PlaySound("bubbleSound");
            anim.SetTrigger("Jump");
            score += 25;
            scoreText.text = "" + score;

            //this line will trigger the score animation
            handle.handleTrigger();

            //this line will trigger the particle when yellow box taken
            particle.Play();

            // I HAVE SET THE MAX BOX COUNT MAX 10
            if (boxCount < 10)
            {
                IncreaseBoxCount();

                transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

                // set the trail effects position with using boxCount
                trail.transform.position = new Vector3(transform.position.x - 0.25f, transform.position.y + (-boxCount + 0.01f), transform.position.z);
                for (int i = 0; i < boxCount; i++)
                {
                    cubeCount[i].gameObject.SetActive(true);

                }
            }


        }

        ///////////////////////// RED BOX  ////////////////////////////

        if (other.gameObject.CompareTag("redBox") && other.isTrigger)
        {
            UIControl.PlaySound("downSound");
            anim.SetTrigger("Jump");


            //  GAME OVER CHECK
            if (boxCount <= 0)
            {
                scoreText.text = "" + score;
                GameOverScoreText.text = "YOUR SCORE " + score;

                speed = 0;
                anim.SetTrigger("dead");
                StartCoroutine(GameOverPanelTimer());
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);

                // set the trail effects position with using boxCount
                trail.transform.position = new Vector3(transform.position.x - 0.25f, transform.position.y + (-boxCount + 0.01f), transform.position.z);

            }

            decreaseBoxCount();
            speed -= 0.2f;


            // this line sets the false all cubes in player 
            for (int i = 0; i < cubeCount.Length; i++)
            {
                cubeCount[i].gameObject.SetActive(false);

            }
            // this loop wil turn on the cubes after calculating box count
            for (int i = 0; i < boxCount; i++)
            {
                cubeCount[i].gameObject.SetActive(true);

            }
        }

        ///////////////////////// GEM  ////////////////////////////
        
        if (other.gameObject.CompareTag("gem") && other.isTrigger)
        {
            UIControl.PlaySound("gemSound");
            score += 50;
            scoreText.text = "" + score;

        }


        // FINISH LINE
        if(other.gameObject.CompareTag("finish") && other.isTrigger)
        {

            scoreText.text = "" + score;
            GameOverScoreText.text = "YOUR SCORE " + score;
            speed = 0;
            StartCoroutine(GameOverPanelTimer());
        }
    }




    public IEnumerator GameOverPanelTimer()
    {
        yield return new WaitForSeconds(1f);
        ui.GameOverPanel();
        gameObject.SetActive(false);

           // SAVING TOTAL SCORE
        totalScore += score;
        totalScoreText.text = "TOTAL SCORE"+ " " + totalScore;
        PlayerPrefs.SetFloat("totalScore", totalScore);

    }



    void IncreaseBoxCount()
    {
        boxCount++;
        scoreText.text = "" + score;

    }

    void decreaseBoxCount()
    {
        boxCount--;
        score -= 25;
        scoreText.text = "" + score;



    }


}
