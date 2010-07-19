/*
 * Erstellt mit SharpDevelop.
 * Benutzer: copyboy
 * Datum: 16.07.2010
 * Zeit: 13:48
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
namespace INVedit
{
	partial class UpdateForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateForm));
			this.boxProgress = new System.Windows.Forms.Label();
			this.barProgress = new System.Windows.Forms.ProgressBar();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnFinish = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// boxProgress
			// 
			this.boxProgress.Location = new System.Drawing.Point(6, 6);
			this.boxProgress.Name = "boxProgress";
			this.boxProgress.Size = new System.Drawing.Size(280, 13);
			this.boxProgress.TabIndex = 1;
			this.boxProgress.Text = "Progress:";
			// 
			// barProgress
			// 
			this.barProgress.Location = new System.Drawing.Point(7, 24);
			this.barProgress.Name = "barProgress";
			this.barProgress.Size = new System.Drawing.Size(278, 21);
			this.barProgress.TabIndex = 2;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(211, 51);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
			// 
			// btnFinish
			// 
			this.btnFinish.Enabled = false;
			this.btnFinish.Location = new System.Drawing.Point(130, 51);
			this.btnFinish.Name = "btnFinish";
			this.btnFinish.Size = new System.Drawing.Size(75, 23);
			this.btnFinish.TabIndex = 3;
			this.btnFinish.Text = "Finish";
			this.btnFinish.UseVisualStyleBackColor = true;
			this.btnFinish.Click += new System.EventHandler(this.BtnFinishClick);
			// 
			// UpdateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 80);
			this.Controls.Add(this.btnFinish);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.barProgress);
			this.Controls.Add(this.boxProgress);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UpdateForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Updating INVedit ...";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpdateFormFormClosing);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button btnFinish;
		private System.Windows.Forms.ProgressBar barProgress;
		private System.Windows.Forms.Label boxProgress;
		private System.Windows.Forms.Button btnCancel;
	}
}
