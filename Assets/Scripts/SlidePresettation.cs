using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlidePresettation : MonoBehaviour
{
    private int _currentSlide = 0;
    private int _maxSlides = 0;

    private Button btnEasy;
    private Button btnMedium;
    private Button btnHard;
    private Button btnNext;

    [SerializeField] private List<GameObject> _slides = new List<GameObject>();

    private void Awake()
    {
        btnEasy = GameObject.Find("btnEasy").GetComponent<Button>();
        btnMedium = GameObject.Find("btnMedium").GetComponent<Button>();
        btnHard = GameObject.Find("btnHard").GetComponent<Button>();
        btnNext = GameObject.Find("btnNext").GetComponent<Button>();
    }

    private void Start()
    {
        foreach (Transform child in transform)
        {
            _slides.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }

        _maxSlides = _slides.Count;
        _slides[_currentSlide].SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextSlide();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousSlide();
        }

        if (_currentSlide == _maxSlides - 1)
        {
            btnEasy.gameObject.SetActive(true);
            btnMedium.gameObject.SetActive(true);
            btnHard.gameObject.SetActive(true);
            btnNext.gameObject.SetActive(false);
        }
        else
        {
            btnEasy.gameObject.SetActive(false);
            btnMedium.gameObject.SetActive(false);
            btnHard.gameObject.SetActive(false);
            btnNext.gameObject.SetActive(true);
        }
    }

    public void NextSlide()
    {
        if (_currentSlide < _maxSlides - 1)
        {
            _slides[_currentSlide].SetActive(false);
            _currentSlide++;
            _slides[_currentSlide].SetActive(true);
        }
    }

    public void PreviousSlide()
    {
        if (_currentSlide > 0)
        {
            _slides[_currentSlide].SetActive(false);
            _currentSlide--;
            _slides[_currentSlide].SetActive(true);
        }
    }

    public void LoadEasy()
    {
        PlayerPrefs.SetInt("EnemyCount", 1);
        PlayerPrefs.SetFloat("EnemyAwakeTime", 2.5f);
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadMedium()
    {
        PlayerPrefs.SetInt("EnemyCount", 2);
        PlayerPrefs.SetFloat("EnemyAwakeTime", 1.5f);
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadHard()
    {
        PlayerPrefs.SetInt("EnemyCount", 3);
        PlayerPrefs.SetFloat("EnemyAwakeTime", 1.0f);
        SceneManager.LoadScene("SampleScene");
    }
}
