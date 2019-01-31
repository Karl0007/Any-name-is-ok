using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using EnumUtils;

public class OperSelect : Button {


	public int m_ID;
	public bool m_InUI;
	// Use this for initialization
	//void Start () {
		
	//}
	
	public void SetColorRed()
	{
		gameObject.GetComponent<Image>().color = new Color(1,0,0,0.1f);
	}

	public void SetColorGreen()
	{
		gameObject.GetComponent<Image>().color = new Color(0,1,0,0.1f);
	}

	public void SetColorWhite()
	{
		gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.1f);
	}

	protected override void DoStateTransition(SelectionState state, bool instant)
	{
		base.DoStateTransition(state, instant);
		switch (state)
		{
			case SelectionState.Normal: //退出
				//if (!m_InUI) UIManager.Instance.ClearRange();
				//SetColorWhite();
				break;
			case SelectionState.Highlighted: //进入
				UIManager.Instance.GetRange(m_ID);
				//m_InUI = true;
				//SetColorGreen();
				break;
			case SelectionState.Pressed: //按下
				if (GameManager.Instance.m_GameState == GameState.GetP1InputOper)
				{
					GameManager.Instance.P1Oper =  UIManager.Instance.GetRange(m_ID);
					//Debug.Log("P1 "+GameManager.Instance.P1Oper);
					UIManager.Instance.ClearRange();
					GameManager.Instance.m_GameState = GameState.GetP2InputSkill;
				}
				if (GameManager.Instance.m_GameState == GameState.GetP2InputOper)
				{
					GameManager.Instance.P2Oper = UIManager.Instance.GetRange(m_ID);
					//Debug.Log("P2 " + GameManager.Instance.P1Oper);
					UIManager.Instance.ClearRange();
					GameManager.Instance.m_GameState = GameState.GetResult;
				}
				break;
			case SelectionState.Disabled:
				//gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				break;
		}
	}
	// Update is called once per frame
	void Update () {
		//Debug.Log(GameManager.Instance.m_GameState);
		if (GameManager.Instance.m_GameState != GameState.GetP1InputOper && GameManager.Instance.m_GameState != GameState.GetP2InputOper)
		{
			interactable = false;
		}
		else
		{
			interactable = true;
		}
	}
}
