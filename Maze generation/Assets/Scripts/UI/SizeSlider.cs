using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class SizeSlider : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private Slider _widthSlider;
    [SerializeField] private Slider _depthSlider;
    
    [Header("Script Specific file")]
    [SerializeField] private MazeGenerator _sliderValue;
    
    /// <summary>
    /// In play mode sets the width of the maze
    /// </summary>
    public void WidthValueSlider()
    {
        _sliderValue.mazeWidth = (int)_widthSlider.value;
    }
    
    /// <summary>
    /// In play mode sets the Depth of the maze
    /// </summary>
    public void DepthValueSlider()
    {
        _sliderValue.mazeDepth = (int)_depthSlider.value;
    }

}
