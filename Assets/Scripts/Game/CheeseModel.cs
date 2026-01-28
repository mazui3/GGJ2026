using System.Collections;
using System.Collections.Generic;
using cfg.Data;
using cfg.Enum;
using QFramework;
using UnityEngine;

public class CheeseModel : AbstractModel
{
    private CheeseScene currentScene;
    public CheeseScene CurrentScene { get { return currentScene; } }
    
    //Need to store the data for the CheeseSentence Player form
    protected override void OnInit()
    {
    }

    public List<CheeseWord> CurrentCheeseList()
    {
        List<CheeseWord> results = new List<CheeseWord>();
        
        if (currentScene == null)
            return null;
        else
        {
            foreach (var word in currentScene.Words)
            {
                TableManager.Instance.Tables.TbCheeseDictionary.Get(word);
            }
        }

        return null;
    }
}
