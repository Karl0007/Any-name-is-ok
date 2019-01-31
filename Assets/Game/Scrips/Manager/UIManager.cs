using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	public static UIManager Instance;

	public GameObject[] P1Button;
	public GameObject[] P2Button;
	public GameObject[] Position;
	public GameObject Summary;
	private void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		P1Button = new GameObject[5];
		P2Button = new GameObject[5];
		Position = new GameObject[15];
		GameObject ss = GameObject.Find("SelectSkill");
		GameObject cv = GameObject.Find("Canvas");
		GameObject op = GameObject.Find("SelectOper");
		Summary = GameObject.Find("Summary");
		for (int i = 0; i < 5; i++)
		{
			P1Button[i] = Instantiate(ss);
			P2Button[i] = Instantiate(ss);
			P1Button[i].transform.parent = cv.transform;
			P2Button[i].transform.parent = cv.transform;

			P1Button[i].GetComponent<SkillSelect>().m_PlayerID = 0;
			P1Button[i].GetComponent<SkillSelect>().m_SkillID = i;
			P1Button[i].GetComponent<UnityEngine.UI.Image>().rectTransform.localPosition = new Vector3(-450, 200 - i*125, 0);

			P2Button[i].GetComponent<SkillSelect>().m_PlayerID = 1;
			P2Button[i].GetComponent<SkillSelect>().m_SkillID = i;
			P2Button[i].GetComponent<UnityEngine.UI.Image>().rectTransform.localPosition = new Vector3(450, 200- i * 125, 0);

		}
		for (int i = 1; i < 15; i++)
		{
			Position[i] = Instantiate(op);
			Position[i].transform.parent = cv.transform;
			Position[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			Position[i].GetComponent<OperSelect>().m_ID = i;
			Position[i].GetComponent<UnityEngine.UI.Image>().rectTransform.localPosition = new Vector3(-375 + i*50 , -300, 0);
		}
		op.SetActive(false);
		ss.SetActive(false);
	}

	public void ClearRange()
	{
		for (int i = 1; i < 15; i++)
		{
			Position[i].GetComponent<OperSelect>().SetColorWhite();
		}
	}
	public int GetRange(int n)
	{
		Player P1 = PlayerManager.Instance.m_Players[0];
		Player P2 = PlayerManager.Instance.m_Players[1];
		int P1S = GameManager.Instance.P1Skill;
		int P2S = GameManager.Instance.P2Skill;
		if (GameManager.Instance.m_GameState == EnumUtils.GameState.GetP1InputOper)
		{
			ClearRange();
			P1.m_Skills[P1S].GetRange(P1, n);
			Debug.Log(P1.m_Skills[P1S].MoveRange.Count);
			foreach (var i in P1.m_Skills[P1S].MoveRange)
			{
				Position[i].GetComponent<OperSelect>().SetColorGreen();
			}
			foreach (var i in P1.m_Skills[P1S].AttackRange)
			{
				Position[i].GetComponent<OperSelect>().SetColorRed();
			}
			Debug.Log(P1.m_Skills[P1S].OperatorNum);
			return P1.m_Skills[P1S].OperatorNum;
		}
		else
		{
			ClearRange();
			P2.m_Skills[P2S].GetRange(P2, n);
			foreach (var i in P2.m_Skills[P2S].MoveRange)
			{
				Position[i].GetComponent<OperSelect>().SetColorGreen();
			}
			foreach (var i in P2.m_Skills[P2S].AttackRange)
			{
				Position[i].GetComponent<OperSelect>().SetColorRed();
			}
			Debug.Log(P1.m_Position);
			Debug.Log(P2.m_Skills[P2S].OperatorNum);
			return P2.m_Skills[P2S].OperatorNum;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
