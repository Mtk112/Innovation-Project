using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Method for changing scenes: Gets integer that specifies which scene to load.

public class MenuScripts : MonoBehaviour {

    public void ChangeScene(int sceneToShow)
    {
        SceneManager.LoadScene(sceneToShow);   
    }

}
