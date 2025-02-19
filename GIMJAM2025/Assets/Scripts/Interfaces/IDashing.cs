using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDashing
{
    float dashSpeed {get;set;}
    float dashDuration {get;set;}
    float dashCooldown {get;set;}
    float dashTime {get;set;}

    void StartDash();
    void StopDash();
}
