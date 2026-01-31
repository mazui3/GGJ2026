using cfg.Data;
using UnityEngine;
using QFramework;
using Unity.VisualScripting;
using UnityEngine.UIElements;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{

	public partial class CheeseWord : ViewController
	{
		public UnityEngine.UI.Image Image;
		public UnityEngine.UI.Button Button;

		CheeseDictionary currentDictionary;
		
		void Start()
		{
			// Code Here
		}

		public void LoadWord(CheeseDictionary theWord)
		{
			currentDictionary = theWord;
			Image.sprite =  Resources.Load<Sprite>("Sprites/Words/" + theWord.Icon);
			Button.onClick.RemoveAllListeners();
			Button.onClick.AddListener((() =>
			{
				Debug.Log(theWord.Wordcontent[0].WordChoice[0]);
			}));
		}

		public string RevealWord()
		{
			return currentDictionary.Wordcontent[0].WordChoice[0];
		}
		
	}
}
