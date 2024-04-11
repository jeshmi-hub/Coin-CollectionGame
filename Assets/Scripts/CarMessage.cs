using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarMessage : MonoBehaviour
{
    public GameObject car; // Reference to the car object
    public GameObject messageText; // Reference to the text component
    private bool hasCrashed = false;
    public AudioSource carCrashSound;


    void Update()
    {
        // Check if the car has crashed (rolled)
        if (!hasCrashed && IsCarRolled())
        {
            hasCrashed = true;
            DisplayCrashMessage();
            carCrashSound.Play();
        }

    }

    private bool IsCarRolled()
    {
        // Check if the car is upside down (rolled)
        return car.transform.up.y < 0.1f;
    }

    private void DisplayCrashMessage()
    {
        // Set the text to display "Car Crashed"
        messageText.GetComponent<TextMeshProUGUI>().text = "Car Crashed!!";
    }
}