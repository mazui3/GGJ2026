using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:46cac9e1-964d-4adf-8e34-14fb48df2c92
	public partial class CheesePanel
	{
		public const string Name = "CheesePanel";
		
		[SerializeField]
		public RectTransform Content;
		[SerializeField]
		public UnityEngine.UI.Image Image;
		
		private CheesePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Content = null;
			Image = null;
			
			mData = null;
		}
		
		public CheesePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		CheesePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new CheesePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
