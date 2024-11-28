using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeSlider : MonoBehaviour
{
    [SerializeField] private Slider _widthSlider;
    [SerializeField] private Slider _depthSlider;
    [SerializeField] private MazeGenerator _sliderValue;

    public void WidthValueSlider()
    {
        _sliderValue.mazeWidth = (int)_widthSlider.value;
        
    }

    public void DepthValueSlider()
    {
        _sliderValue.mazeDepth = (int)_depthSlider.value;
    }

}
