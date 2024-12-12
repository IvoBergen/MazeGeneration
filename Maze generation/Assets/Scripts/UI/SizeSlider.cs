using UnityEngine;
using UnityEngine.UI;

public class SizeSlider : MonoBehaviour
{
    [SerializeField] private Slider _widthSlider;
    [SerializeField] private Slider _depthSlider;
    [SerializeField] private MazeGenerator _sliderValue;

    //In play mode sets the width of the maze
    public void WidthValueSlider()
    {
        _sliderValue.mazeWidth = (int)_widthSlider.value;
    }

    //In play mode sets the Depth of the maze
    public void DepthValueSlider()
    {
        _sliderValue.mazeDepth = (int)_depthSlider.value;
    }

}
