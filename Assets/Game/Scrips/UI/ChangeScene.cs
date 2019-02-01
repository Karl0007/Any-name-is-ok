using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumUtils;

public class ChangeScene : MonoBehaviour {
	public static ChangeScene Instance;

	private void Awake()
	{
		Instance = this;
	}

	public Character P1;
	public Character P2;
	public GameObject Result;

	// Use this for initialization
	void Start () {
		Debug.Log(ChangeScene2.Instance.Winer);
		Result.GetComponent<UnityEngine.UI.Text>().text = ChangeScene2.Instance.Winer;
	}
	
	// Update is called once per frame
	void Update () {

	}

}
