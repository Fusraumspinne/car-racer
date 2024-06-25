using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class Hauptmenu : MonoBehaviour
{
    public GameObject Grün;
    public GameObject Rot;

    public bool isMobileSteering;

    public void Start()
    {
        PlayerPrefs.SetInt("mobileSteering", 0);
        PlayerPrefs.Save();
    }

    public void ToggleMobileSteering()
    {
        if (isMobileSteering)
        {
            isMobileSteering = false;
            Rot.SetActive(true);
            Grün.SetActive(false);

            PlayerPrefs.SetInt("mobileSteering", 0);
            PlayerPrefs.Save();
        }
        else
        {
            isMobileSteering = true;
            Rot.SetActive(false);
            Grün.SetActive(true);

            PlayerPrefs.SetInt("mobileSteering", 1);
            PlayerPrefs.Save();
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("World");
    }
}
