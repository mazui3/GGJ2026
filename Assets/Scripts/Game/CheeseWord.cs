using cfg.Data;
using cfg.Enum;
using UnityEngine;
using QFramework;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.UIElements;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{

	public partial class CheeseWord : ViewController
	{
		public UnityEngine.UI.Image Image;
		public UnityEngine.UI.Button Button;
		
		private float aspectRatio = 0.5f;
		
		CheeseDictionary currentDictionary;
		
		void Start()
		{
			// Code Here
		}

		//使用 LayoutElement 强制覆盖
		public void LoadWord(CheeseDictionary theWord)
		{
			currentDictionary = theWord;
			Sprite sprite = Resources.Load<Sprite>("Sprites/Words/" + theWord.Icon);
			Image.sprite = sprite;
			
			LayoutElement le = GetComponent<LayoutElement>();
			if (le == null) le = gameObject.AddComponent<LayoutElement>();
			
			float targetWidth = sprite.rect.width * aspectRatio;
			float targetHeight = sprite.rect.height * aspectRatio;
			
			le.preferredWidth = targetWidth;
			le.preferredHeight = targetHeight;
			
			RectTransform rt = Image.transform.GetComponent<RectTransform>();
			rt.sizeDelta = new Vector2(targetWidth, targetHeight);
			
			Button.onClick.RemoveAllListeners();
			Button.onClick.AddListener((() =>
			{
				Debug.Log(theWord.Wordcontent[0].WordChoice[0]);
			}));
		}
		
		public string RevealWord(CheeseWordData dataLookUp = CheeseWordData.Subject)
		{
			// I should use a map instead of a list of workContent
			var theWord = currentDictionary.Wordcontent.Find(x => x.TypeOfWord == dataLookUp);
			// TODO Word choice is a list to involve some randomness later
			return theWord.WordChoice[0];
		}
		
	}
}
