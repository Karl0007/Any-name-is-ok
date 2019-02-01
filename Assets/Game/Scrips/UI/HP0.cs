using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP0 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<UnityEngine.UI.Slider>().value -= 
			(gameObject.GetComponent<UnityEngine.UI.Slider>().value - PlayerManager.Instance.m_Players[0].m_HP / PlayerManager.Instance.m_Players[0].m_HP_MAX)* 0.05f;
	}
}
