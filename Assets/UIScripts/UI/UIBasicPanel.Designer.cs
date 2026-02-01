using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:67caae7f-7d7b-4ffa-a7a9-53b90ef11bb2
	public partial class UIBasicPanel
	{
		public const string Name = "UIBasicPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image Logo;
		[SerializeField]
		public UnityEngine.UI.Button StartBtn;
		[SerializeField]
		public UnityEngine.UI.Button ExitBtn;
		
		private UIBasicPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Logo = null;
			StartBtn = null;
			ExitBtn = null;
			
			mData = null;
		}
		
		public UIBasicPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIBasicPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIBasicPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
