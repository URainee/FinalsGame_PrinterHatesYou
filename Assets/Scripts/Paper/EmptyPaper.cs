using UnityEngine;

public class EmptyPaper : BasePaper
{
    protected override void Start()
    {
        SetStatus(Status.Empty);
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
