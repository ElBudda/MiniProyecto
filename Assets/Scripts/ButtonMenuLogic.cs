using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ButtonMenuLogic : MonoBehaviour
{

    public void inicio()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    public void salir()
    {
        Debug.Log("cerrando juego");
        Application.Quit();
    }
}
