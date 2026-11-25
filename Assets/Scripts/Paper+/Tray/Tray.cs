using UnityEngine;

public class Tray : MonoBehaviour
{
    //*
    //This Tray is the GameObject used to increase the score counter in GameManager, meaning that
    //it notes how many successful 'prints' you make when you drag a PrintedPaper into it.
    //*

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PrintedPaper>() != null)
        {
            PrintedPaper printedPaper = collision.gameObject.GetComponent<PrintedPaper>();

            if (printedPaper.IsDragging == false)
            {
                printedPaper.TakeDamage(1);

                AudioSystem.Instance.PlaySound(SoundType.SFX, 0);

                GameManager.Instance.AddScore(1);

                //*
                //Send out code to tick the GameManager's score counter by one!
                //e.g. GameManager.Instance.AddScore();
                //*
            }
        }
    }

}
