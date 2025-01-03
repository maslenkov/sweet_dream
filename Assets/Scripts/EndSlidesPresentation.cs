using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndSlidesPresentation : MonoBehaviour
{
    private int _currentSlide = 0;
    private int _maxSlides = 0;
    [SerializeField] private List<GameObject> _slides = new List<GameObject>();

    private void Awake()
    {
        if (SoundManager.instance == null)
        {
            // Instantiate(Resources.Load("SoundManager"));
        } else {
            SoundManager.instance.musicSource.clip = SoundManager.instance._musicClip1;
            SoundManager.instance.musicSource.Play();
        }
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
    }

    public void NextSlide()
    {
        if (_currentSlide < _maxSlides - 1)
        {
            _slides[_currentSlide].SetActive(false);
            _currentSlide++;
            _slides[_currentSlide].SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("CreditScene");
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
}
