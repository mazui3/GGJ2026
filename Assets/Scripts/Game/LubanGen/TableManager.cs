using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class TableManager : Singleton<TableManager>
{
    private TableManager() {}
    private cfg.Tables _tables;
    public cfg.Tables Tables
    {
        get
        {
            if (_tables == null) 
                ReloadTable();
            return _tables;
        }
    }
    
    public void ReloadTable()
    {
        _tables = new cfg.Tables(file => SimpleJSON.JSON.Parse(Resources.Load<TextAsset>("Tables/Datapause/" + file).text));
    }
}
