using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePresettation : MonoBehaviour
{
    private int _currentSlide = 0;
    private int _maxSlides = 0;
    private List<GameObject> _slides = new List<GameObject>();

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
