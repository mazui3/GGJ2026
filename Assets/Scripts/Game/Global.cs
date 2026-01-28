using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class Global : Architecture<Global>
{
    public static bool IsAlive;
    
    protected override void Init()
    {
        AudioKit.PlaySoundMode = AudioKit.PlaySoundModes.IgnoreSameSoundInSoundFrames;
        RegisterModel(new CheeseModel());
        
        // Command
        IsAlive = true;
    }
    
    protected override void OnDeinit()
    {
        IsAlive = false;
    }
}
