using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class map_selection : MonoBehaviour
{
    public void map1()
    {
        SceneManager.LoadScene("Map1");
    }

    public void map2()
    {
        SceneManager.LoadScene("Map2");
    }

    public void back()
    {
        SceneManager.LoadScene("main_menu");
    }
}