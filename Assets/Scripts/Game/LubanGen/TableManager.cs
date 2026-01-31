using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    private TableManager() {}
    
    private static TableManager _instance;
    public static TableManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // 在场景中寻找，如果没找到就创建一个
                _instance = FindObjectOfType<TableManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("TableManager");
                    _instance = go.AddComponent<TableManager>();
                }
                // 确保它在切换场景时不被销毁
                DontDestroyOnLoad(_instance.gameObject);
                // _instance.Init(); // 在这里执行你的数据加载逻辑
            }
            return _instance;
        }
    }
    
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
