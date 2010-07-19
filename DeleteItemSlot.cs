using System;
using System.Drawing;
using System.Windows.Forms;

namespace INVedit
{
	public class DeleteItemSlot : ItemSlot
	{
		Image enabled;
		Image disabled;
		
		public DeleteItemSlot(Image enabled, Image disabled) : base(0xFF)
		{
			this.enabled = enabled;
			this.disabled = disabled;
			Enabled = false;
			
			DragBegin += delegate { Enabled = true; };
			DragEnd += delegate { Enabled = false; };
		}
		
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			Default = Enabled ? enabled : disabled;
		}
		
		protected override void OnDragOver(DragEventArgs e)
		{
			base.OnDragOver(e);
			if (e.Effect != DragDropEffects.Move)
				e.Effect = DragDropEffects.Move;
		}
		
		protected override void OnDragDrop(DragEventArgs e)
		{
			other = null;
		}
	}
}
