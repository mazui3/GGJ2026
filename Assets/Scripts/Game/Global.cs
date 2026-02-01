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
        RegisterSystem(new CheeseSystem());
        
        // 如果有其他 System 也可以在这里注册
        RegisterSystem(new GamePlaySystem());
        
        // Command
        IsAlive = true;
    }
    
    protected override void OnDeinit()
    {
        IsAlive = false;
    }
}

public struct ResetGameEvent
{
    public int Level;
}

public struct DropCheeseEvent 
{
    public int CheeseId;
    public string Word;
}

public class RestartGameCommand : AbstractCommand
{
    private readonly int level;
    
    public RestartGameCommand(int targetLevel)
    {
        level = targetLevel;
    }

    protected override void OnExecute()
    {
        this.GetSystem<GamePlaySystem>().StartLevel(1);
        this.GetSystem<GamePlaySystem>().ChangeState(GamePlayType.Reset);
    }
}