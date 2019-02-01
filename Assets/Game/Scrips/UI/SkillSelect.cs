using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using EnumUtils;

public class SkillSelect : Button {

	public int m_SkillID;
	public int m_PlayerID;
	//public Sprite m_Sprite;
	public Player m_Player;
	public Skill m_Skill;

	// Use this for initialization
	protected override void Start () {
		m_Player = PlayerManager.Instance.m_Players[m_PlayerID];
		//Debug.Log(m_Player.m_Attack);
		m_Skill = m_Player.m_Skills[m_SkillID];
		gameObject.GetComponent<UnityEngine.UI.Image>().sprite = m_Skill.m_Skill.Sprite;

	}

	void SetText(UnityEngine.UI.Text text)
	{
		text.text  = m_Skill.m_Skill.Name+"  \n"+ m_Skill.m_Skill.Summary+"\n";
		text.text += "消耗体力：" + m_Skill.m_Skill.CostMP;
		if (m_Skill.m_Skill.CostMPType == CostMPType.ByLength) text.text += "每格";
		text.text += "\n伤害值：" + m_Skill.m_Skill.Attack;
		text.text += "\n优先级：" + m_Skill.m_Skill.Speed;
	}

	protected override void DoStateTransition(SelectionState state, bool instant)
	{
		base.DoStateTransition(state, instant);
		switch (state)
		{
			case SelectionState.Normal: //退出
				UIManager.Instance.Summary.transform.position = new Vector3(5000,5000,5000);
				gameObject.transform.localScale = new Vector3(1f,1f,1f);
				break;
			case SelectionState.Highlighted: //进入
				SetText(UIManager.Instance.Summary.GetComponent<UnityEngine.UI.Text>());
				UIManager.Instance.Summary.transform.position = gameObject.transform.position +  new Vector3(m_PlayerID == 0 ? 200 : -200,0, 0);
				gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
				break;
			case SelectionState.Pressed: //按下
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				if (GameManager.Instance.m_GameState == GameState.GetP1InputSkill && m_PlayerID == 0)
				{
					GameManager.Instance.m_GameState = GameState.GetP1InputOper;
					GameManager.Instance.P1Skill = m_SkillID;
				}
				if (GameManager.Instance.m_GameState == GameState.GetP2InputSkill && m_PlayerID == 1)
				{
					GameManager.Instance.m_GameState = GameState.GetP2InputOper;
					GameManager.Instance.P2Skill = m_SkillID;
				}
				break;
			case SelectionState.Disabled:
				//gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				break;
		}
	}


	// Update is called once per frame
	void Update () {
		if (GameManager.Instance.m_GameState != GameState.GetP1InputSkill && GameManager.Instance.m_GameState != GameState.GetP2InputSkill)
		{
			interactable = false;
		}
		else
		{
			interactable = true;
			//if (GameManager.Instance.m_GameState == GameState.GetP1InputSkill && m_PlayerID == 0)
			//	interactable = true;
			//else if (GameManager.Instance.m_GameState == GameState.GetP2InputSkill && m_PlayerID == 1)
			//	interactable = true;
			//else
			//	interactable = false;
		}
	}

}
