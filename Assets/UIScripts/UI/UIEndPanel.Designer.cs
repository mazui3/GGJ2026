using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:93abd0e2-c653-4313-82e7-f78e29fdb12b
	public partial class UIEndPanel
	{
		public const string Name = "UIEndPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image Gallery;
		[SerializeField]
		public TMPro.TextMeshProUGUI Endroll;
		[SerializeField]
		public UnityEngine.UI.Button BackBtn;
		
		private UIEndPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Gallery = null;
			Endroll = null;
			BackBtn = null;
			
			mData = null;
		}
		
		public UIEndPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIEndPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIEndPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
