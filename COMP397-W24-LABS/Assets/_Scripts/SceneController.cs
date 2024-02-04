using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void ChangeScene() {
        SceneManager.LoadScene("SampleScene");
    }

    public void ChangeScene(string SceneName) {
        SceneManager.LoadScene("SceneName");
    }
}
