using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Unity.VisualScripting;

namespace QFramework.Example
{
	public class UICheesePanelData : UIPanelData
	{
	}
	public partial class UICheesePanel : UIPanel, IController
	{
		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UICheesePanelData ?? new UICheesePanelData();
			// please add init code here
			
			this.GetModel<CheeseModel>().CurrentScene.RegisterWithInitValue(scene => 
			{
				if (scene != null)
				{
					RefreshUI(scene.Id); 
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			this.GetModel<CheeseModel>().CheeseDrag.RegisterWithInitValue(isEnable => 
			{
				Cheese.isDraggable = isEnable;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}
		
		public void RefreshUI(int level)
		{
			var cheeses = this.GetModel<CheeseModel>().CurrentScene.Value;
			Content.gameObject.DestroyChildren();
			foreach (var word in this.GetModel<CheeseModel>().CurrentCheeseList())
			{
				var theWord = Instantiate( Resources.Load("Prefabs/CheeseWord"), Content.transform);
				theWord.name = "CheeseWord_" + word.ID;
				theWord.GetComponent<CheeseWord>().LoadWord(word);
			}
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
