using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public Text menuText;

	public string BEEGIN_String = "<color=green>>BEGIN<</color>\n\n-QUIT-";
	public string QUIT_String = "-BEGIN-\n\n<color=green>>QUIT<</color>";

	bool isBegin = true;
	public string SceneToLoad;


	public UIShakeEffect CamShake;

	// Start is called before the first frame update
	void Start()
	{

	}

	void Update()
    {
		if (Input.GetButtonDown("Up") || Input.GetButtonDown("Down"))
		{
			isBegin = !isBegin;
			CamShake.TriggerShake();
		}


		if (Input.GetButtonDown("Sword"))
		{
			if (isBegin)
			{
				SceneManager.LoadScene(SceneToLoad);
			}
			else
			{
				Application.Quit();
			}

			CamShake.TriggerShake();
		}


		if (isBegin)
		{
			menuText.text = BEEGIN_String;
		}
		else
		{
			menuText.text = QUIT_String;
		}
    }
}
