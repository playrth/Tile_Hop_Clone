using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject GameOverDisplay;

    Rigidbody BallRb;
    Vector3 target;
    public float h = 25;
    public float gravity = -1;

    bool InitializedPos = false;
    Vector3 CurrentPos=Vector3.zero;
    Vector3 PreviousPos= Vector3.zero;
    [SerializeField] float MinimumSwipeDistance = 40f;
    Vector3 HorrizontalDirection = Vector3.zero;
    [SerializeField] float HorrizontalSpeed = 2.0f;
    [SerializeField] GameObject PlayerParent;
    bool HasCollided = false;

    bool Gameover = false;

    [SerializeField]
    TMP_Text ScoreText;

    [SerializeField]
    TMP_Text GameOver_ScoreText;
    [SerializeField]
    TMP_Text GameOver_HighScoreText;

    [SerializeField] 
    Music MusicScriptableobject;
    [SerializeField]
    GameObject BounceAudioObj;



    int Score = 0;

    private void Awake()
    {
        AudioSystem.Singleton.Sound = MusicScriptableobject.Sound;
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioSystem.Singleton.PlaySound($"BackGround_{Random.Range(1,3)}", this.gameObject);
        GameOverDisplay.SetActive(false);
        AdManager.instance.RequestInterstitial();
        BallRb = this.GetComponent<Rigidbody>();
        BallRb.useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Gameover==false)
        {
            if (Touchscreen.current.primaryTouch.press.isPressed)
            {
                CurrentPos = Touchscreen.current.primaryTouch.position.ReadValue();
                if (!InitializedPos)
                {
                    PreviousPos = CurrentPos;
                    InitializedPos = true;
                }

                if (Vector2.Distance(CurrentPos, PreviousPos) >= MinimumSwipeDistance)
                {
                    DetectSwip();
                }

            }
            else
            {
                HorrizontalDirection = Vector3.zero;
                PreviousPos = Vector3.zero;
                CurrentPos = Vector3.zero;
                InitializedPos = false;
            }
            if (PlayerParent.transform.position.x >= -3.5 && PlayerParent.transform.position.x <= 3.5)
            {
                PlayerParent.transform.Translate(HorrizontalDirection * Time.deltaTime * HorrizontalSpeed);
            }
            if (PlayerParent.transform.position.x < -3.5)
            {
                PlayerParent.transform.position = new Vector3(-3.5f, PlayerParent.transform.position.y, PlayerParent.transform.position.z);
            }
            if (PlayerParent.transform.position.x > 3.5)
            {
                PlayerParent.transform.position = new Vector3(3.5f, PlayerParent.transform.position.y, PlayerParent.transform.position.z);
            }
            if (transform.position.y < 0)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
                GameOver();
            }
        }
        
    }
    void DetectSwip()
    {
        Vector2 Direction = CurrentPos - PreviousPos;
            Direction.Normalize();
        if(!HasCollided)
        {
            if (Vector2.Dot(Vector2.right, Direction) > 0.9f)
            {
                HorrizontalDirection = Vector3.right;
            }
            else if (Vector2.Dot(Vector2.left, Direction) > 0.9f)
            {
                HorrizontalDirection = Vector3.left;
            }
           
        }
        PreviousPos = CurrentPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(Gameover==false)
        {
            HasCollided = true;
            HorrizontalDirection = Vector3.zero;
            if (collision.gameObject.tag == "Tile")
            {
                AudioSystem.Singleton.StopSound(BounceAudioObj);
                AudioSystem.Singleton.PlaySound("Bounce", BounceAudioObj);
                Score++;
                ScoreText.text = Score.ToString();
                BallRb.useGravity = false;
                BallRb.velocity = Vector3.zero;
                GameObject o= SpawnManager.instance.OpenTilePool.Dequeue();
                SpawnManager.instance.ClosedTilePool.Enqueue(o);
                GameObject NextTarget = SpawnManager.instance.OpenTilePool.Peek();
                h = Vector3.Distance(collision.transform.position, NextTarget.transform.position) / 2;
                target = new Vector3(this.transform.position.x, 0, NextTarget.transform.position.z);
                Launch();
            }
            HasCollided = false;
        }
        
    }

    private void GameOver()
    {
        Gameover = true;
        AudioSystem.Singleton.StopSound(this.gameObject);
        AudioSystem.Singleton.PlaySound("GameOver", this.gameObject);
        AdManager.instance.ShowInterstitial();
        BallRb.isKinematic = true;
        if(Score>PlayerPrefs.GetInt("HighScore",0))
        {
            PlayerPrefs.SetInt("HighScore", Score);
            GameOver_ScoreText.text = "NEW HIGHSCORE:" + Score;
            GameOver_HighScoreText.text= "";
        }
        else
        {
            GameOver_ScoreText.text = "SCORE:" + Score;
            GameOver_HighScoreText.text = "HIGHSCORE:"+ PlayerPrefs.GetInt("HighScore", 0);
        }
        GameOverDisplay.SetActive(true);
    }

    void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        BallRb.useGravity = true;
        BallRb.velocity = CalculateLaunchData();
    }

    Vector3 CalculateLaunchData()
    {
        float displacementY = target.y - BallRb.position.y;
        Vector3 displacementXZ = new Vector3(target.x - BallRb.position.x, 0, target.z - BallRb.position.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return velocityXZ + velocityY * -Mathf.Sign(gravity);
    }
}

