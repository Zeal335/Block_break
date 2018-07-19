using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int score;

    GameManager gameMgr;

    // Use this for initialization
    void Start()
    {
        gameMgr = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        //衝突判定
        if (collision.gameObject.tag == "Ball")
        {
            //相手のタグがBallならば、自らを消す
            gameMgr.OnBlockBreak(score);
            Destroy(this.gameObject);
        }
    }
}
