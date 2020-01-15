using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Dropdown m_SceneDropDown = null;

    string m_SelectedSceneName;

    // Start is called before the first frame update
    void Start()
    {
        List<string> SceneNames = new List<string>();
        EditorBuildSettingsScene[] GameScenes = EditorBuildSettings.scenes;
        string name = "";
        for (int i = 1; i < GameScenes.Length; i++)
        {
            name = GameScenes[i].path.Substring(14);
            SceneNames.Add(name.Substring(0, name.Length - 6));
        }
        if (SceneNames.Count > 0)
        {
            m_SceneDropDown.AddOptions(SceneNames);
            m_SelectedSceneName = m_SceneDropDown.options[m_SceneDropDown.value].text;
        }
    }

    public void SelectScene()
    {
        m_SelectedSceneName = m_SceneDropDown.options[m_SceneDropDown.value].text;
    }

    public void StartScene()
    {
        SceneManager.LoadScene(m_SelectedSceneName);
    }
}
