using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketBehaviourScript : MonoBehaviour {

    private float accel = 1750.0f;//加える力の大きさ

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //力を加える
        this.GetComponent<Rigidbody>().AddForce(
            transform.right * Input.GetAxisRaw("Horizontal") * accel,
            ForceMode.Impulse);
	}
}
