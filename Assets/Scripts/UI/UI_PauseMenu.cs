using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ScottBarley.IGB200.V1 
{
    public class UI_PauseMenu : MonoBehaviour
    {
        bool _isGamePaused;
        [SerializeField] Transform _pauseMenu;

        void Start()
        {
            _isGamePaused = false;
            _pauseMenu.gameObject.SetActive(false);
        }

        public void fn_ResumeGame()
        {
            _pauseMenu.gameObject.SetActive(false);
            _isGamePaused = false;
            Time.timeScale = 1f;

            DisableMouseCursor();         
        }

        public void fn_PauseGame()
        {
            _pauseMenu.gameObject.SetActive(true);
            _isGamePaused = true;
            Time.timeScale = 0f;

            EnableMouseCursor();
        }

        public void fn_TogglePauseMenu()
        {
            if (_isGamePaused)
            {
                fn_ResumeGame();
            }
            else
            {
                fn_PauseGame();
            }
        }

        private void EnableMouseCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        private void DisableMouseCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }


    }
}