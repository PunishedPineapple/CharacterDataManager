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
using System.Runtime.InteropServices;

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
		private List<string> mDataFileNameList = new List<string>();
		private Config mSettings;

		//	Imports
		[DllImport( "Kernel32.dll", CharSet = CharSet.Unicode )]
		static extern bool CreateHardLink( string lpFileName, string lpExistingFileName, IntPtr lpSecurityAttributes );

		//	Member Functions
		private void PopulateCharacterLists()
		{
			//	Save off the character selections before we clean up and rebuild.
			SaveSourceCharacterSelection();
			SaveTargetCharacterListSelection();
			SaveCopyAsLinksOption();

			CharacterListDropdown.Items.Clear();
			TargetCharactersListBox.Items.Clear();
			PopulateDataFilesListBox( true );
			mCharacterFolderList = Directory.GetDirectories( mSettings.ProgramSettings.CharacterDataFolderPath, "FFXIV_CHR*", SearchOption.TopDirectoryOnly );
			foreach( string dir in mCharacterFolderList )
			{
				CharacterListDropdown.Items.Add( mSettings.CharacterAliasSettings.GetAlias( dir.Split( '\\' ).Last() ) );
				TargetCharactersListBox.Items.Add( mSettings.CharacterAliasSettings.GetAlias( dir.Split( '\\' ).Last() ) );
			}

			//	Set the selections to what we had saved.
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
			for( int i = 0; i < TargetCharactersListBox.Items.Count; ++i )
			{
				TargetCharactersListBox.SetSelected( i, mSettings.ProgramSettings.DefaultSelectedTargetFolders.Contains( mCharacterFolderList[i].Split( '\\' ).Last() ) );
			}

			//	Don't allow the user to attempt to copy as links unless the filesystem supports hard links.
			bool allowLinks = mCharacterFolderList.Length > 0 && new DriveInfo( Directory.GetDirectoryRoot( mCharacterFolderList[0] ) ).DriveFormat.Equals( "NTFS" );
			CopyAsLinksCheckbox.Enabled = allowLinks;
			CopyAsLinksCheckbox.Checked = mSettings.ProgramSettings.CopyAsLinks && allowLinks;
		}

		private void PopulateDataFilesListBox( bool clear = false )
		{
			//	Save off the selected files into settings before we clean up and rebuild.
			SaveFileListSelection();

			DataFilesListBox.SelectedIndex = -1;
			DataFilesListBox.Items.Clear();
			mDataFileNameList.Clear();

			if( !clear && CharacterListDropdown.SelectedIndex > -1 )
			{
				//	Fill in the files we have.
				string[] files = Directory.GetFiles( mCharacterFolderList[CharacterListDropdown.SelectedIndex], "*.DAT", SearchOption.TopDirectoryOnly );
				foreach( string file in files )
				{
					DataFilesListBox.Items.Add( mSettings.FileAliasSettings.GetAlias( file.Split( '\\' ).Last().Trim() ) );
					mDataFileNameList.Add( file.Split( '\\' ).Last().Trim() );
				}

				//	Set the selection to what we had saved.
				for( int i = 0; i < DataFilesListBox.Items.Count; ++i )
				{
					DataFilesListBox.SetSelected( i, mSettings.ProgramSettings.DefaultSelectedFiles.Contains( mDataFileNameList[i].ToString() ) );
				}
			}
		}

		private void CopyFiles( string[] fileNames, string sourceDir, string[] destDirs, bool copyAsLinks )
		{
			bool hadLinkingFailures = false;
			bool hadMissingFilesOrFolders = false;

			foreach( string fileName in fileNames )
			{
				foreach( string destDir in destDirs )
				{
					//	Don't copy from source back to source.
					if( destDir.Equals( sourceDir ) ) continue;

					//	Just for cleanliness.
					string sourceFilePath = sourceDir + "\\" + fileName;
					string destFilePath = destDir + "\\" + fileName;

					//	If the source file or the destination directory doesn't exist, skip it.
					if( !File.Exists( sourceFilePath ) || !Directory.Exists( destDir ) )
					{
						hadMissingFilesOrFolders = true;
						continue;
					}

					//	If we're copying a linked file over an existing hard link, it might fail, so delete first before copying/linking.
					if( File.Exists( destFilePath ) ) File.Delete( destFilePath );

					//	Handle any requested linking.
					if( copyAsLinks )
					{
						//	Try to link.
						hadLinkingFailures |= !CreateHardLink( @"\\?\" + destFilePath, @"\\?\" + sourceFilePath, IntPtr.Zero );

						//	If we couldn't link, clean up and fall back to plain old copying.
						if( hadLinkingFailures )
						{
							if( File.Exists( destFilePath ) ) File.Delete( destFilePath );
							copyAsLinks = false;
						}
					}

					//	Handle regular copying.  Can't be an "else" due to being fallback behavior from failed linking.
					if( !copyAsLinks )
					{
						File.Copy( sourceFilePath, destFilePath );
					}
				}
			}

			if( hadLinkingFailures )
			{
				MessageBox.Show( "Error(s) occurred while trying to create file links.  This most likely occurred either because the file system does not support hard links, or because an attempt was made to overwrite an existing link with the linked file.  An attempt was made to copy the file(s) without creating links.", "Error!" );
			}

			if( hadMissingFilesOrFolders )
			{
				MessageBox.Show( "Error(s) occurred while trying to copy files: One or more source files or destination directories could not be found.", "Error!" );
			}

			if( !hadMissingFilesOrFolders )
			{
				MessageBox.Show( "The specified copying/linking operation was completed.", "Complete" );
			}

		}

		//	Event Handlers
		private void HelpLinkLabel_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
		{
			System.Diagnostics.Process.Start( "https://github.com/PunishedPineapple/CharacterDataManager" );
		}

		private void CharacterDataManagerForm_FormClosed( object sender, FormClosedEventArgs e )
		{
			//	Save which character is selected for next time.
			SaveSourceCharacterSelection();

			//	Save which files are selected for next time.
			SaveFileListSelection();

			//	Save which characters we have selected in the right-hand list.
			SaveTargetCharacterListSelection();

			//	Save any other miscellaneous things.
			SaveCopyAsLinksOption();

			//	Write it all out to file.
			mSettings.SaveConfig();
		}

		void SaveFileListSelection()
		{
			if( DataFilesListBox.Items.Count > 0 )
			{
				mSettings.ProgramSettings.DefaultSelectedFiles.Clear();
				foreach( int i in DataFilesListBox.SelectedIndices )
				{
					mSettings.ProgramSettings.DefaultSelectedFiles.Add( mDataFileNameList[i].ToString() );
				}
			}
		}

		void SaveSourceCharacterSelection()
		{
			if( CharacterListDropdown.Items.Count > 0 && CharacterListDropdown.SelectedIndex > -1 && CharacterListDropdown.SelectedIndex < mCharacterFolderList.Length )
			{
				mSettings.ProgramSettings.DefaultCharacterID = mCharacterFolderList[CharacterListDropdown.SelectedIndex].Split( '\\' ).Last();
			}
		}

		void SaveTargetCharacterListSelection()
		{
			if( TargetCharactersListBox.Items.Count > 0 )
			{
				mSettings.ProgramSettings.DefaultSelectedTargetFolders.Clear();
				for( int i = 0; i < TargetCharactersListBox.Items.Count; ++i )
				{
					if( TargetCharactersListBox.SelectedIndices.Contains( i ) ) mSettings.ProgramSettings.DefaultSelectedTargetFolders.Add( mCharacterFolderList[i].Split( '\\' ).Last() );
				}
			}
		}

		void SaveCopyAsLinksOption()
		{
			if( CopyAsLinksCheckbox.Visible )
			{
				mSettings.ProgramSettings.CopyAsLinks = CopyAsLinksCheckbox.Checked;
			}
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

		private void CopyToSelectedButton_Click( object sender, EventArgs e )
		{
			if( CharacterListDropdown.SelectedIndex > -1 &&
				CharacterListDropdown.SelectedIndex < mCharacterFolderList.Length &&
				Directory.Exists( mCharacterFolderList[CharacterListDropdown.SelectedIndex] ) )
			{
				//	Set up which files to copy.
				List<string> filesToCopyList = new List<string>();
				foreach( int i in DataFilesListBox.SelectedIndices )
				{
					filesToCopyList.Add( mDataFileNameList[i] );
				}

				//	Set up which folders to copy to.
				List<string> destinationDirsList = new List<string>();
				foreach( int i in TargetCharactersListBox.SelectedIndices )
				{
					destinationDirsList.Add( mCharacterFolderList[i] );
				}

				//	Do the copying
				CopyFiles( filesToCopyList.ToArray(), mCharacterFolderList[CharacterListDropdown.SelectedIndex], destinationDirsList.ToArray(), CopyAsLinksCheckbox.Checked );
			}
		}

		private void CopyToAllButton_Click( object sender, EventArgs e )
		{
			if( CharacterListDropdown.SelectedIndex > -1 &&
				CharacterListDropdown.SelectedIndex < mCharacterFolderList.Length &&
				Directory.Exists( mCharacterFolderList[CharacterListDropdown.SelectedIndex] ) )
			{
				//	Set up which files to copy.
				List<string> filesToCopyList = new List<string>();
				foreach( int i in DataFilesListBox.SelectedIndices )
				{
					filesToCopyList.Add( mDataFileNameList[i] );
				}

				//	Set up which folders to copy to.  In this case, this is just everything but the source folder.
				List<string> destinationDirsList = new List<string>();
				foreach( string str in mCharacterFolderList )
				{
					if( !str.Equals( mCharacterFolderList[CharacterListDropdown.SelectedIndex] ) ) destinationDirsList.Add( str );
				}

				//	Do the copying
				CopyFiles( filesToCopyList.ToArray(), mCharacterFolderList[CharacterListDropdown.SelectedIndex], destinationDirsList.ToArray(), CopyAsLinksCheckbox.Checked );
			}
		}

		private void SetCharacterAliasButton_Click( object sender, EventArgs e )
		{
			int currentCharacterIndex = CharacterListDropdown.SelectedIndex;
			if( currentCharacterIndex > -1 && currentCharacterIndex < mCharacterFolderList.Length )
			{
				string characterFolderName = mCharacterFolderList[CharacterListDropdown.SelectedIndex].Split( '\\' ).Last();
				string input = Interaction.InputBox( "Input the alias (friendly name) that you wish to use for the character " + characterFolderName, "Set Alias", mSettings.CharacterAliasSettings.GetAlias( characterFolderName ) );
				if( input.Length > 0 )
				{
					mSettings.CharacterAliasSettings.SetAlias( mCharacterFolderList[CharacterListDropdown.SelectedIndex].Split( '\\' ).Last(), input );
					PopulateCharacterLists();
					CharacterListDropdown.SelectedIndex = currentCharacterIndex;
				}
			}
		}
	}
}
