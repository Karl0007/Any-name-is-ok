using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using EnumUtils;
using UnityEngine.SceneManagement;

public class ChooseCharacter : MonoBehaviour {

	public Button[] button;

	public void GoToGame()
	{
		if (ChangeScene.Instance.P1 != Character.None && ChangeScene.Instance.P2 != Character.None)
			SceneManager.LoadScene("test");
	}

	public void SetP1W()
	{
		ChangeScene.Instance.P1 = Character.Warrior;
	}

	public void SetP2W()
	{
		ChangeScene.Instance.P2 = Character.Warrior;
	}

	public void SetP1A()
	{
		ChangeScene.Instance.P1 = Character.Assassin;
	}

	public void SetP2A()
	{
		ChangeScene.Instance.P2 = Character.Assassin;
	}

	public void SetP1M()
	{
		ChangeScene.Instance.P1 = Character.Mage;
	}

	public void SetP2M()
	{
		ChangeScene.Instance.P2 = Character.Mage;
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(1))
		{
			Debug.Log(button.Length);
			foreach (var i in button)
			{
				i.interactable = true;
				ChangeScene.Instance.P1 = Character.None;
				ChangeScene.Instance.P2 = Character.None;
			}
		}
	}
}
