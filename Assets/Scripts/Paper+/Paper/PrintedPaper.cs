using UnityEngine;

public class PrintedPaper : BasePaper
{
    protected override void Start()
    {
        SetStatus(Status.Printed);
        //DebugText();
    }

    protected override void SetStatus(Status passedStatus)
    {
        base.SetStatus(passedStatus);
    }

    private void DebugText()
    {
        Debug.Log(CurrentStatus);
    }
}
