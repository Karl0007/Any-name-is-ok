﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumUtils;
using UnityEngine.SceneManagement;

public class PlayerView : MonoBehaviour {

	public GameObject m_Player;
	public Vector3 Distination;
	public bool Blood;
	public bool startplay;

	public void MoveTo(Vector3 x)
	{
		Distination = x;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(Vector3.Distance(Distination, gameObject.transform.position));
		if (Vector3.Distance(Distination , gameObject.transform.position) > 0.1f)
		{
			//startplay = true;
			gameObject.transform.position += (Distination - gameObject.transform.position).normalized * Time.deltaTime *5;
		}
		else
		{
			//Debug.Log(GameManager.Instance.m_GameState +" " +startplay);
			if (GameManager.Instance.m_GameState == GameState.P1Playing && startplay && !AudioManager.Instance.Effect.isPlaying)
			{
				//Debug.Log(Blood);
				//if (Blood) EffectManager.Instance.PlayBlood(gameObject.transform.position);
				GameManager.Instance.m_GameState = GameState.P2Playing;
				startplay = false;
				Blood = false;
				if (PlayerManager.Instance.m_Players[1].m_HP <= 0)
				{
					ChangeScene2.Instance.Winer = "P1 Win";
					SceneManager.LoadScene("begin");
				}
				if (PlayerManager.Instance.m_Players[0].m_HP <= 0)
				{
					ChangeScene2.Instance.Winer = "P2 Win";
					SceneManager.LoadScene("begin");
				}
			}
			else if (GameManager.Instance.m_GameState == GameState.P2Playing && startplay && !AudioManager.Instance.Effect.isPlaying)
			{
				//if (Blood) EffectManager.Instance.PlayBlood(gameObject.transform.position);
				GameManager.Instance.m_GameState = GameState.Done;
				startplay = false;
				Blood = false;
				if (PlayerManager.Instance.m_Players[0].m_HP <= 0)
				{
					ChangeScene2.Instance.Winer = "P2 Win";
					SceneManager.LoadScene("begin");
				}
				if (PlayerManager.Instance.m_Players[1].m_HP <= 0)
				{
					ChangeScene2.Instance.Winer = "P1 Win";
					SceneManager.LoadScene("begin");
				}
			}

		}
	}
}
