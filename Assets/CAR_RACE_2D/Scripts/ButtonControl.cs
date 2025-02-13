using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonControl : MonoBehaviour
{
    // Start is called before the first frame update
    public void HomeSceneLoad()
    {
        SceneManager.LoadScene("Main scene");
    }


}
