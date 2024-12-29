using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSlidesPresentation : MonoBehaviour
{
    private int _currentSlide = 0;
    private int _maxSlides = 0;
    [SerializeField] private List<GameObject> _slides = new List<GameObject>();

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
