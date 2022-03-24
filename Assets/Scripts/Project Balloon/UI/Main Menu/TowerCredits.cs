/*
 * A framework for a credits system
 * Written by Angela Salacata '?, Natasha Badami '20, George Castle '22, Nikhil Ghosh '24
 * Modified for Project Tower by Andrew Zhou '22
 *
 * ANDREW NOTE: This function needs a tad bit of reworking and cleaning up for future use, remind me to get on that later
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace WSoft.UI
{
    public class TowerCredits : MonoBehaviour
    {
        public GameObject titlePrefab;
        public GameObject namePrefab;
        public GameObject endPrefab;

        [Tooltip("This should be the relative size of both title and name prefabs - along with any spacing added on by the Vertical Layout Group.\n\nANDREW NOTE: This might disappear later, when I find a way to calculate this automatically on script start.")]
        public int spacing = 100;
        public UnityEvent OnComplete;

        private bool scrolling = false;

        private float goalPosition = 0f;
        private int count = 0;

        private float startTime;
        private float finishTime;

        private RectTransform rectTransform;

        [System.Serializable]
        public struct Segment
        {
            public string teamName;
            public List<string> members;
        }

        public List<Segment> segments;

        // Start is called before the first frame update
        void Start()
        {
            foreach (Segment segment in segments)
            {
                createPair(segment.teamName, segment.members);
            }

            createNode(endPrefab);

            rectTransform = GetComponent<RectTransform>();
            scrolling = false;
            // might want to actually start scrolling here?
            StartScroll();
            // ExampleCoroutine();
        }

        void Update()
        {
            if (scrolling)
            {
                if (rectTransform.anchoredPosition.y < goalPosition)
                {
                    float t = (Time.unscaledTime - startTime) / (float)count;
                    if (t > 0)
                    {

                        rectTransform.anchoredPosition = Vector2.Lerp(new Vector2(0, 0), new Vector2(0, goalPosition), t);
                    }
                }
                else if (finishTime == 0)
                {
                    finishTime = Time.unscaledTime;
                }

                if (finishTime > 0 && Time.unscaledTime - finishTime > 2f)
                {
                    OnComplete.Invoke();
                }
            }
        }


        void OnEnable()
        {
            if (count > 0)
            {
                StartScroll();
            }
        }
        public void StartScroll()
        {
            goalPosition = count * spacing;
            Debug.Log(goalPosition);
            startTime = Time.unscaledTime;
            scrolling = true;
        }

        public void StopScroll()
        {
            scrolling = false;
            startTime = 0;
            finishTime = 0;
            rectTransform.anchoredPosition = new Vector2(0, 0);
        }

        void createPair(string title, List<string> names)
        {
            createNode(titlePrefab).GetComponent<TMPro.TextMeshProUGUI>().text = title;
            foreach (string name in names)
            {
                createNode(namePrefab).GetComponent<TMPro.TextMeshProUGUI>().text = name;
                count++;
            }
        }

        GameObject createNode(GameObject prefab)
        {
            GameObject newObj = Instantiate(prefab);
            newObj.transform.parent = transform;
            return newObj;
        }

        public void ReturnToMainMenu()
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }
}