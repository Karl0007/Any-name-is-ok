using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumUtils;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	private PlayerView playerView;

	public Character m_CharacterType;
	public float m_HP { get; set; }
	public float m_HP_MAX { get; set; }
	public float m_MP { get; set; }
	public float m_MP_MAX { get; set; }
	public float m_Speed { get; set; }
	public float m_Attack { get; set; }
	public float m_MP_RE { get; set; }
	public int m_Position { get; set; }
	public int m_ID { get; private set; }
	public List<BuffClass> m_Buff = new List<BuffClass>();
	public List<DebuffClass> m_Debuff = new List<DebuffClass>();
	public List<Skill> m_Skills = new List<Skill>();
	public GameObject M_Player;

	public Player Opponent;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void UseSkill(int _skill,int _oper)
	{
		m_Skills[_skill].IntiSkill(this,_oper);
		m_Skills[_skill].UseSkill();
		MoveToPosition();
	}

	public void MoveToPosition()
	{
		M_Player.transform.position = new Vector3(m_Position - 7,0,0);
		debug_show();
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
	public GameObject debug_ui;
	void debug_show()
	{
		debug_ui = GameObject.Find("Text");
		debug_ui.GetComponent<UnityEngine.UI.Text>().text = "HP" + m_HP + "/" + m_HP_MAX + "  " + "MP" + m_MP + "/" + m_MP_MAX + "\n"; 
	}

	//DEBUG
}
