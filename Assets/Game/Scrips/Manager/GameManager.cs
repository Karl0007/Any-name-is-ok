using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumUtils;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	//public Character P1 = Character.Assassin;
	//public Character P2 = Character.Mage;
	public static GameManager Instance;
	public int P1Skill;
	public int P2Skill;
	public int P1Oper;
	public int P2Oper;
	public PlaySkill first, second;

	public GameState m_GameState;
	private void Awake()
	{
		Instance = this;
	}
	// Use this for initialization
	void Start() {
		//Debug.Log(123123);
		//PlayerManager.Instance.InitPlayerInfo(Character.Assassin, Character.Mage);
		PlayerManager.Instance.InitPlayerInfo(ChangeScene.Instance.P1, ChangeScene.Instance.P2);
		m_GameState = GameState.GetP1InputSkill;

	}

	void StartGame()
	{
		//PlayerManager.Instance.InitPlayerInfo(P1, P2);
		//m_GameState = GameState.GetP1InputSkill;
	}

	public delegate void PlaySkill();

	void P2Play()
	{
		if (PlayerManager.Instance.m_Players[1].m_playerView.startplay) return;
		//float tmpHP0 = PlayerManager.Instance.m_Players[0].m_HP;
		//float tmpHP1 = PlayerManager.Instance.m_Players[1].m_HP;
		PlayerManager.Instance.m_Players[1].UseSkill(P2Skill, P2Oper);

		//if (PlayerManager.Instance.m_Players[1].m_Skills[P2Skill].m_Skill.MoveType == MoveType.Flash)
		//	PlayerManager.Instance.m_Players[1].transform.position = new Vector3((PlayerManager.Instance.m_Players[1].m_Position - 7.5f), 0, 0);
		//if (tmpHP0 > PlayerManager.Instance.m_Players[0].m_HP) PlayerManager.Instance.m_Players[0].m_playerView.Blood = true;
		//if (tmpHP1 > PlayerManager.Instance.m_Players[1].m_HP) PlayerManager.Instance.m_Players[1].m_playerView.Blood = true;
		//Debug.Log(tmpHP0 + "  " + PlayerManager.Instance.m_Players[0].m_HP);
		//Debug.Log(tmpHP1 + "  " + PlayerManager.Instance.m_Players[1].m_HP);

		PlayerManager.Instance.m_Players[1].m_playerView.startplay = true;
	}

	void P1Play()
	{
		if (PlayerManager.Instance.m_Players[0].m_playerView.startplay) return;
		//float tmpHP0 = PlayerManager.Instance.m_Players[0].m_HP;
		//float tmpHP1 = PlayerManager.Instance.m_Players[1].m_HP;
		PlayerManager.Instance.m_Players[0].UseSkill(P1Skill, P1Oper);

		//if (PlayerManager.Instance.m_Players[0].m_Skills[P1Skill].m_Skill.MoveType == MoveType.Flash)
		//	PlayerManager.Instance.m_Players[0].transform.position = new Vector3((PlayerManager.Instance.m_Players[0].m_Position - 7.5f), 0, 0);

		//if (tmpHP0 > PlayerManager.Instance.m_Players[0].m_HP) PlayerManager.Instance.m_Players[0].m_playerView.Blood = true;
		//if (tmpHP1 > PlayerManager.Instance.m_Players[1].m_HP) PlayerManager.Instance.m_Players[1].m_playerView.Blood = true;
		PlayerManager.Instance.m_Players[0].m_playerView.startplay = true;
	}

	// Update is called once per frame
	void Update () {

		Player[] _p = new Player[2];
		_p[0] = PlayerManager.Instance.m_Players[0];
		_p[1] = PlayerManager.Instance.m_Players[1];
		if (m_GameState == GameState.GetP1InputSkill)
		{
			if (_p[0].m_Speed * _p[0].m_MP < _p[1].m_Speed * _p[1].m_MP)
			{
				UIManager.Instance.SetPriority(new Vector3(440, 270, 0));
			}
			else
			{
				UIManager.Instance.SetPriority(new Vector3(-440, 270, 0));
			}
		}
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
			UIManager.Instance.ClearMessage();
			if (_p[0].m_Speed * _p[0].m_MP + _p[0].m_Skills[P1Skill].m_Skill.Speed * 10000 <
				_p[1].m_Speed * _p[1].m_MP + _p[1].m_Skills[P2Skill].m_Skill.Speed * 10000)
			{
				first = P2Play;
				second = P1Play;
				//PlayerManager.Instance.m_Players[1].UseSkill(P2Skill, P2Oper);
				//PlayerManager.Instance.m_Players[0].UseSkill(P1Skill, P1Oper);
			}
			else
			{
				first = P1Play;
				second = P2Play;
				//PlayerManager.Instance.m_Players[0].UseSkill(P1Skill, P1Oper);
				//PlayerManager.Instance.m_Players[1].UseSkill(P2Skill, P2Oper);
			}
			m_GameState = GameState.P1Playing;

		}

		if (m_GameState == GameState.P1Playing)
		{
			first();
			//debug_show();
		}

		if (m_GameState == GameState.P2Playing)
		{
			second();
			//debug_show();
		}

		if (m_GameState == GameState.Done)
		{

			for (int i = 0; i <= 1; i++)
			{
				for (int k = 0; k < _p[i].m_Buff.Count; k++)
				{
					Debug.Log("P" + i + "buff " + _p[i].m_Buff[k].Type + "  " + _p[i].m_Buff[k].Time);
					if (--_p[i].m_Buff[k].Time <= 0)
					{
						switch (_p[i].m_Buff[k].Type)
						{
							case Buff.DefendAndAttack:
								_p[i].defend.GetComponent<ParticleSystem>().Stop();
								break;
							case Buff.CantDie:
								_p[i].cantdie.GetComponent<ParticleSystem>().Stop();
								break;
							case Buff.DoubleHarm:
								_p[i].doubleattack.GetComponent<ParticleSystem>().Stop();
								break;
						}
						if (_p[i].m_Buff[k].Type == Buff.CantDie) _p[i].m_HP = 0;
						for (int j = 0; j < _p[i].m_Buff.Count; j++)
						{
							if (_p[i].m_Buff[j].Type == _p[i].m_Buff[k].Type)
							{
								_p[i].m_Buff.RemoveAt(j);
							}
						}
						//_p[i].m_Buff.Remove(_p[i].m_Buff[k]);
					}
					//m_GameState = GameState.GetP1InputSkill;
				}
				for (int k = 0; k < _p[i].m_Debuff.Count; k++)
				{
					Debug.Log("P" + i + "DEbuff " + _p[i].m_Debuff[k].Type + "  " + _p[i].m_Debuff[k].Time);
					if (--_p[i].m_Debuff[k].Time <= 0)
					{
						switch (_p[i].m_Debuff[k].Type)
						{
							case DeBuff.Dizzy:
								_p[i].dizzy.GetComponent<ParticleSystem>().Stop();
								break;
							case DeBuff.Blood:
								_p[i].blood.GetComponent<ParticleSystem>().Stop();
								break;
							case DeBuff.PrePoison:
								_p[i].poison.GetComponent<ParticleSystem>().Stop();
								break;
						}
						for (int j = 0; j < _p[i].m_Debuff.Count; j++)
						{
							if (_p[i].m_Debuff[j].Type == _p[i].m_Debuff[k].Type)
							{
								_p[i].m_Debuff.RemoveAt(j);
							}
						}
						//_p[i].m_Debuff.Remove(_p[i].m_Debuff[k]);
					}
				}
				m_GameState = GameState.GetP1InputSkill;
				_p[i].m_MP += _p[i].m_MP_RE;
				if (_p[i].m_MP > _p[i].m_MP_MAX) _p[i].m_MP = _p[i].m_MP_MAX;
				if (_p[i].m_HP > _p[i].m_HP_MAX) _p[i].m_MP = _p[i].m_HP_MAX;
				if (_p[i].m_HP <= 0)
				{//Debug.Log(_p[i].Opponent.m_ID + "Win");
					UIManager.Instance.AddMessage("P" + (_p[i].Opponent.m_ID + 1) + "胜利！");
					ChangeScene2.Instance.Winer = "P" + (_p[i].Opponent.m_ID + 1) + "Win";
					SceneManager.LoadScene("begin");
				}
			}
			m_GameState = GameState.GetP1InputSkill;
			if (_p[0].m_Speed * _p[0].m_MP  < _p[1].m_Speed * _p[1].m_MP)
			{
				UIManager.Instance.SetPriority(new Vector3(440,270,0));
			}
			else
			{
				UIManager.Instance.SetPriority(new Vector3(-440, 270, 0));
			}
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
