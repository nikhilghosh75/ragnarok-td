using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * A button that controls the speed of gameplay
 * Written by Nikhil Ghosh
 * Modified by Amber Renton and Andrew Zhou
 */

public class SpeedButton : MonoBehaviour {
    [System.Serializable]
    public struct SpeedOption {
        public float speed;
        public Sprite sprite;
    }

    public List<SpeedOption> speedOptions;
    public Image image;
    int currentSpeedOption = 0;
    private Button button;
    private bool roundStarted = false;

    // Start is called before the first frame update
    void Start() {
        // image.color = Color.green;
        SetSpeedOption(speedOptions[0]);
        button = GetComponent<Button>();
        EnemySpawner.Get().events.OnRoundStart.AddListener(OnRoundStart);
        EnemySpawner.Get().events.OnRoundEnd.AddListener(OnRoundEnd);
    }

    public void OnClick() {
        if (roundStarted) {
            ToNext();
        }
        else {
            SetSpeedOption(speedOptions[currentSpeedOption]);
            image.color = Color.white;
            EnemySpawner.Get().StartRound();
        }
    }

    public void SetSpeedOption(SpeedOption speedOption) {
        if (Time.timeScale != 0) // make sure we don't break a pause
            Time.timeScale = speedOption.speed;
        image.sprite = speedOption.sprite;
    }

    public void ToNext() {
        currentSpeedOption = (currentSpeedOption + 1) % speedOptions.Count;
        SetSpeedOption(speedOptions[currentSpeedOption]);
    }

    void OnRoundStart() {
        roundStarted = true;
    }

    // On end of round, set the button back to single play arrow
    void OnRoundEnd() {
        roundStarted = false;
        // image.color = Color.green;
        SetSpeedOption(speedOptions[0]);
    }
}
