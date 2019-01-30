using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumUtils;

public class Player : MonoBehaviour {

	private PlayerView playerView;

	public float m_HP { get; set; }
	public float m_HP_MAX { get; set; }
	public float m_MP { get; set; }
	public float m_MP_MAX { get; set; }
	public float m_Speed { get; set; }
	public float m_Attack { get; set; }
	public float m_MP_RE { get; set; }
	public int m_Position { get; set; }
	public int m_ID { get; private set; }
	public List<BuffClass> m_Buff;
	public List<DebuffClass> m_Debuff;
	public Skill[] skills;


	public Player Opponent;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
