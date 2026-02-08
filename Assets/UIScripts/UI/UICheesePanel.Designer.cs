using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:0c6c0b9e-69d3-4f4f-8fc5-0e0fa901d2b6
	public partial class UICheesePanel
	{
		public const string Name = "UICheesePanel";
		
		[SerializeField]
		public RectTransform Content;
		[SerializeField]
		public Cheese Cheese;
		[SerializeField]
		public RectTransform ThePlaceCheeseSupposedToBe;
		
		private UICheesePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Content = null;
			Cheese = null;
			ThePlaceCheeseSupposedToBe = null;
			
			mData = null;
		}
		
		public UICheesePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UICheesePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UICheesePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
