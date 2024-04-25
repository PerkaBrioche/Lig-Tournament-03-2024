using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Manage : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start_but()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit_button()
    {
        Application.Quit();
    }
    public void Menu() { SceneManager.LoadScene("Menu"); }
    public void Tuto_but() { SceneManager.LoadScene("Tutone"); }
    public void Tuto_but2() { SceneManager.LoadScene("Tutwo"); }
    
    public void Tuto_but3() { SceneManager.LoadScene("Tuthree"); }
}
