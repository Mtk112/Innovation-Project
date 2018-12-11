using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//Method for setting selected value from Tracktype Dropdown.
[RequireComponent(typeof(Dropdown))]
public class Tracktype : MonoBehaviour {

    const string TrackOption = "TrackValue";

    private Dropdown dropdown;

    private void Awake()
    {
        dropdown = GetComponent<Dropdown>();

        //selects the new dropdown value and saves it as a player prefence.
        dropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(TrackOption, dropdown.value);
            PlayerPrefs.Save();
            Debug.Log("TrackType : " + PlayerPrefs.GetInt(TrackOption));
        }));
    }
    //Loads the currently selected tracktype option, or sets it as a default value if not previously set.
    void Start()
    {
        dropdown.value = PlayerPrefs.GetInt(TrackOption, 0);
    }
}
