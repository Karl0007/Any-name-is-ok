using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumUtils;

public class PlayerManager : MonoBehaviour {

	private static PlayerManager Instance;

	public Player[] m_Players;
	private Skill[] m_Skills;
	private int[] OperatorNum;
	public CharacterDataS m_CharacterData;
	public AllSkills m_AllSkills;


	private void Awake()
	{
		Instance = this;
	}
	// Use this for initialization
	void Start () {
		InitPlayerInfo(Character.Warrior,Character.Mage);
		//m_Players[0].m_Skills[0].IntiSkill(m_Players[0],0);
		//m_Players[0].m_Skills[0].UseSkill();
		//m_Players[0].MoveToPosition();
		m_Players[0].UseSkill(0,10);
		m_Players[1].UseSkill(1, 0);
		m_Players[0].UseSkill(0,0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InitPlayerInfo(Character P1,Character P2)
	{
		m_Players = new Player[2];
		m_Players[0] = new Player();
		m_Players[1] = new Player();
		m_Players[0].M_Player = GameObject.Find("Cube");
		m_Players[1].M_Player = GameObject.Find("Sphere");
		m_Players[0].Opponent = m_Players[1];
		m_Players[1].Opponent = m_Players[0];
		foreach (var i in m_CharacterData.CharacterInfo)
		{
			if (P1 == i.Type)
			{
				m_Players[0].m_CharacterType = P1;
				m_Players[0].m_Attack = i.Attack;
				m_Players[0].m_HP = m_Players[0].m_HP_MAX = i.HP;
				m_Players[0].m_MP = m_Players[0].m_MP_MAX = i.MP;
				m_Players[0].m_MP_RE = i.RE_MP;
				m_Players[0].m_Speed = i.Speed;
				m_Players[0].m_Position = 5;
			}
			if (P2 == i.Type)
			{
				m_Players[1].m_CharacterType = P2;
				m_Players[1].m_Attack = i.Attack;
				m_Players[1].m_HP = m_Players[1].m_HP_MAX = i.HP;
				m_Players[1].m_MP = m_Players[1].m_MP_MAX = i.MP;
				m_Players[1].m_MP_RE = i.RE_MP;
				m_Players[1].m_Speed = i.Speed;
				m_Players[1].m_Position = 9;
			}
		}
		foreach (var i in m_AllSkills.SkillInfos)
		{
			foreach (var j in m_Players)
				if (j.m_CharacterType == i.Character || i.Character == Character.None)
				{
					Skill tmpskill = new Skill();
					tmpskill.m_Skill = i;
					Debug.Log(j.m_HP);
					j.m_Skills.Add(tmpskill);
				}
		}
		m_Players[1].MoveToPosition();
		m_Players[0].MoveToPosition();
	}

	void GetPlayerInput()
	{

	}

}
