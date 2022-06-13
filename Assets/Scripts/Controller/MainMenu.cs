using System.Collections.Generic;
using System.IO;
using Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controller
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] public Button continueButton;
        [SerializeField] public GameObject menuCanvas;
        [SerializeField] public GameObject levelsCanvas;
        [SerializeField] public List<Button> levelButtonList;
        private bool _shouldLoad;
        private int _lastIndex;

        private void Start()
        {
            menuCanvas.SetActive(true);
            levelsCanvas.SetActive(false);
            if (File.Exists(Application.persistentDataPath + "/gamedata.dat"))
            {
                continueButton.interactable = true;
                _shouldLoad = true;
                _lastIndex = GameData.LoadGameData().lastLevelIndex;
            }
            else _lastIndex = 1;
        }

        public void LoadScene(int index)
        {
            if (index == -1)
            {
                DontDestroyOnLoad(new GameObject("NewGame"));
                SceneManager.LoadScene(1);
                return;
            }

            if (_shouldLoad && index == -2)
            {
                SceneManager.LoadScene(_lastIndex);
                return;
            }

            SceneManager.LoadScene(index);
        }

        public void LoadCustomScene(int index)
        {
            DontDestroyOnLoad(new GameObject("NewGame"));
            SceneManager.LoadScene(index);
        }

        public void LevelsCanvas()
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
            levelsCanvas.SetActive(!levelsCanvas.activeSelf);
            SetLevels();
        }

        private void SetLevels()
        {
            for (var i = 0; i < levelButtonList.Count; i++)
            {
                if (i < _lastIndex) levelButtonList[i].interactable = true;
                else levelButtonList[i].interactable = false;
            }
        }

        public void DeleteSaveFile()
        {
            continueButton.interactable = false;
            SetLevels();
            if (File.Exists(Application.persistentDataPath + "/gamedata.dat"))
            {
                File.Delete(Application.persistentDataPath + "/gamedata.dat");
                _lastIndex = 1;
            }
        }

        public void ExitApp()
        {
            Application.Quit();
        }
    }
}