using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    Rigidbody rigid;
    public float factor;
    private float startTime;
    public float maxDis = 5;
    public GameObject Stage;
    private GameObject currentStage;
    private GameObject nextGameObject;
    public Transform _cameras;
    private Vector3 _cameraReducePlayer;
    public Text text;
    private int score;
    public GameObject[] prefabs;
    public Text addScore;
    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody>();
        currentStage = Stage;
        _cameraReducePlayer = _cameras.position - transform.position;
        addScore.enabled = false;
        CreatStage();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            startTime = Time.time;
        }
        if (Input.GetMouseButtonUp(0))
        {
            var delTime = Time.time - startTime;
            //Debug.Log(delTime);
            OnJump(delTime,nextGameObject);

        }
	}
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Plane")
        {
            SceneManager.LoadScene("Jump");
        }
        else if (other.gameObject.name == "Cube(Clone)" && other.gameObject != currentStage)
        {
            addScore.enabled = true;
            addScore.text = "+1";
            currentStage = other.gameObject;
            rigid.velocity = Vector3.zero;
            score++;
            text.text = score.ToString();
            CreatStage();
            StartCoroutine(UIAdd());
        }
        else if (other.gameObject.name == "Cube1(Clone)" && other.gameObject != currentStage)
        {
            addScore.enabled = true;
            addScore.text = "+2";
            currentStage = other.gameObject;
            rigid.velocity = Vector3.zero;
            score+=2;
            text.text = score.ToString();
            CreatStage();
            StartCoroutine(UIAdd());
        }
        else if (other.gameObject.name == "Cube2(Clone)" && other.gameObject != currentStage)
        {
            addScore.enabled = true;
            addScore.text = "+3";
            currentStage = other.gameObject;
            rigid.velocity = Vector3.zero;
            score+=3;
            text.text = score.ToString();
            CreatStage();
            StartCoroutine(UIAdd());
        }
        OnCameraMove();
    }

    private void OnCameraMove()
    {
        _cameras.DOMove(transform.position + _cameraReducePlayer, 3);
        //_cameras.position =;
    }

    void OnJump(float force,GameObject next)
    {
        rigid.AddForce(new Vector3(1, 1, 0) * force * factor, ForceMode.Impulse);
        //rigid.AddForce(((next.transform.position - transform.position).normalized) * force * factor, ForceMode.Impulse);
    }
    void CreatStage()
    {

        var stage = Instantiate(prefabs[UnityEngine.Random.Range(0,prefabs.Length)]);
        nextGameObject = stage;
        stage.transform.position = currentStage.transform.position + new Vector3(UnityEngine.Random.Range(1.5f,maxDis),0,0);
        //stage.name = "Cube";
    }
    IEnumerator UIAdd()
    {
        yield return new WaitForSeconds(1f);
        addScore.enabled = false;

    }
}
