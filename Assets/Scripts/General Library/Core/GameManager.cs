/*
 * Handles the loading of scenes and the pausing, unpausing, and quitting of the application
 * Note that the tutorial level functionality was removed because it is too specific to Dreamwillow
 * @ Nikhil Ghosh '23, Max Perraut '20, Matthew Rader '19, Zena Abulhab '?, George Castle '22, Gino Bryja '20, Zhenyuan Zhang '?
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace WSoft.Core
{
    public class GameManager : MonoBehaviour
    {
        //Game managment parameters
            
        [Tooltip("Scene name of each level of the game")]
        public List<string> levelSequence;

        [Tooltip("The index for the level in levelSequqnce the game should restart at")]
        public int levelRestartIndex = 1;

        [SerializeField]
        private string mainMenuSceneName = "MainMenu";

        [Tooltip("Time before loading of new scene during transition.")]
        public float levelTransitionTime = 3f;

        [Tooltip("Length of Game Over sequence.")]
        public float gameOverTime = 2f;

        [Tooltip("Raised when the game is paused")]
        public UnityEvent gamePauseEvent;
        [Tooltip("Raised when the game is resumed")]
        public UnityEvent gameResumeEvent;

        public bool InTransition { get; private set; } = false;

        // A wrapper of all the events to group them in the editor
        [System.Serializable]
        public class GameManagerEvents
        {
            [System.Serializable]
            public class TimeEvent : UnityEvent<float> { }

            [Tooltip("Invoked at the start of a level transition. Passed the duration of the transition.")]
            public TimeEvent OnLevelTransition;

            [Tooltip("Invoked after a new level is loaded.")]
            public UnityEvent OnLevelLoaded;

            [Tooltip("Invoked when the game manager restarts the game")]
            public UnityEvent OnRestart;

            [Tooltip("Invoked when the game is over")]
            public UnityEvent OnGameOver;

            [Tooltip("Invoked when the game manager returns to the main menu")]
            public UnityEvent OnReturnToMainMenu;

            [Tooltip("Invoked when the game is about to quit (so saving can occur)")]
            public UnityEvent OnGameQuit;
        }

        [Tooltip("A struct that contains the UnityEvents")]
        public GameManagerEvents events;

        public static GameManagerEvents Events { get { return instance.events; } }

        private static GameManager instance;
        public static GameManager Instance { get { return instance; } }

        public static bool gamePaused { get; private set; } = false; // True if the game is paused, false otherwise.
        private static float currentTimeScale = 1f; // keep track of current time scale before pause. Edit: Rex Ma

        //True while in a transitional state
        public static bool inTransition { get; private set; } = false;

        // Sets the Singleton Instance up. Written by Nikhil Ghosh
        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            Screen.SetResolution(1920, 1080, true);
        }

        //////////////////////////////////////
        ///SCENE CONTROL

        /// <summary>
        /// Start the game (from main menu)
        /// </summary>
        public static void StartGame(string startSceneName)
        {
            instance.StartCoroutine(instance.DoSceneTransition(startSceneName));
        }

        /// <summary>
        /// Returns the game to the main menu
        /// </summary>
        public static void ReturnToMainMenu()
        {
            Events.OnReturnToMainMenu.Invoke();
            Events.OnRestart.Invoke();

            // load first level
            SceneManager.LoadScene(instance.mainMenuSceneName);

            if (gamePaused)
            {
                UnpauseGame();
            }
        }

        /// <summary>
        /// Preform a transition to a new scene
        /// </summary>
        /// <param name="sceneName">The scene to transition to</param>
        /// <returns></returns>
        private IEnumerator DoSceneTransition(string sceneName)
        {
            Debug.Log(string.Format("Starting transition to scene \"{0}\"", sceneName));

            inTransition = true;

            events.OnLevelTransition.Invoke(levelTransitionTime);

            PauseGame();

            yield return new WaitForSecondsRealtime(levelTransitionTime);

            SceneManager.LoadScene(sceneName);

            UnpauseGame();

            events.OnLevelLoaded.Invoke();

            inTransition = false;

            Debug.Log(string.Format("Completed transition to scene \"{0}\"", sceneName));
        }

        /// <summary>
        /// Public interface for loading a scene asynchronously.
        /// </summary>
        /// <param name="sceneName">The scene to be loaded</param>
        /// <param name="callback">The action to be taken when the scene is loaded</param>
        public static void LoadScene(string sceneName, System.Action callback = null)
        {
            if (!Instance.InTransition)
                Instance.StartCoroutine(Instance.DoLoadScene(sceneName, callback));
        }

        /// <summary>
        /// Public interface
        /// </summary>
        /// <param name="sceneName">The scene to be loaded</param>
        /// <param name="callback">The action to be taken when the scene is loaded</param>
        public static void LoadSceneAsync(string sceneName, System.Action callback = null)
        {
            if (!Instance.InTransition)
                Instance.StartCoroutine(Instance.DoLoadSceneAsync(sceneName, callback));
        }

        // Preform a transition to a new scene
        private IEnumerator DoLoadScene(string sceneName, System.Action callback)
        {
            Debug.Log(string.Format("Starting transition to scene \"{0}\"", sceneName));

            InTransition = true;

            SceneManager.LoadScene(sceneName);

            // make sure game is unpaused after loading
            Time.timeScale = 1f;
            gamePaused = false;

            events.OnLevelLoaded.Invoke();
            InTransition = false;

            Debug.Log(string.Format("Completed transition to scene \"{0}\"", sceneName));

            yield return null;
            callback?.Invoke();
        }

        private IEnumerator DoLoadSceneAsync(string sceneName, System.Action callback)
        {
            Debug.Log(string.Format("Starting transition to scene \"{0}\"", sceneName));

            InTransition = true;
            var currentSceneName = SceneManager.GetActiveScene().name;

            var load = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            yield return new WaitUntil(() => { return load.isDone; });

            events.OnLevelLoaded.Invoke();

            Debug.Log(string.Format("Completed transition to scene \"{0}\"", sceneName));

            callback?.Invoke();

            load = SceneManager.UnloadSceneAsync(currentSceneName);
            yield return new WaitUntil(() => { return load.isDone; });
            InTransition = false;
        }

        /// <summary>
        /// When the game is over, pause the game and invoke the events
        /// </summary>
        public static void GameOver()
        {
            PauseGame();
            Events.OnGameOver.Invoke();
        }

        /// <summary>
        /// Quits the game. Note that this only works on builds, not the editor versions.
        /// </summary>
        public static void QuitGame()
        {
            Events.OnGameQuit.Invoke();
            Application.Quit();
        }

        public static void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            UnpauseGame();
        }

        /// <summary>
        /// Pauses the game by setting the Time Scale to 0 and stops the timers in the Game Manager. Use this function to pause the game.
        /// Note that this function only shuts down things that require the Time class (e.g. anything using Time.deltaTime or Time.fixedDeltaTime).
        /// </summary>
        public static void PauseGame()
        {
            Debug.Log("----- PAUSE -----");
            instance.gamePauseEvent.Invoke();
            gamePaused = true;
            currentTimeScale = Time.timeScale;  // Edit: Rex Ma
            Time.timeScale = 0f;
        }

        /// <summary>
        /// Unpauses the game by setting the time scale to 1 and resuming the timers in the Game Manager. Use this function to unpause the game.
        /// </summary>
        public static void UnpauseGame()
        {
            Debug.Log("----- UNPAUSE -----");
            instance.gameResumeEvent.Invoke();
            gamePaused = false;
            Time.timeScale = currentTimeScale; // Edit: Rex Ma
        }

        /// <summary>
        /// Toggle the pausing of the game (Unpausing the game if it is paused, and pausing the game if it is unpaused)
        /// </summary>
        public static void TogglePause()
        {
            if (gamePaused)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
}

