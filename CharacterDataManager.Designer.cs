namespace CharacterDataManager
{
	partial class CharacterDataManagerForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterDataManagerForm));
			this.CharacterDataFolderTextBox = new System.Windows.Forms.TextBox();
			this.CharacterFolderBrowseButton = new System.Windows.Forms.Button();
			this.CharacterListDropdown = new System.Windows.Forms.ComboBox();
			this.SetCharacterAliasButton = new System.Windows.Forms.Button();
			this.DataFilesListBox = new System.Windows.Forms.ListBox();
			this.TargetCharactersListBox = new System.Windows.Forms.ListBox();
			this.CopyToSelectedButton = new System.Windows.Forms.Button();
			this.CopyToAllButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.MainFormToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.CopyAsLinksCheckbox = new System.Windows.Forms.CheckBox();
			this.CharacterDataFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.HelpLinkLabel = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// CharacterDataFolderTextBox
			// 
			this.CharacterDataFolderTextBox.Location = new System.Drawing.Point(8, 24);
			this.CharacterDataFolderTextBox.Name = "CharacterDataFolderTextBox";
			this.CharacterDataFolderTextBox.ReadOnly = true;
			this.CharacterDataFolderTextBox.Size = new System.Drawing.Size(256, 20);
			this.CharacterDataFolderTextBox.TabIndex = 0;
			// 
			// CharacterFolderBrowseButton
			// 
			this.CharacterFolderBrowseButton.Location = new System.Drawing.Point(272, 24);
			this.CharacterFolderBrowseButton.Name = "CharacterFolderBrowseButton";
			this.CharacterFolderBrowseButton.Size = new System.Drawing.Size(40, 23);
			this.CharacterFolderBrowseButton.TabIndex = 1;
			this.CharacterFolderBrowseButton.Text = "...";
			this.MainFormToolTip.SetToolTip(this.CharacterFolderBrowseButton, "Select the folder that contains the FFXIV character configuration data for all ch" +
        "aracters.");
			this.CharacterFolderBrowseButton.UseVisualStyleBackColor = true;
			this.CharacterFolderBrowseButton.Click += new System.EventHandler(this.CharacterFolderBrowseButton_Click);
			// 
			// CharacterListDropdown
			// 
			this.CharacterListDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CharacterListDropdown.FormattingEnabled = true;
			this.CharacterListDropdown.Location = new System.Drawing.Point(8, 72);
			this.CharacterListDropdown.Name = "CharacterListDropdown";
			this.CharacterListDropdown.Size = new System.Drawing.Size(256, 21);
			this.CharacterListDropdown.TabIndex = 2;
			this.CharacterListDropdown.SelectedIndexChanged += new System.EventHandler(this.CharacterListDropdown_SelectedIndexChanged);
			// 
			// SetCharacterAliasButton
			// 
			this.SetCharacterAliasButton.Location = new System.Drawing.Point(272, 72);
			this.SetCharacterAliasButton.Name = "SetCharacterAliasButton";
			this.SetCharacterAliasButton.Size = new System.Drawing.Size(75, 23);
			this.SetCharacterAliasButton.TabIndex = 3;
			this.SetCharacterAliasButton.Text = "Set Alias";
			this.MainFormToolTip.SetToolTip(this.SetCharacterAliasButton, "Set a friendly name for the currently selected character.");
			this.SetCharacterAliasButton.UseVisualStyleBackColor = true;
			this.SetCharacterAliasButton.Click += new System.EventHandler(this.SetCharacterAliasButton_Click);
			// 
			// DataFilesListBox
			// 
			this.DataFilesListBox.FormattingEnabled = true;
			this.DataFilesListBox.Location = new System.Drawing.Point(8, 120);
			this.DataFilesListBox.Name = "DataFilesListBox";
			this.DataFilesListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.DataFilesListBox.Size = new System.Drawing.Size(256, 264);
			this.DataFilesListBox.TabIndex = 4;
			// 
			// TargetCharactersListBox
			// 
			this.TargetCharactersListBox.FormattingEnabled = true;
			this.TargetCharactersListBox.Location = new System.Drawing.Point(280, 120);
			this.TargetCharactersListBox.Name = "TargetCharactersListBox";
			this.TargetCharactersListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.TargetCharactersListBox.Size = new System.Drawing.Size(256, 329);
			this.TargetCharactersListBox.TabIndex = 5;
			// 
			// CopyToSelectedButton
			// 
			this.CopyToSelectedButton.Location = new System.Drawing.Point(8, 392);
			this.CopyToSelectedButton.Name = "CopyToSelectedButton";
			this.CopyToSelectedButton.Size = new System.Drawing.Size(256, 23);
			this.CopyToSelectedButton.TabIndex = 6;
			this.CopyToSelectedButton.Text = "Copy to Selected";
			this.MainFormToolTip.SetToolTip(this.CopyToSelectedButton, "Copy the highlighted files from the selected character to the highlighted charact" +
        "er(s) on the right.");
			this.CopyToSelectedButton.UseVisualStyleBackColor = true;
			this.CopyToSelectedButton.Click += new System.EventHandler(this.CopyToSelectedButton_Click);
			// 
			// CopyToAllButton
			// 
			this.CopyToAllButton.Location = new System.Drawing.Point(8, 424);
			this.CopyToAllButton.Name = "CopyToAllButton";
			this.CopyToAllButton.Size = new System.Drawing.Size(256, 23);
			this.CopyToAllButton.TabIndex = 7;
			this.CopyToAllButton.Text = "Copy to All";
			this.MainFormToolTip.SetToolTip(this.CopyToAllButton, "Copy the highlighted files from the selected character to all other characters.");
			this.CopyToAllButton.UseVisualStyleBackColor = true;
			this.CopyToAllButton.Click += new System.EventHandler(this.CopyToAllButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(146, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "FFXIV Character Data Folder:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(155, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Character From Which to Copy:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 104);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(70, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Files to Copy:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(280, 104);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(111, 13);
			this.label4.TabIndex = 11;
			this.label4.Text = "Characters to Update:";
			// 
			// CopyAsLinksCheckbox
			// 
			this.CopyAsLinksCheckbox.AutoSize = true;
			this.CopyAsLinksCheckbox.Enabled = false;
			this.CopyAsLinksCheckbox.Location = new System.Drawing.Point(440, 96);
			this.CopyAsLinksCheckbox.Name = "CopyAsLinksCheckbox";
			this.CopyAsLinksCheckbox.Size = new System.Drawing.Size(92, 17);
			this.CopyAsLinksCheckbox.TabIndex = 8;
			this.CopyAsLinksCheckbox.Text = "Copy as Links";
			this.MainFormToolTip.SetToolTip(this.CopyAsLinksCheckbox, resources.GetString("CopyAsLinksCheckbox.ToolTip"));
			this.CopyAsLinksCheckbox.UseVisualStyleBackColor = true;
			// 
			// CharacterDataFolderDialog
			// 
			this.CharacterDataFolderDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
			// 
			// HelpLinkLabel
			// 
			this.HelpLinkLabel.AutoSize = true;
			this.HelpLinkLabel.Location = new System.Drawing.Point(488, 8);
			this.HelpLinkLabel.Name = "HelpLinkLabel";
			this.HelpLinkLabel.Size = new System.Drawing.Size(52, 13);
			this.HelpLinkLabel.TabIndex = 9;
			this.HelpLinkLabel.TabStop = true;
			this.HelpLinkLabel.Text = "Info/Help";
			this.HelpLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HelpLinkLabel_LinkClicked);
			// 
			// CharacterDataManagerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(547, 460);
			this.Controls.Add(this.CopyAsLinksCheckbox);
			this.Controls.Add(this.HelpLinkLabel);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.CopyToAllButton);
			this.Controls.Add(this.CopyToSelectedButton);
			this.Controls.Add(this.TargetCharactersListBox);
			this.Controls.Add(this.DataFilesListBox);
			this.Controls.Add(this.SetCharacterAliasButton);
			this.Controls.Add(this.CharacterListDropdown);
			this.Controls.Add(this.CharacterFolderBrowseButton);
			this.Controls.Add(this.CharacterDataFolderTextBox);
			this.Name = "CharacterDataManagerForm";
			this.Text = "FFXIV Character Data Manager";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CharacterDataManagerForm_FormClosed);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox CharacterDataFolderTextBox;
		private System.Windows.Forms.Button CharacterFolderBrowseButton;
		private System.Windows.Forms.ComboBox CharacterListDropdown;
		private System.Windows.Forms.Button SetCharacterAliasButton;
		private System.Windows.Forms.ListBox DataFilesListBox;
		private System.Windows.Forms.ListBox TargetCharactersListBox;
		private System.Windows.Forms.Button CopyToSelectedButton;
		private System.Windows.Forms.Button CopyToAllButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ToolTip MainFormToolTip;
		private System.Windows.Forms.FolderBrowserDialog CharacterDataFolderDialog;
		private System.Windows.Forms.LinkLabel HelpLinkLabel;
		private System.Windows.Forms.CheckBox CopyAsLinksCheckbox;
	}
}

