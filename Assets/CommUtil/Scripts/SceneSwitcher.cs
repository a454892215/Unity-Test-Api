using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CommUtil.Scripts
{
    public class SceneSwitcher : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                ToPreScene();
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                ToNextScene();
            }
        }

        //去指定场景
        public static void SwitchScene(String sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        //去下一个场景
        public void ToNextScene()
        {
            String currentSceneName = SceneManager.GetActiveScene().name;
            string num = currentSceneName.Substring(currentSceneName.Length - 1);
            int targetNum = int.Parse(num) +1;
            SceneManager.LoadScene("Scene" + targetNum);
        }

        //去上一个场景
        public void ToPreScene()
        {
            String currentSceneName = SceneManager.GetActiveScene().name;
            string num = currentSceneName.Substring(currentSceneName.Length - 1);
            int targetNum = int.Parse(num) - 1;
            SceneManager.LoadScene("Scene" + targetNum);
        }

        //退出游戏
        public void ExitGame()
        {
           Application.Quit();
        }
    }
}
