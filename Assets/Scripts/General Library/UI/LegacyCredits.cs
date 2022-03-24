/*
 * A framework for a credits system
 * Written by Angela Salacata '?, Natasha Badami '20, George Castle '22
 * NOT APPROVED
 * 
 * This code may have aspects/assumptions that were specific to its original project. 
 * For instance, you should modify the dictionary with the names of the team members for your project
 * I would recommend using it as a reference (when implementing a new script), rather than purely copying it and pasting it
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Legacy
{
    public class Credits : MonoBehaviour
    {
        public GameObject titlePrefab;
        public GameObject namePrefab;
        public GameObject endPrefab;

        public UnityEvent OnComplete;

        public bool scrolling = false;

        private float goalPosition = 0f;
        private int count = 0;

        private float startTime;
        private float finishTime;

        private RectTransform rectTransform;

        private Dictionary<string, List<string>> credits = new Dictionary<string, List<string>> {
        { "Producer", new List<string> {
            "Nico Williams"
        }},
        { "Design Director", new List<string> {
            "Matt Rader",
            }},
        { "Art Director", new List<string> {
            "George Castle"
            }},
        { "Programming Director", new List<string> {
            "Natasha Badami",
            }},
        { "Audio Director", new List<string> {
            "Logan Hughes",
            }},
        { "Pod Leads", new List<string> {
            "Naveen Sabharwal",
            "David Sohn",
            }},
        { "Programming", new List<string> {
            "Albert Tsui",
            "Alex Song",
            "Angela Salacata",
            "Cameron Foss",
            "Christine Chen",
            "David Sohn",
            "Ebony Matthews",
            "Evan Brisita",
            "George Castle",
            "Jacob Smellie",
            "Natasha Badami",
            "Tim Cho",
            "Vanessa Su",
            "William Bostick",
            "Yan Sun",
            "Yuhong Chen",
            "Ziang Wang"
            }},
        { "Design", new List<string> {
            "Ahmad Faiyaz",
            "Albert Tsui",
            "Ebony Matthews",
            "George Castle",
            "Matthew Rader",
            "Rob Swor",
            "Yuhong Chen"
            }},
        { "Art", new List<string> {
            "Amber Renton",
            "David Sohn",
            "Deniz Acikbas",
            "Dilan Huang",
            "George Castle",
            "Kaavya Ramachandhran",
            "Lucy Zhang",
            "Naveen Sabharwal",
            "Trevor Harkness"
            }},
        { "Audio", new List<string> {
            "Ahmad Faiyaz",
            "Alex O'Brien",
            "Emily Sulkey",
            "Eugene Yoon",
            "Landon Young",
            "Logan Hughes"
            }}
    };

        // 10??

        // Start is called before the first frame update
        void Start()
        {

            foreach (KeyValuePair<string, List<string>> entry in credits)
            {
                createPair(entry.Key, entry.Value);
            }

            createNode(endPrefab);

            rectTransform = GetComponent<RectTransform>();
            scrolling = false;
            // might want to actually start scrolling here?
            //StartScroll();
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
            goalPosition = count * 90;
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