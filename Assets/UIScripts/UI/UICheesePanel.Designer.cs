using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:1d05beb1-ed73-4b86-b999-5d6cfd16075f
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
