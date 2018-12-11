using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//Method for setting selected value from Tracktype Dropdown.
[RequireComponent(typeof(InputField))]
public class SiteName : MonoBehaviour
{

    const string Site = "testikohde";

    private InputField inputField;

    private void Awake()
    {
        inputField = GetComponent<InputField>();

        //selects the new dropdown value and saves it as a player prefence.
        inputField.onValueChanged.AddListener(new UnityAction<string>(index =>
        {
            PlayerPrefs.SetString("SiteName",inputField.text);
            PlayerPrefs.SetInt("SitePicId", 1);
            PlayerPrefs.Save();
            Debug.Log("SiteName : " + PlayerPrefs.GetString("SiteName"));
            Debug.Log("SitePicId : " + PlayerPrefs.GetInt("SitePicId"));
        }));
    }
    //Loads the currently selected traffictype option, or sets it as a default value if not previously set.
    void Start()
    {
        inputField.text = PlayerPrefs.GetString("SiteName");
    }

}
