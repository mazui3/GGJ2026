using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:bcf3d176-d36a-49d4-bc81-e335cee9e693
	public partial class UICheesePanel
	{
		public const string Name = "UICheesePanel";
		
		[SerializeField]
		public RectTransform Content;
		[SerializeField]
		public Cheese Cheese;
		
		private UICheesePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Content = null;
			Cheese = null;
			
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
