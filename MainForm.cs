using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;
using System.Net;

namespace INVedit
{
	public partial class MainForm : Form
	{
		static string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		bool update = true;
		List<CheckBox> groups = new List<CheckBox>();
		
		public MainForm(string[] files)
		{
			InitializeComponent();
			
			Data.Init("items.txt");
			CheckForUpdates("http://localhost/");
			
			boxItems.LargeImageList = Data.list;
			boxItems.ItemDrag += ItemDrag;
			
			foreach (Data.Group group in Data.groups.Values) {
				CheckBox box = new CheckBox();
				box.Size = new Size(26, 26);
				box.Location = new Point(Width-189, 30 + groups.Count*27);
				box.ImageList = Data.list;
				box.ImageIndex = group.imageIndex;
				box.Appearance = Appearance.Button;
				box.Anchor = AnchorStyles.Top | AnchorStyles.Right;
				box.Checked = true;
				box.Tag = group;
				box.CheckedChanged += ItemChecked;
				box.MouseDown += ItemMouseDown;
				new ToolTip().SetToolTip(box, group.name);
				Controls.Add(box);
				groups.Add(box);
			}
			
			UpdateItems();
			
			foreach (string file in files)
				if (File.Exists(file)) Open(file);
		}
		
		void Open(string file)
		{
			FileInfo info = new FileInfo(file);
			if (info.Extension.ToLower() == ".inv") {
				try {
					Page page = new Page(info.Name);
					page.file = file;
					Text = "INVedit - "+page.Text;
					Inventory.Load(NBT.Tag.Load(page.file)["Inventory"], page.slots);
					tabControl.TabPages.Add(page);
					tabControl.SelectedTab = page;
					btnSave.Enabled = true;
					btnSaveGame.Enabled = true;
					btnCloseTab.Enabled = true;
					btnReload.Enabled = true;
				} catch (Exception ex) { MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
			} else if (info.Extension.ToLower() == ".dat") {
				try {
					Page page = new Page(info.Name);
					page.file = file;
					Text = "INVedit - "+page.Text;
					Inventory.Load(file, page.slots);
					tabControl.TabPages.Add(page);
					tabControl.SelectedTab = page;
					btnSave.Enabled = true;
					btnSaveGame.Enabled = true;
					btnCloseTab.Enabled = true;
					btnReload.Enabled = true;
				} catch (Exception ex) { MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
			}
		}
		
		protected override void OnDragEnter(DragEventArgs e) {
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
				string[] files = ((string[])e.Data.GetData(DataFormats.FileDrop));
				foreach (string file in files) {
					FileInfo info = new FileInfo(file);
					if (info.Extension.ToLower() == ".inv" || info.Extension.ToLower() == ".dat")
						e.Effect = DragDropEffects.Copy;
				}
			}
		}
		protected override void OnDragDrop(DragEventArgs e) {
			OnDragEnter(e);
			BringToFront();
			if (e.Effect == DragDropEffects.None) return;
			string[] files = ((string[])e.Data.GetData(DataFormats.FileDrop));
			foreach (string file in files)
				if (File.Exists(file)) Open(file);
		}
		
		void UpdateItems()
		{
			boxItems.BeginUpdate();
			boxItems.Clear();
			foreach (CheckBox box in groups) if (box.Checked)
				foreach (Data.Item i in ((Data.Group)box.Tag).items)
					boxItems.Items.Add(new ListViewItem(i.name, i.imageIndex){ Tag = i });
			boxItems.EndUpdate();
		}
		
		void ItemChecked(object sender, EventArgs e)
		{
			if (update) UpdateItems();
		}
		
		void ItemMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;
			update = false;
			bool changed = false;
			CheckBox self = (CheckBox)sender;
			foreach (CheckBox box in groups) if (box.Checked == (self!=box)) {
				changed = true;
				box.Checked = (self==box);
			}
			self.Select();
			update = true;
			if (changed) UpdateItems();
		}
		
		void ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (e.Button != MouseButtons.Left) return;
			ListViewItem item = (ListViewItem)e.Item;
			Item i = new Item(((Data.Item)item.Tag).id);
			DoDragDrop(i, DragDropEffects.Copy | DragDropEffects.Move);
		}
		
		void BtnNewClick(object sender, EventArgs e)
		{
			Page page = new Page("unnamed.inv");
			Text = "INVedit - unnamed.inv";
			tabControl.TabPages.Add(page);
			tabControl.SelectedTab = page;
			btnSave.Enabled = true;
			btnSaveGame.Enabled = true;
			btnCloseTab.Enabled = true;
			btnReload.Enabled = false;
		}
		
