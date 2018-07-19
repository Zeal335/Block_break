using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour {

    [SerializeField] Text scoreText;
    [SerializeField] Text lifeText;
    [SerializeField] Text clearText;
    [SerializeField] GameObject objGameOver;
    [SerializeField] Text Title;
    [SerializeField] AudioClip gameClearBgm;
    [SerializeField] AudioClip gameLastBgm;
    [SerializeField] GameObject objClearEffect;


    int totalScore;
    int playerLife;

    int blockCount;
    int allBlockCount;

    public GameObject objBall;
    Ball ball;

    AudioSource thisAudioSource;

    public bool isGameClear { get { return blockCount <= 0; } }
    public bool isGameLast { private set; get; }

    // Use this for initialization
    void Start ()
    {
        objClearEffect.SetActive(false);
        clearText.gameObject.SetActive(false);
        objGameOver.SetActive(false);
        Title.gameObject.SetActive(false);
        playerLife = 5;
        scoreText.text = totalScore.ToString();
        lifeText.text = playerLife.ToString();
        ball = objBall.GetComponent<Ball>();
        thisAudioSource = GetComponent<AudioSource>();

        allBlockCount = FindObjectsOfType<Block>().Length;
        blockCount = allBlockCount;

        Debug.Log(blockCount);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnBlockBreak(int score)
    {
        AddScore(score);
        blockCount--;
        if (blockCount <= 0)
        {
            GameClear();
        }
        else if(blockCount <= ( allBlockCount / 4) && !isGameLast)
        {
            isGameLast = true;
            thisAudioSource.Stop();
            thisAudioSource.loop = true;
            thisAudioSource.clip = gameLastBgm;
            thisAudioSource.Play();
        }
    }

    public void AddScore(int score)
    {
        totalScore += score;
        scoreText.text = totalScore.ToString();
    }

    public void DcLife()
    {
        playerLife -= 1;
        lifeText.text = playerLife.ToString();
        objBall.SetActive(false);
        if (playerLife == 0)
        {
            Gameover();
        }
        else
        {
            StartCoroutine(Revival());
        }
    }

    void GameClear()
    {
        objClearEffect.SetActive(true);
        clearText.gameObject.SetActive(true);
        StartCoroutine(GoToTitle());
        thisAudioSource.Stop();
        thisAudioSource.loop = true;
        thisAudioSource.clip = gameClearBgm;
        thisAudioSource.Play();
    }

    void Gameover()
    {

        objGameOver.SetActive(true);
        StartCoroutine(GoToTitle());
    }

    IEnumerator Revival()
    {
        yield return new WaitForSeconds(1);
        objBall.SetActive(true);
        ball.Init();
    }

    IEnumerator GoToTitle()
    {
        Title.gameObject.SetActive(true);
        while (!Input.GetKey(KeyCode.Return))
        {
            yield return null;
        }

        SceneManager.LoadScene(0);
    }
}
