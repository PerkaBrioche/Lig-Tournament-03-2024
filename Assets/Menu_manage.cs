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
}