		void BtnOpenClick(object sender, EventArgs e)
		{
			try {
				openFileDialog.Filter = "Inventory files (*.inv)|*.inv|All files (*.*)|*.*";
				openFileDialog.Title = "Open inventory";
				if (openFileDialog.ShowDialog() == DialogResult.OK) {
					Page page = new Page(openFileDialog.SafeFileName);
					page.file = openFileDialog.FileName;
					Text = "INVedit - "+page.Text;
					Inventory.Load(NBT.Tag.Load(page.file)["Inventory"], page.slots);
					tabControl.TabPages.Add(page);
					tabControl.SelectedTab = page;
					btnSave.Enabled = true;
					btnSaveGame.Enabled = true;
					btnCloseTab.Enabled = true;
					btnReload.Enabled = true;
				}
			} catch (Exception ex) { MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
		}
		
		void BtnSaveClick(object sender, EventArgs e)
		{
			try {
				saveFileDialog.Filter = "Inventory files (*.inv)|*.inv|All files (*.*)|*.*";
				saveFileDialog.Title = "Save inventory";
				if (saveFileDialog.ShowDialog() == DialogResult.OK) {
					Page page = (Page)tabControl.SelectedTab;
					page.file = saveFileDialog.FileName;
					page.world = false;
					NBT.Tag root = NBT.Tag.Create("");
					Inventory.Save(root, page.slots);
					root.Save(page.file);
					page.Text = new System.IO.FileInfo(page.file).Name;
					Text = "INVedit - "+page.Text;
					btnReload.Enabled = true;
				}
			} catch (Exception ex) { MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
		}
		
		void BtnOpenGameClick(object sender, EventArgs e)
		{
			try {
				openFileDialog.Filter = "Minecraft level files|level.dat|All files (*.*)|*.*";
				openFileDialog.Title = "Open from game";
				if (openFileDialog.ShowDialog() == DialogResult.OK) {
					Page page = new Page(openFileDialog.SafeFileName);
					page.file = openFileDialog.FileName;
					page.world = true;
					Text = "INVedit - "+page.Text;
					Inventory.Load(page.file, page.slots);
					tabControl.TabPages.Add(page);
					tabControl.SelectedTab = page;
					btnSave.Enabled = true;
					btnSaveGame.Enabled = true;
					btnCloseTab.Enabled = true;
					btnReload.Enabled = true;
				}
			} catch (Exception ex) { MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
		}
		
		void BtnSaveGameClick(object sender, EventArgs e)
		{
			try {
				openFileDialog.Filter = "Minecraft level files|level.dat|All files (*.*)|*.*";
				openFileDialog.Title = "Save to game";
				if (openFileDialog.ShowDialog() == DialogResult.OK) {
					Page page = (Page)tabControl.SelectedTab;
					page.file = openFileDialog.FileName;
					page.world = true;
					Inventory.Save(page.file, page.slots);
					page.Text = openFileDialog.SafeFileName;
					Text = "INVedit - "+page.Text;
					btnReload.Enabled = true;
				}
			} catch (Exception ex) { MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
		}
		
		void BtnCloseTabClick(object sender, EventArgs e)
		{
			tabControl.TabPages.Remove(tabControl.SelectedTab);
			if (tabControl.TabPages.Count == 0) {
				btnSave.Enabled = false;
				btnSaveGame.Enabled = false;
				btnCloseTab.Enabled = false;
			}
		}
		
		void BtnAboutClick(object sender, EventArgs e)
		{
			new AboutForm().ShowDialog();
		}
		
		
		void TabControlDragOver(object sender, DragEventArgs e)
		{
			Point point = tabControl.PointToClient(new Point(e.X, e.Y));
			TabPage hover = null;
			for (int i = 0; i < tabControl.TabPages.Count; ++i)
				if (tabControl.GetTabRect(i).Contains(point)) {
				hover = tabControl.TabPages[i]; break;
			}
			if (hover == null) return;
			if (!e.Data.GetDataPresent(typeof(Item))) return;
			tabControl.SelectedTab = hover;
		}
		
		void TabControlSelected(object sender, TabControlEventArgs e)
		{
			if (e.TabPage != null) {
				Text = "INVedit - "+e.TabPage.Text;
				btnReload.Enabled = (((Page)e.TabPage).file != null);
			} else {
				Text = "INVedit - Minecraft Inventory Editor";
				btnReload.Enabled = false;
			}
			
		}
		
		void BtnOpenGameDropDownOpening(object sender, EventArgs e)
		{
			btnOpenWorld1.Enabled = File.Exists(appdata+"/.minecraft/saves/World1/level.dat");
			btnOpenWorld2.Enabled = File.Exists(appdata+"/.minecraft/saves/World2/level.dat");
			btnOpenWorld3.Enabled = File.Exists(appdata+"/.minecraft/saves/World3/level.dat");
			btnOpenWorld4.Enabled = File.Exists(appdata+"/.minecraft/saves/World4/level.dat");
			btnOpenWorld5.Enabled = File.Exists(appdata+"/.minecraft/saves/World5/level.dat");
		}
		
		void BtnSaveGameDropDownOpening(object sender, EventArgs e)
		{
			btnSaveWorld1.Enabled = File.Exists(appdata+"/.minecraft/saves/World1/level.dat");
			btnSaveWorld2.Enabled = File.Exists(appdata+"/.minecraft/saves/World2/level.dat");
			btnSaveWorld3.Enabled = File.Exists(appdata+"/.minecraft/saves/World3/level.dat");
			btnSaveWorld4.Enabled = File.Exists(appdata+"/.minecraft/saves/World4/level.dat");
			btnSaveWorld5.Enabled = File.Exists(appdata+"/.minecraft/saves/World5/level.dat");
		}
		
		void BtnOpenWorldClick(object sender, EventArgs e)
		{
			try {
				int index = 0;
				if (sender == btnOpenWorld1) { index = 1; }
				else if (sender == btnOpenWorld2) { index = 2; }
				else if (sender == btnOpenWorld3) { index = 3; }
				else if (sender == btnOpenWorld4) { index = 4; }
				else if (sender == btnOpenWorld5) { index = 5; }
				Page page = new Page("World "+index);
				Text = "INVedit - "+page.Text;
				page.file = appdata+"/.minecraft/saves/World"+index+"/level.dat";
				page.world = true;
				Inventory.Load(page.file, page.slots);
				tabControl.TabPages.Add(page);
				tabControl.SelectedTab = page;
				btnSave.Enabled = true;
				btnSaveGame.Enabled = true;
				btnCloseTab.Enabled = true;
				btnReload.Enabled = true;
			} catch (Exception ex) { MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
		}
		
		void BtnSaveWorldClick(object sender, EventArgs e)
		{
			try {
				int index = 0;
				if (sender == btnSaveWorld1) { index = 1; }
				else if (sender == btnSaveWorld2) { index = 2; }
				else if (sender == btnSaveWorld3) { index = 3; }
				else if (sender == btnSaveWorld4) { index = 4; }
				else if (sender == btnSaveWorld5) { index = 5; }
				Page page = (Page)tabControl.SelectedTab;
				string file = appdata+"/.minecraft/saves/World"+index+"/level.dat";
				if (file == page.file ||
				    MessageBox.Show("Overwrite game inventory of world "+index+"?", "Question",
				                    MessageBoxButtons.OKCancel,
				                    MessageBoxIcon.Question) == DialogResult.OK) {
					page.file = file;
					page.world = true;
					Inventory.Save(page.file, page.slots);
					page.Text = "World "+index;
					Text = "INVedit - "+page.Text;
					btnReload.Enabled = true;
				}
			} catch (Exception ex) { MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
		}
		
		void BtnReloadClick(object sender, EventArgs e)
		{
			try {
				Page page = (Page)tabControl.SelectedTab;
				if (page.world) Inventory.Load(page.file, page.slots);
				else Inventory.Load(NBT.Tag.Load(page.file)["Inventory"], page.slots);
			} catch (Exception ex) { MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
		}
		
		void CheckForUpdates(string url)
		{
			WebClient client = new WebClient();
			client.DownloadFileCompleted += delegate {
				string[] lines = File.ReadAllLines("version");
				File.Delete("version");
				int version = 1;
				List<string> download = new List<string>();
				foreach (string line in lines) {
					if (line == "") ++version;
					else if (version > Data.version &&
					         !download.Contains(line)) download.Add(url+line);
				}
				if (download.Count > 0) {
					btnUpdate.Tag = download;
					btnUpdate.Enabled = true;
					btnUpdate.Text = "Update available";
					btnUpdate.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
				}
			};
			client.DownloadFileAsync(new Uri(url+"version"), "version");
		}
		
		void BtnUpdateClick(object sender, EventArgs e)
		{
			if (MessageBox.Show("INVedit will now close and update itself.\nAll unsaved changes will be lost. Continue?",
			                    "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
			new UpdateForm((List<string>)((ToolStripButton)sender).Tag).Start();
			Hide();
		}
	}
}
