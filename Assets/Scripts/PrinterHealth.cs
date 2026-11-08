using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PrinterHealth : MonoBehaviour, IDamageable
{
    [Header("Progress Bar")]
    [SerializeField] private bool _isPrinting;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private int _maxProgress = 60;
    [SerializeField] private int _currentProgress = 0;
    [SerializeField] private int _progressPerSecond = 2;
    private Coroutine _progressCoroutine;
    [SerializeField] private bool _isJammed;
    private int _randChance;
    private int _randDurability; // either 2, 4 or 6

    // 
    
    [Header("Health")]
    [SerializeField] private Slider healthbar;
    [SerializeField] private int maxHealth = 100;

    [Header("Regeneration")]
    [SerializeField] private int regenPerSecond = 1;
    [SerializeField] private float delay = 1f;
    [SerializeField] private float interval = 1f;

    private int health;

    private float lastDamagePoint;
    private float nextRegenPoint;

    private bool broken;


    private void Start()
    {
        _isPrinting = false;
        _isJammed = false;

        _progressBar.maxValue = _maxProgress;
        _progressBar.value = _currentProgress;

        UpdateProgress(_isPrinting); 

        //

        health = maxHealth;

        healthbar.minValue = 0;
        healthbar.maxValue = maxHealth;

        UpdateMeter();

        nextRegenPoint = Time.time + interval;
    }


    public void TakeDamage(int amount)
    {
        if (broken)
        {
            return;
        }

        if (_isJammed)
        {
            // *
            // Runs only is the printer is jammed...
            //* 

            DurabilityDamage(amount);
        }

        health = Mathf.Max(0, health - amount);
        lastDamagePoint = Time.time;

        if (_isPrinting)
        {
            // *
            // Runs only while printer is actively printing...
            // *
            ProgressThroughHit();
        }
        

        UpdateMeter();

        if (health <= 0)
        {
            BreakingPoint();
        }
    }


    private void Update()
    {
        if (broken || health >= maxHealth)
        {
            return;
        }

        if (Time.time < lastDamagePoint + delay)
        {
            return;
        }

        if (Time.time < nextRegenPoint)
        {
            return;
        }

        RegenHealth();
    }

    private void RegenHealth()
    {
        health = Mathf.Min(maxHealth, health + regenPerSecond);

        UpdateMeter();

        nextRegenPoint = Time.time + interval;
    }

    private void UpdateMeter()
    {
        healthbar.value = health;
    }

    private void BreakingPoint()
    {
        broken = true;
        Debug.Log("Printer broke!");
    }

    private void DurabilityDamage(int amount)
    {
        // *
        // Function added by Jordan on November 8, 2026
        // * 
        _randDurability -= amount;
        
        if (_randDurability <= 0)
        {
            Debug.Log("Printer Fixed");
            _isJammed = false;
        }
    }

    #region Progress Bar 

    private void ProgressThroughHit() 
    {
        // *
        // Only work if printer is actively printing
        // * 
        
        int RandProgressChance = Random.Range(1, 4);
        if(RandProgressChance == 1)
        {
            _currentProgress += 2;
            Debug.Log("Randomly Gets Chance!");
        }
    }
    
    public void SetPrinterBool()
    {
        // *
        // Note: Assigned to button
        // * 

        _isPrinting = !_isPrinting;
        UpdateProgress(_isPrinting);
    }

    private void UpdateProgress(bool isPrinting)
    {
        if (_isJammed)
        {
            Debug.Log("Printer unable to print...it is broken");
            _isPrinting = false;
            return;
        }

        if (isPrinting)
        {
            // *
            // Maybe what's wrong is that when the print button is pressed again...
            // It resets the coroutine...
            // And that's what bugs! 
            // So I need code in that it doesn't reset the coroutine when the Print button 
            // is pressed again
            // This glitch happens because the coroutine isn't supposed to be turned on
            // By player input...
            // * 

            if (_progressCoroutine != null)
            {
                StopCoroutine(_progressCoroutine);
            }

            _progressCoroutine = StartCoroutine(FillProgress());
        }
        else
        {
            if (_progressCoroutine != null)
            {
                StopCoroutine(_progressCoroutine);
            }
        }
    }

    // *
    // Note: the coroutine randomly stops working when the button is pressed too fast
    // *

    private IEnumerator FillProgress() 
    {
        yield return new WaitForSeconds(2);

        while (_currentProgress < _maxProgress)
        {
            // *
            // Every increment, the chance of the printer jamming is rolled, where if it gets a 1 then it james
            // and ends the coroutine prematurely, setting the _isPrinting to false and settiing _isJammed to
            // true.
            // After that, it rolls another random number to set the durability of the printer that needs to be beaten
            // to unjam the printer. 
            // *
            _randChance = Random.Range(1, 8);

            if (_randChance == 1)
            {
                Debug.Log("Printer Stopped Working");
                _isPrinting = false;
                _isJammed = true;
                _randDurability = Random.Range(2, 6);

                yield break;
            }    

            _currentProgress += _progressPerSecond;
            _progressBar.value = _currentProgress;

            yield return new WaitForSeconds(1);
        }

        _progressCoroutine = null;
    }
    #endregion 
}
