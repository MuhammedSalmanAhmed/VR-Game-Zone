using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour
{
    public GameManager gameManager;

	void OnTriggerEnter ()
	{
		gameManager.startLevel();
	}
}
