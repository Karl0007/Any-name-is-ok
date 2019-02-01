using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour {

	int op;
	// Use this for initialization
	void Start () {
		op = 1;
		gameObject.GetComponent<Light>().intensity = Random.Range(0,1);
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Light>().intensity += op * Time.deltaTime * 0.05f;
		if (gameObject.GetComponent<Light>().intensity >= 1) op = -1;
		if(gameObject.GetComponent<Light>().intensity <= 0) op = 1;

	}
}
