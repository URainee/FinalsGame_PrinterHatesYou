using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PrinterScript : MonoBehaviour, IDamageable
{
    // *
    // Variables
    // *

    [Header("References")]
    private Animator _anim;

    [Header("Printing")]
    [SerializeField] private GameObject _printedPaperPrefab;
    [SerializeField] private Transform _printedPaperSpawnPoint;

    [SerializeField] private int _maxProgress;
    [SerializeField] private int _currentProgress;
    [SerializeField] private int _progressPerSecond;
    [SerializeField] private float _progressDelay;
    [SerializeField] private float _progressInterval;
    private float _nextProgressPoint;

    [Header("Health")]
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _regenPerSecond = 1;
    [SerializeField] private float _regenDelay = 1f;
    [SerializeField] private float _regenInterval = 1f;
    private float _lastDamagePoint;
    private float _nextRegenPoint;
    [SerializeField] private int _printerHealthPhase = 1;

    [Header("Connection")]

    [Header("Jamming")]
    [SerializeField] private int _healthToUnjam;

    [Header("Booleans")]

    // Checks
    [SerializeField] private bool _isBroken;
    [SerializeField] private bool _isLoaded; // checks to see whether there is paper in the printer
    [SerializeField] private bool _isConnected;

    [SerializeField] private bool _canPrint;
    [SerializeField] private bool _isPrinting;
    [SerializeField] private bool _isJammed;

    // *
    // MonoBehaviour Functions
    // *

    private void Start()
    {
        SetPhase(1);

        _anim = GetComponent<Animator>();

        _currentHealth = _maxHealth;

        _isBroken = false;
        _isLoaded = false;
        _isConnected = false;
        _isPrinting = false;
        _isJammed = false;

        _canPrint = false;

        _nextProgressPoint = Time.time + _progressDelay;

        _nextRegenPoint = Time.time + _regenInterval;

        UIManager.Instance.PrintProgress(_currentProgress, _maxProgress);

        UIManager.Instance.PassTest(1);

    }

    private void Update()
    {
        RegenerateHealthChecks();

        _canPrint = PrinterChecks();

        if (_canPrint && _isPrinting)
        {
            Print();
        }
    }

    // *
    // Printer Checks
    // Or the checks done before the printer is allowed to PRINT 
    // *

    #region Printer Checks

    private bool PrinterChecks()
    {    
        if (_isBroken)
        {
            Debug.Log("Printer is Broken");
            return false;
        }

        if (!_isConnected)
        {
            Debug.Log("Printer is not Connected");
            return false;
        }

        if (!_isLoaded)
        {
            Debug.Log("Printer is not Loaded");
            return false;
        }

        if (_isJammed)
        {
            Debug.Log("Printer is jammed");
            return false;
        }

        Debug.Log("Printer can Print");
        return true;
    }

    #endregion

    // *
    // Print
    // *

    public void StartPrinter()
    {
        if (_canPrint)
        {
            _isPrinting = true;
            UIManager.Instance.Print();
        }
    }

    private void Print()
    {
        while (_currentProgress < _maxProgress)
        {
            // While the currentProgress is less than maxProgress
            // If Time.time hasn't passed the _nextProgressPoint then it returns
            // But if it does then it increments the progress bar and sets a new progress point

            if (Time.time < _nextProgressPoint)
            {
                return;
            }

            if(_currentProgress <= _maxProgress - _progressInterval || _currentProgress >= 0 + _progressInterval)
            {
                EffectRoll();
            }

            _currentProgress += _progressPerSecond;

            UIManager.Instance.PrintProgress(_currentProgress, _maxProgress);

            _nextProgressPoint = Time.time + _progressDelay;
        }

        StopPrinter();

        SpawnPrintedPaper(); 

        _anim.SetBool("isLoaded", false);

        _isLoaded = false;

        _currentProgress = 0;

        UIManager.Instance.PrintProgress(_currentProgress, _maxProgress);
    }

    private void StopPrinter()
    {
        Debug.Log("Stopped Printing");
        _isPrinting = false;
    }

    private void SpawnPrintedPaper()
    {
        Instantiate(_printedPaperPrefab, _printedPaperSpawnPoint.position, _printedPaperSpawnPoint.rotation);
    }

    // *
    // Logic
    // *

    #region Health/Damage

    public void TakeDamage(int damageAmount)
    {
        if (_isBroken) { return; }

        _currentHealth = Mathf.Max(0, _currentHealth - damageAmount);

        _lastDamagePoint = Time.time;

        // Refactor Jam Damage Code Later

        if (_isJammed)
        {
            DamageToUnjam(damageAmount);
        }

        if (_currentHealth <= 0) 
        {
            if (_printerHealthPhase <= 2)
            {
                //regenerates health and increments the phase
                _printerHealthPhase++;

                _currentHealth = _maxHealth;

                SetPhase(_printerHealthPhase);
            
                return;
            }

            // if printer is in final phase...
            DestroyPrinter();
        }
    }

    private void RegenerateHealthChecks()
    {
        if (_isBroken || _currentHealth >= _maxHealth) { return; }

        if (Time.time < _lastDamagePoint + _regenDelay) { return; }

        if (Time.time < _nextRegenPoint) { return; }

        RegenerateHealth();
    }

    private void RegenerateHealth()
    {
        _currentHealth = Mathf.Min(_maxHealth, _currentHealth + _regenPerSecond);

        _nextRegenPoint = Time.time + _regenInterval;
    }

    private void DestroyPrinter()
    {
        StopPrinter();

        _isBroken = true;

        if(_isBroken == true)
        {
            SetPhase(4);

            GameManager.Instance.PlayEnding(3);
        }
    }

    #endregion

    #region Connection

    public void ConnectPrinter()
    {
        if (!_isBroken)
        {
            _isConnected = true;
            UIManager.Instance.PrinterConnected();
        }
    }

    private void DisconnectPrinter()
    {
        if (!_isBroken)
        {
            _isConnected = false;
            UIManager.Instance.PrinterDisconnected();
        }
    }

    #endregion

    #region Jamming
    private void JamPrinter()
    {
        if (_isPrinting)
        {
            _isJammed = true;

            _healthToUnjam = Random.Range(2, 6);
            // Sets the health needed to pass to unjam the printer...

            UIManager.Instance.PrinterJam();
            //Calls UI Manager's PrinterJam method to dispatch OnPrinterJam action
        }
    }

    private void DamageToUnjam(int damageAmount)
    {
        _healthToUnjam -= damageAmount;

        if (_healthToUnjam <= 0)
        {
            UnjamPrinter();
        }
    }

    private void UnjamPrinter()
    {
        if (_isJammed)
        {
            _isJammed = false;
            UIManager.Instance.PrinterUnjam();
        }
    }

    #endregion 

    #region Probability Checks

    private void EffectRoll()
    {
            int randDisconnectChance = Random.Range(1, 10);

            if (randDisconnectChance == 1)
            {
                Debug.Log("Printer Disconnected");
                DisconnectPrinter();
                StopPrinter();
                return;
            }

            int randJamChance = Random.Range(1, 10);

            if (randJamChance == 1)
            {
                Debug.Log("Printer Jam");
                JamPrinter();
                StopPrinter();
                return;
            }
    }
    #endregion

    #region Animation

    private void SetPhase(int newPhase)
    {
        switch (newPhase)
        {
            case 1:
                break;
            case 2:
                 _anim.SetTrigger("toPhaseTwo");
                break;
            case 3:
                _anim.SetTrigger("toPhaseThree");
                break;
            case 4:
                _anim.SetTrigger("isBroken");
                break;
            default:
                Debug.Log("No such phase exists...!");
                break;
        }
    }

    private void ShakePrinter()
    {
        //*
        //to be called when printer is hit
        //*

        Transform originalPosition = this.transform;
    }

    #endregion

    #region Collisions and Triggers

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EmptyPaper>() != null)
        {
            EmptyPaper emptyPaper = collision.gameObject.GetComponent<EmptyPaper>();

            if (emptyPaper.IsDragging == false && _isLoaded == false)
            {
                emptyPaper.TakeDamage(1);

                _anim.SetBool("isLoaded", true);

                _isLoaded = true;
            }
        }
    }
    #endregion 
}
