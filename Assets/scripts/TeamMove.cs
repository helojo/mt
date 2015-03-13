using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TeamMove
{
    public void ResumeMove()
    {
        if (teamMove != null)
        {
            teamMove.enabled = true;
        }
        if (teamMove2 != null)
        {
            teamMove2.enabled = true;
        }
        if (audio != null)
        {
            audio.Play();
        }
    }

    public void StopMove()
    {
        if (teamMove != null)
        {
            teamMove.enabled = false;
            teamMove.isPaused = true;
        }
        if (teamMove2 != null)
        {
            teamMove2.enabled = false;
            teamMove2.isPaused = true;
        }
        if (audio != null)
        {
            audio.Pause();
        }
    }

    public AudioSource audio { get; set; }

    public iTween teamMove { get; set; }

    public iTween teamMove2 { get; set; }
}

