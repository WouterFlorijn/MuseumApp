﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public float loadTime;

    protected string nextScene;
    protected float timer, scale;
    protected Material barBGMat, barSelectedMat, barInactiveMat;
    protected GameObject progressBar;
    protected TextMesh text;

    private CardboardHead head;

	void Start()
    {
        Load();
	}

    public virtual void Load()
    {
        head = Camera.main.GetComponent<StereoController>().Head;
        timer = 0;
        barBGMat = Resources.Load("Materials/BarBG") as Material;
        barSelectedMat = Resources.Load("Materials/BarSelected") as Material;
        barInactiveMat = Resources.Load("Materials/BarInactive") as Material;
        progressBar = transform.parent.FindChild("Bar Progress Pivot").gameObject;
        text = transform.parent.FindChild("Text").GetComponent<TextMesh>();
    }
	
	void Update()
    {
        RaycastHit hit;
        bool isLookedAt = GetComponent<Collider>().Raycast(head.Gaze, out hit, Mathf.Infinity);
        if (isLookedAt && IsActive())
        {
            this.GetComponent<Renderer>().material = barSelectedMat;
            scale = (5f / loadTime) * timer;
            Debug.Log(scale + " " + timer);
            progressBar.transform.localScale = new Vector3(scale, 0.01f, 1);
            timer += Time.deltaTime;

            if (timer > loadTime)
            {
                SceneManager.LoadScene(nextScene);
                OnNextScene();
            }
        }
        else
        {
            if (IsActive())
            {
                GetComponent<Renderer>().material = barBGMat;
                text.color = Color.white;
            }
            else
            {
                GetComponent<Renderer>().material = barInactiveMat;
                text.color = new Color(0, 0, 0, 0);
            }
            progressBar.transform.localScale = new Vector3(0, 0.01f, 1);
            timer = 0;
        }
	}

    public virtual void OnNextScene() { }

    public virtual bool IsActive()
    {
        return true;
    }

    void OnGUI()
    {

    }
}