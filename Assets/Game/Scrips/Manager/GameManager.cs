using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumUtils;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;
	public int P1Skill;
	public int P2Skill;
	public int P1Oper;
	public int P2Oper;

	public GameState m_GameState;
	private void Awake()
	{
		Instance = this;
	}
	// Use this for initialization
	void Start () {
		Debug.Log(123123);
		PlayerManager.Instance.InitPlayerInfo(Character.Warrior, Character.Assassin);

		m_GameState = GameState.GetP1InputSkill;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(1))
		{
			if (m_GameState == GameState.GetP1InputOper)
			{
				UIManager.Instance.ClearRange();
				m_GameState = GameState.GetP1InputSkill;
			}
			if (m_GameState == GameState.GetP2InputOper)
			{
				UIManager.Instance.ClearRange();
				m_GameState = GameState.GetP2InputSkill;
			}
		}

		if (m_GameState == GameState.GetResult)
		{
			Player[] _p = new Player[2];
			_p[0] = PlayerManager.Instance.m_Players[0];
			_p[1] = PlayerManager.Instance.m_Players[1];
			if (_p[0].m_Speed * _p[0].m_MP + _p[0].m_Skills[P1Skill].m_Skill.Speed * 10000 <
				_p[1].m_Speed * _p[1].m_MP + _p[1].m_Skills[P2Skill].m_Skill.Speed * 10000)
			{
				PlayerManager.Instance.m_Players[1].UseSkill(P2Skill, P2Oper);
				PlayerManager.Instance.m_Players[0].UseSkill(P1Skill, P1Oper);
			}
			else
			{
				PlayerManager.Instance.m_Players[0].UseSkill(P1Skill, P1Oper);
				PlayerManager.Instance.m_Players[1].UseSkill(P2Skill, P2Oper);
			}

			for (int i=0;i <= 1; i++)
			{
				for (int k= 0 ; k < _p[i].m_Buff.Count ; k++)
				{
					Debug.Log("P"+i+"buff " + _p[i].m_Buff[k].Type + "  "+ _p[i].m_Buff[k].Time);
					if (--_p[i].m_Buff[k].Time <= 0) _p[i].m_Buff.Remove(_p[i].m_Buff[k]);
					m_GameState = GameState.GetP1InputSkill;
				}
				for (int k = 0; k < _p[i].m_Debuff.Count; k++) 
				{
					Debug.Log("P" + i + "DEbuff " + _p[i].m_Debuff[k].Type + "  " + _p[i].m_Debuff[k].Time);
					if (--_p[i].m_Debuff[k].Time <= 0) _p[i].m_Debuff.Remove(_p[i].m_Debuff[k]);
					m_GameState = GameState.GetP1InputSkill;
				}
				_p[i].m_MP += _p[i].m_MP_RE;
				if (_p[i].m_MP > _p[i].m_MP_MAX) _p[i].m_MP = _p[i].m_MP_MAX;
				if (_p[i].m_HP > _p[i].m_HP_MAX) _p[i].m_MP = _p[i].m_HP_MAX;
				if (_p[i].m_HP <= 0) Debug.Log(_p[i].Opponent.m_ID+"Win");
			}


			m_GameState = GameState.GetP1InputSkill;
		}
		debug_show();
	}

	public GameObject debug_ui;
	public void debug_show()
	{
		Player p = PlayerManager.Instance.m_Players[0];
		debug_ui = GameObject.Find("Text");
		debug_ui.GetComponent<UnityEngine.UI.Text>().text = "HP" + p.m_HP + "/" + p.m_HP_MAX + "  " + "MP" + p.m_MP + "/" + p.m_MP_MAX + "\n" + "HP" + p.Opponent.m_HP + "/" + p.Opponent.m_HP_MAX + "  " + "MP" + p.Opponent.m_MP + "/" + p.Opponent.m_MP_MAX + "\n";
	}

}
