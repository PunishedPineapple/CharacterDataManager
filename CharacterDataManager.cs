using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

//	Use for InputBox rather than rolling our own trivial input dialog.
using Microsoft.VisualBasic;

namespace CharacterDataManager
{
	public partial class CharacterDataManagerForm : Form
	{
		//	Construction
		public CharacterDataManagerForm()
		{
			//	WinForms Stuff
			InitializeComponent();

			//	Get config settings.
			mSettings = new Config();

			//	Populate the controls for the character config folder, character list, and file list based on the saved settings.
			if( Directory.Exists( mSettings.ProgramSettings.CharacterDataFolderPath ) )
			{
				CharacterDataFolderTextBox.Text = mSettings.ProgramSettings.CharacterDataFolderPath;
				PopulateCharacterLists();
				if( mSettings.ProgramSettings.DefaultCharacterID.Length > 0 && Directory.Exists( mSettings.ProgramSettings.CharacterDataFolderPath + '\\' + mSettings.ProgramSettings.DefaultCharacterID ) )
				{
					for( int i = 0; i < CharacterListDropdown.Items.Count; ++i )
					{
						if( mCharacterFolderList[i].Split( '\\' ).Last() == mSettings.ProgramSettings.DefaultCharacterID )
						{
							CharacterListDropdown.SelectedIndex = i;
							break;
						}
					}
				}
			}

			//	Show a warning about backing up character data if this is the first time that the program has been used.
			if( mSettings.ProgramSettings.ShowInitialWarning )
			{
				MessageBox.Show( "Please back up your character configuration data before using this program.  This can be done either through the configuration options in the FFXIV launcher, or on the character selection screen in-game.", "Warning" );
				mSettings.ProgramSettings.ShowInitialWarning = false;
			}
		}

		//	Data Members
		private string[] mCharacterFolderList;
		private Config mSettings;

		//	Member Functions
		private void PopulateCharacterLists()
		{
			//	*****TODO: Probably save off the selected characters in the righthand box into settings before we clean up and rebuild.  Maybe save the default character too.*****

			CharacterListDropdown.Items.Clear();
			TargetCharactersListBox.Items.Clear();
			PopulateDataFilesListBox( true );
			mCharacterFolderList = Directory.GetDirectories( mSettings.ProgramSettings.CharacterDataFolderPath, "FFXIV_CHR*", SearchOption.TopDirectoryOnly );
			foreach( string dir in mCharacterFolderList )
			{
				CharacterListDropdown.Items.Add( mSettings.CharacterAliasSettings.GetAlias( dir.Split( '\\' ).Last() ) );
				TargetCharactersListBox.Items.Add( mSettings.CharacterAliasSettings.GetAlias( dir.Split( '\\' ).Last() ) );
			}

			//	*****TODO: Can we set the entry in the list box to be unselectable for the character in the combo box, or do we need to just let it happen and only handle ignoring it when actually copying files?*****

			//	Set the selection to what we had saved.
			for( int i = 0; i < TargetCharactersListBox.Items.Count; ++i )
			{
				TargetCharactersListBox.SetSelected( i, mSettings.ProgramSettings.DefaultSelectedTargetFolders.Contains( mCharacterFolderList[i].Split( '\\' ).Last() ) );
			}
		}

		private void PopulateDataFilesListBox( bool clear = false )
		{
			//	*****TODO: Probably save off the selected files into settings before we clean up and rebuild.*****

			DataFilesListBox.SelectedIndex = -1;
			DataFilesListBox.Items.Clear();

			if( !clear && CharacterListDropdown.SelectedIndex > -1 )
			{
				//	Fill in the files we have.
				string[] files = Directory.GetFiles( mCharacterFolderList[CharacterListDropdown.SelectedIndex], "*.DAT", SearchOption.TopDirectoryOnly );
				foreach( string file in files )
				{
					DataFilesListBox.Items.Add( file.Split( '\\' ).Last().Trim() );
				}

				//	Set the selection to what we had saved.
				for( int i = 0; i < DataFilesListBox.Items.Count; ++i )
				{
					DataFilesListBox.SetSelected( i, mSettings.ProgramSettings.DefaultSelectedFiles.Contains( DataFilesListBox.Items[i].ToString() ) );
				}
			}
		}

		//	Event Handlers
		private void HelpLinkLabel_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
		{
			System.Diagnostics.Process.Start( "https://github.com/PunishedPineapple/CharacterDataManager" );
		}

		private void CharacterDataManagerForm_FormClosed( object sender, FormClosedEventArgs e )
		{
			//	*****TODO: Package these bits off into functions that we can use for saving selections up above too (when changing characters, directories, etc.).*****

			//	Save which character is selected for next time.
			if( CharacterListDropdown.SelectedIndex > -1 && CharacterListDropdown.SelectedIndex < mCharacterFolderList.Length )
			{
				mSettings.ProgramSettings.DefaultCharacterID = mCharacterFolderList[CharacterListDropdown.SelectedIndex].Split( '\\' ).Last();
			}

			//	Save which files are selected for next time.
			//	*****TODO: Use indices like the next bit so that we can have aliases.
			mSettings.ProgramSettings.DefaultSelectedFiles.Clear();
			foreach( int i in DataFilesListBox.SelectedIndices )
			{
				mSettings.ProgramSettings.DefaultSelectedFiles.Add( DataFilesListBox.Items[i].ToString() );
				
			}

			//	Save which characters we have selected in the right-hand list.
			mSettings.ProgramSettings.DefaultSelectedTargetFolders.Clear();
			for( int i = 0; i < TargetCharactersListBox.Items.Count; ++i )
			{
				if( TargetCharactersListBox.SelectedIndices.Contains( i ) ) mSettings.ProgramSettings.DefaultSelectedTargetFolders.Add( mCharacterFolderList[i].Split( '\\' ).Last() );
			}

			//	Write it all out to file.
			mSettings.SaveConfig();
		}

		private void CharacterListDropdown_SelectedIndexChanged( object sender, EventArgs e )
		{
			PopulateDataFilesListBox( CharacterListDropdown.SelectedIndex < 0 || CharacterListDropdown.SelectedIndex >= mCharacterFolderList.Length );
		}

		private void CharacterFolderBrowseButton_Click( object sender, EventArgs e )
		{
			CharacterDataFolderDialog.ShowDialog();
			if( ( CharacterDataFolderDialog.SelectedPath != null ) && Directory.Exists( CharacterDataFolderDialog.SelectedPath ) )
			{
				mSettings.ProgramSettings.CharacterDataFolderPath = CharacterDataFolderDialog.SelectedPath;
				CharacterDataFolderTextBox.Text = mSettings.ProgramSettings.CharacterDataFolderPath;
				PopulateCharacterLists();
			}
		}
	}
}
