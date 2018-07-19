using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField] float speed = 10.0f;
    [SerializeField] float accel;
    [SerializeField] int hitcount;
    public GameObject objRacket;

    [SerializeField] GameObject prefabHitEffect;
    [SerializeField] AudioClip hitSE;

    Rigidbody thisRigidbody;
    AudioSource thisAudioSource;

    GameManager gameMgr;



    Vector3 startPosition;
    
    // Use this for initialzation
    void Start()
    {
        gameMgr = FindObjectOfType<GameManager>();
        thisRigidbody = GetComponent<Rigidbody>();
        startPosition = transform.position;

        thisAudioSource = gameObject.AddComponent<AudioSource>();

        Init();
    }

    public void Init()
    {
        transform.position = startPosition;
        thisRigidbody.velocity = Vector3.zero;
        hitcount = 0;

        thisRigidbody.AddForce(
        (transform.forward + transform.right) * speed,
        ForceMode.VelocityChange);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void Up()
    {
        hitcount++;

        int judg;

        float diff = transform.position.x - objRacket.transform.position.x;
        Debug.Log(diff);
        if(diff <= 0)
        {
            judg = -1;
        }
        else
        {
            judg = 1;
        }

        thisRigidbody.velocity = new Vector3((speed + hitcount * accel) * judg, 
                                             0, 
                                             speed + hitcount * accel);
    }

    void OnCollisionEnter(Collision collision)
    {
        //衝突判定
        if (collision.gameObject.tag == "Bottom")
        {
            if (!gameMgr.isGameClear)
            {
                gameMgr.DcLife();
            }
        }
        else if (collision.gameObject.tag == "Racket")
        {
            Up();
        }
        else
        {
            hitcount++;

            GameObject effect = Instantiate(prefabHitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1);

            thisAudioSource.PlayOneShot(hitSE);
        }
    }

}
