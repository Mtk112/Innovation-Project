using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//Method for setting selected value from Tracktype Dropdown.
[RequireComponent(typeof(Dropdown))]
public class Traffictype : MonoBehaviour {

    const string TrafficOption = "TrafficValue";

    private Dropdown dropdown;

    private void Awake()
    {
        dropdown = GetComponent<Dropdown>();

        //selects the new dropdown value and saves it as a player prefence.
        dropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(TrafficOption, dropdown.value);
            PlayerPrefs.Save();
            Debug.Log("TrafficType : "+ PlayerPrefs.GetInt(TrafficOption));
        }));
    }
    //Loads the currently selected traffictype option, or sets it as a default value if not previously set.
    void Start ()
    {
        dropdown.value = PlayerPrefs.GetInt(TrafficOption, 0);
	}
	
}
