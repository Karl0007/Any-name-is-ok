using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumUtils;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public PlayerView m_playerView;

	public Character m_CharacterType;
	public float m_HP { get; set; }
	public float m_HP_MAX { get; set; }
	public float m_MP { get; set; }
	public float m_MP_MAX { get; set; }
	public float m_Speed { get; set; }
	public float m_Attack { get; set; }
	public float m_MP_RE { get; set; }
	public int m_Position { get; set; }
	public int m_ID { get; set; }
	public List<BuffClass> m_Buff = new List<BuffClass>();
	public List<DebuffClass> m_Debuff = new List<DebuffClass>();
	public List<Skill> m_Skills = new List<Skill>();
	public GameObject M_Player;
	public GameObject dizzy;
	public GameObject blood;
	public GameObject poison;
	public GameObject doubleattack;
	public GameObject defend;
	public GameObject cantdie;


	public Player Opponent;
	static bool INIT = false;

	public void Init()
	{

		Debug.Log(EffectManager.Instance.x);
		Debug.Log(EffectManager.Instance.m_Effects.Count);
		dizzy = Instantiate(EffectManager.Instance.m_Effects["dizzy"]);
		blood = Instantiate(EffectManager.Instance.m_Effects["blood"]);
		poison = Instantiate(EffectManager.Instance.m_Effects["poison"]);
		defend = Instantiate(EffectManager.Instance.m_Effects["DFA"]);
		cantdie = Instantiate(EffectManager.Instance.m_Effects["canttdie"]);
		doubleattack = Instantiate(EffectManager.Instance.m_Effects["double"]);
		INIT = true;
		dizzy.transform.parent = M_Player.transform;
		poison.transform.parent = M_Player.transform;
		defend.transform.parent = M_Player.transform;
		cantdie.transform.parent = M_Player.transform;
		doubleattack.transform.parent = M_Player.transform;
		blood.transform.parent = M_Player.transform;

		dizzy.transform.position = M_Player.transform.position;
		poison.transform.position = M_Player.transform.position;
		defend.transform.position = M_Player.transform.position;
		cantdie.transform.position = M_Player.transform.position;
		doubleattack.transform.position = M_Player.transform.position;
		blood.transform.position = M_Player.transform.position;

		dizzy.transform.localPosition += new Vector3(0,1,0);
		poison.transform.localPosition += new Vector3(0,0.5f,0);
		blood.transform.localPosition += new Vector3(0,0.5f,0);
		defend.transform.localPosition += new Vector3(0,0.5f,0);

		dizzy.GetComponent<ParticleSystem>().Stop();
		blood.GetComponent<ParticleSystem>().Stop();
		poison.GetComponent<ParticleSystem>().Stop();
		defend.GetComponent<ParticleSystem>().Stop();
		cantdie.GetComponent<ParticleSystem>().Stop();
		doubleattack.GetComponent<ParticleSystem>().Stop();

		//dizzy.transform.localPosition += new Vector3(0,1,0);

		//dizzy.transform.position = new Vector3(0, 0, 0);
		// dizzy.GetComponent<ParticleSystem>().Play();
	}
	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {

	}


	public void UseSkill(int _skill,int _oper)
	{
		if (!INIT)
		{
			Init();
			Opponent.Init();
		}
		if (FindSetBuff(DeBuff.Dizzy)>0 || FindSetBuff(DeBuff.Freeze)>0)
		{
			Debug.Log("被控制无法行动");
			UIManager.Instance.AddMessage("玩家P" + (m_ID+1) +"眩晕 无法行动");
			MoveToPosition();
			return;
		}
		Debug.Log("行动");
		UIManager.Instance.AddMessage("玩家P" + (m_ID+1) + "使用了技能 ： " + m_Skills[_skill].m_Skill.Name);
		//Debug.Log( EffectManager.Instance.x);
		//EffectManager.Instance.PlaySkillEffects(new Vector3(6, 0, 0), "attack3");
		m_Skills[_skill].IntiSkill(this,_oper);
		m_Skills[_skill].UseSkill();
		MoveToPosition();
		Debug.Log("attackrange" + m_Skills[_skill].AttackRange.Count);
		foreach (var i in m_Skills[_skill].AttackRange)
		{
			string tmp = "";
			switch (m_CharacterType)
			{
				case Character.None:
					break;
				case Character.Warrior:
					EffectManager.Instance.PlaySkillEffects(new Vector3(i - 7.5f, 0, 0), "attack2");
					break;
				case Character.Mage:
					EffectManager.Instance.PlaySkillEffects(new Vector3(i - 7.5f, 0, 0), "attack3");
					break;
				case Character.Assassin:
					EffectManager.Instance.PlaySkillEffects(new Vector3(i - 9f, 0, 0), "attack1");
					break;
			}
			Debug.Log(tmp);
			
		}
	}

	public void MoveToPosition()
	{
		//Debug.Log(m_Position);
		m_playerView.MoveTo(new Vector3((m_Position - 7.5f), 0, 0));
		if (m_Position < Opponent.m_Position)
			M_Player.transform.localEulerAngles = new Vector3(0, 90, 0);
		else
			M_Player.transform.localEulerAngles = new Vector3(0,-90,0);

		//M_Player.transform.position = new Vector3(m_Position - 7,0,0);
	}

	public int FindSetBuff(Buff _buff, int _set = -1)
	{
		int cnt = 0;
		foreach (var i in m_Buff)
		{
			if (i.Type == _buff && i.Time > 0)
			{
				cnt++;
				if (_set >= 0) i.Time = _set;
			}
		}
		return cnt;
	}

	public int FindSetBuff(DeBuff _debuff, int _set = -1)
	{
		int cnt = 0;
		foreach (var i in m_Debuff)
		{
			if (i.Type == _debuff && i.Time > 0)
			{
				cnt++;
				if (_set >= 0) i.Time = _set;
			}
		}
		return cnt;
	}


	//DEBUG

	//DEBUG
}
