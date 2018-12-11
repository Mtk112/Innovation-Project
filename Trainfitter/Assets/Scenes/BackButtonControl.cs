using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonControl : MonoBehaviour {

    private int sceneIndex;

	//Checks which scene is active.
	void Start () {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
	}
	
	//If back button is pressed while on main menu, application will close. If on any other scene, application loads main menu.
	void Update () {
		if(sceneIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene(0);
        }
	}
}
