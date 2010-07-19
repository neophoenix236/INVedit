using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace INVedit
{
	public class ItemSlot : CheckBox
	{
		static Font font1 = new Font(FontFamily.GenericMonospace, 8, FontStyle.Bold);
		static Font font2 = new Font(FontFamily.GenericMonospace, 10, FontStyle.Bold);
		protected static Item other = null;
		ToolTip toolTip = new ToolTip(){ AutomaticDelay = 300 };
		
		protected static event Action<ItemSlot> DragBegin = delegate {  };
		protected static event Action<ItemSlot> DragEnd = delegate {  };
		
		public bool Selected { get; set; }
		public byte Slot { get; set; }
		public Item Item { get; set; }
		public Image Default { get; set; }
		
		public event Action<ItemSlot> DragDone = delegate {  };
		
		public ItemSlot(byte slot)
		{
			Slot = slot;
			
			Size = new Size(48, 48);
			
			Appearance = Appearance.Button;
			AllowDrop = true;
			TabStop = false;
		}
		
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left) return;
			LostFocus += OnLostFocus;
			if (Item == null) return;
			Checked = true;
			DragBegin(this);
			other = Item;
			DragDropEffects final = DoDragDrop(Item, DragDropEffects.Copy | DragDropEffects.Move | DragDropEffects.Link);
			DragEnd(this);
			if (final == DragDropEffects.Move) { Item = other; }
			Checked = false;
			DragDone(this);
		}
		
		void OnLostFocus(object sender, EventArgs e)
		{
			LostFocus -= OnLostFocus;
			Refresh();
		}
		
		protected override void OnKeyDown(KeyEventArgs e) {  }
		
		protected override void OnDragOver(DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(Item))) {
				Item item = (Item)e.Data.GetData(typeof(Item));
				if (Item != item) {
					if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move) {
						if ((e.KeyState & 8) == 8) e.Effect = DragDropEffects.Copy;
						else if ((e.KeyState & 32) == 32 && item.Count > 1) {
							if (Item != null) {
								if (Item.ID == item.ID && item.Stackable && Item.Count < 64)
									e.Effect = DragDropEffects.Link;
								else e.Effect = DragDropEffects.None;
							} else e.Effect = DragDropEffects.Link;
						} else if (Item != null && Item.ID == item.ID &&
						           Item.Count >= (item.Stackable ? 64 : 1)) {
							e.Effect = DragDropEffects.None;
						} else e.Effect = DragDropEffects.Move;
					} else e.Effect = DragDropEffects.Copy;
				} else e.Effect = DragDropEffects.None;
			} else e.Effect = DragDropEffects.None;
		}
		
		protected override void OnDragDrop(DragEventArgs e)
		{
			OnDragOver(e);
			if (e.Effect == DragDropEffects.None) return;
			Item item = (Item)e.Data.GetData(typeof(Item));
			if (e.Effect == DragDropEffects.Link) {
				if (Item == null) {
					Item = new Item(item.ID, (byte)(item.Count/2), Slot, item.Damage);
					item.Count -= Item.Count;
				} else {
					byte count = Item.Count;
					Item.Count = Math.Min((byte)(count+item.Count/2), (byte)64);
					item.Count -= (byte)(Item.Count-count);
				}
			} else if (e.Effect == DragDropEffects.Move && Item != null && item.ID == Item.ID) {
				byte count = (byte)Math.Min((int)Item.Count + item.Count, item.Stackable ? 64 : 1);
				byte over = (byte)Math.Max((int)Item.Count + item.Count - (item.Stackable ? 64 : 1), 0);
				Item = new Item(Item.ID, count, Slot, Item.Damage);
				other = over>0 ? new Item(Item.ID, over) : null;
			} else {
				other = Item; Item = item;
				if (e.Effect == DragDropEffects.Copy)
					Item = new Item(Item.ID, Item.Count, Slot, Item.Damage);
				else Item.Slot = Slot;
				toolTip.SetToolTip(this, Item.Name);
				toolTip.Active = true;
			}
			LostFocus += OnLostFocus;
			Select();
		}
		
		public void Set(short id, byte count, short damage)
		{
			Item = new Item(id, count, Slot, damage);
			toolTip.SetToolTip(this, Item.Name);
			toolTip.Active = true;
		}
		
		public void Clear()
		{
			Item = null;
			toolTip.Active = false;
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			
			Graphics g = e.Graphics;
			if (Selected) g.DrawRectangle(new Pen(Color.Black), 4, 4, Width-9, Height-9);
			
			Image image = Default;
			if (Item != null) image = Item.Image;
			if (image == null) return;
			
			g.InterpolationMode = InterpolationMode.NearestNeighbor;
			g.PixelOffsetMode = PixelOffsetMode.Half;
			
			g.DrawImage(image, ClientSize.Width/2-16, ClientSize.Height/2-16, 32, 32);
			
			if (Item == null) return;
			
			if (!Item.Known) {
				string value = Item.ID.ToString();
				Color color1 = Color.Black;
				Color color2 = Color.White;
				DrawString3(g, color1, 0, -1, value);
				DrawString3(g, color1, 1, -1, value);
				DrawString3(g, color1, -1, 0, value);
				DrawString3(g, color1, 2, 0, value);
				DrawString3(g, color1, 0, 1, value);
				DrawString3(g, color1, 1, 1, value);
				DrawString3(g, color2, 0, 0, value);
				DrawString3(g, color2, 1, 0, value);
			}
			
			if (Item.Count > 1) {
				string value = Item.Count.ToString();
				Color color1 = Color.Black;
				Color color2 = Color.White;
				if (Item.Count > (Item.Stackable ? 64 : 1)) {
					color1 = Color.FromArgb(172, 0, 0);
				}
				DrawString2(g, color1, 3, 1, value);
				DrawString2(g, color1, 4, 1, value);
				DrawString2(g, color1, 2, 2, value);
				DrawString2(g, color1, 5, 2, value);
				DrawString2(g, color1, 3, 3, value);
				DrawString2(g, color1, 4, 3, value);
				DrawString2(g, color2, 3, 2, value);
				DrawString2(g, color2, 4, 2, value);
			}
			if (Item.Damage != 0) {
				if (Item.Damage > 0 && Item.Damage <= Item.MaxDamage && Item.MaxDamage > 0) {
					Rectangle rect = new Rectangle(5, ClientSize.Height-8, ClientSize.Width-10, 3);
					g.FillRectangle(new SolidBrush(Color.Black), rect);
					byte b = (byte)(Item.Damage*255/Item.MaxDamage);
					Color color = Color.FromArgb(b, 255-b, 0);
					int width = (int)((1-(double)(Item.Damage-1)/(Item.MaxDamage-1))*(ClientSize.Width-10));
					rect = new Rectangle(5, ClientSize.Height-8, width, 3);
					g.FillRectangle(new SolidBrush(color), rect);
				} else {
					string value = Math.Abs(Item.Damage).ToString();
					Color color1 = Color.FromArgb(56, 0, 0);
					Color color2 = Color.FromArgb(240, 0, 0);
					if (Item.Damage < 0) {
						color1 = Color.FromArgb(0, 56, 0);
						color2 = Color.FromArgb(0, 212, 0);
					}
					DrawString(g, color1, 3, 1, value);
					DrawString(g, color1, 4, 1, value);
					DrawString(g, color1, 2, 2, value);
					DrawString(g, color1, 5, 2, value);
					DrawString(g, color1, 3, 3, value);
					DrawString(g, color1, 4, 3, value);
					DrawString(g, color2, 3, 2, value);
					DrawString(g, color2, 4, 2, value);
				}
			}
		}
		
		void DrawString(Graphics g, Color color, int x, int y, string text)
		{
			g.DrawString(text, font1, new SolidBrush(color), x+2, y+1);
		}
		void DrawString2(Graphics g, Color color, int x, int y, string text)
		{
			StringFormat format = new StringFormat(){
				Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far };
			g.DrawString(text, font2, new SolidBrush(color), ClientSize.Width-x, ClientSize.Width-y, format);
		}
		void DrawString3(Graphics g, Color color, int x, int y, string text)
		{
			StringFormat format = new StringFormat(){
				Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
			g.DrawString(text, font1, new SolidBrush(color), ClientSize.Width/2+x, ClientSize.Width/2+y, format);
		}
	}
}
