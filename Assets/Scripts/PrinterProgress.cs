using UnityEngine;
using UnityEngine.UI;

public class PrinterProgress : MonoBehaviour
{
    [Header("Progress Bar")] // move to different script
    [SerializeField] private bool _isActive;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private int _maxProgress = 60;
    [SerializeField] private int _currentProgress = 0;
    [SerializeField] private int _progressPerSecond = 2;

    private Coroutine _progressCoroutine;

    [SerializeField] private bool _isStopped;
    private int _randChance;
    private int _randDurability; // either 2, 4 or 6

    // maybe make the printer taking damage and whether it actually moves up the progress random...? 

    //
}
