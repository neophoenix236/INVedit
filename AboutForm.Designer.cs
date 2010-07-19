/*
 * Erstellt mit SharpDevelop.
 * Benutzer: copyboy
 * Datum: 28.06.2010
 * Zeit: 09:35
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
namespace INVedit
{
	partial class AboutForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.linkLabel3 = new System.Windows.Forms.LinkLabel();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.ErrorImage = null;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.InitialImage = null;
			this.pictureBox1.Location = new System.Drawing.Point(9, 11);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.TabIndex = 18;
			this.pictureBox1.TabStop = false;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(11, 120);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(202, 13);
			this.label6.TabIndex = 17;
			this.label6.Text = "Free to use and distribute.";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(12, 86);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(94, 13);
			this.label7.TabIndex = 15;
			this.label7.Text = "Silk icon set:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(12, 52);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(94, 13);
			this.label5.TabIndex = 16;
			this.label5.Text = "More information:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(76, 106);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(112, 13);
			this.label4.TabIndex = 14;
			this.label4.Text = "copyboy@hotmail.de";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(36, 106);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(42, 13);
			this.label3.TabIndex = 13;
			this.label3.Text = "E-Mail:";
			// 
			// linkLabel3
			// 
			this.linkLabel3.Location = new System.Drawing.Point(109, 86);
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.Size = new System.Drawing.Size(101, 13);
			this.linkLabel3.TabIndex = 11;
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = "famfamfam.com";
			this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel3LinkClicked);
			// 
			// linkLabel2
			// 
			this.linkLabel2.Location = new System.Drawing.Point(109, 52);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(101, 13);
			this.linkLabel2.TabIndex = 12;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "minecraftforum.net";
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel2LinkClicked);
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(100, 34);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(79, 13);
			this.linkLabel1.TabIndex = 10;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "SharpDevelop";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1LinkClicked);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(45, 34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Made with";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(43, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(166, 27);
			this.label1.TabIndex = 8;
			this.label1.Text = "INVedit ({version}) - by copyboy\r\nC# .NET ({net-version})";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(108, 71);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(102, 13);
			this.label9.TabIndex = 20;
			this.label9.Text = "epaGamer";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(12, 71);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(94, 13);
			this.label8.TabIndex = 19;
			this.label8.Text = "Program icon:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// AboutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(222, 140);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.linkLabel3);
			this.Controls.Add(this.linkLabel2);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "AboutForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About INVedit";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.LinkLabel linkLabel3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label1;
	}
}
