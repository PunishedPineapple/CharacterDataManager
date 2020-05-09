using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CharacterDataManager
{
	class ProgramConfig
	{
		public ProgramConfig( string configFilePath )
		{
			ConfigFilePath = configFilePath;
			DefaultSelectedFiles = new List<string>();
			DefaultSelectedTargetFolders = new List<string>();
			SetDefaultConfig();
			ReadSavedConfig();
		}

		protected void SetDefaultConfig()
		{
			CharacterDataFolderPath = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments ) + "\\My Games\\FINAL FANTASY XIV - A Realm Reborn\\";
			DefaultCharacterID = "";
			ShowInitialWarning = true;
			DefaultSelectedFiles.Add( "COMMON.DAT" );
			DefaultSelectedFiles.Add( "CONTROL0.DAT" );
			DefaultSelectedFiles.Add( "CONTROL1.DAT" );
			DefaultSelectedFiles.Add( "GS.DAT" );
			DefaultSelectedFiles.Add( "HOTBAR.DAT" );
			DefaultSelectedFiles.Add( "KEYBIND.DAT" );
			DefaultSelectedFiles.Add( "LOGFLTR.DAT" );
			DefaultSelectedFiles.Add( "MACRO.DAT" );
		}

		protected void ReadSavedConfig()
		{
			//	Read the config if we have it.
			if( File.Exists( ConfigFilePath ) )
			{
				List<string> lines = File.ReadLines( ConfigFilePath ).ToList();
				foreach( string line in lines )
				{
					if( line.Split( '=' ).First().Trim().Equals( "CharacterDataFolderPath" ) )		CharacterDataFolderPath = line.Split( '=' ).Last().Trim();
					if( line.Split( '=' ).First().Trim().Equals( "DefaultCharacterID" ) )			DefaultCharacterID = line.Split( '=' ).Last().Trim();
					if( line.Split( '=' ).First().Trim().Equals( "ShowInitialWarning" ) )			ShowInitialWarning = bool.Parse( line.Split( '=' ).Last().Trim() );

					if( line.Split( '=' ).First().Trim().Equals( "DefaultSelectedFiles" ) )
					{
						DefaultSelectedFiles.Clear();
						foreach( string str in line.Split( '=' ).Last().Trim().Split( ',' ) )
						{
							if( str.Trim().Length > 0 ) DefaultSelectedFiles.Add( str.Trim() );
						}
					}

					if( line.Split( '=' ).First().Trim().Equals( "DefaultSelectedTargetFolders" ) )
					{
						DefaultSelectedTargetFolders.Clear();
						foreach( string str in line.Split( '=' ).Last().Trim().Split( ',' ) )
						{
							if( str.Trim().Length > 0 ) DefaultSelectedTargetFolders.Add( str.Trim() );
						}
					}
				}
			}
		}

		public void SaveConfig()
		{
			if( Directory.Exists( Path.GetDirectoryName( ConfigFilePath ) ) )
			{
				string cfgString = "";
				cfgString += "CharacterDataFolderPath" + " = " + CharacterDataFolderPath + "\r\n";
				cfgString += "DefaultCharacterID" + " = " + DefaultCharacterID + "\r\n";
				cfgString += "ShowInitialWarning" + " = " + ShowInitialWarning.ToString() + "\r\n";

				cfgString += "DefaultSelectedFiles" + " = ";
				foreach( string str in DefaultSelectedFiles )
				{
					cfgString += str + ',';
				}
				cfgString += "\r\n";

				cfgString += "DefaultSelectedTargetFolders" + " = ";
				foreach( string str in DefaultSelectedTargetFolders )
				{
					cfgString += str + ',';
				}
				cfgString += "\r\n";

				if( File.Exists( ConfigFilePath ) ) File.Copy( ConfigFilePath, ConfigFilePath + ".bak", true );
				File.WriteAllText( ConfigFilePath, cfgString );
			}
		}

		public string CharacterDataFolderPath { get; set; }
		public string DefaultCharacterID { get; set; }
		public bool ShowInitialWarning { get; set; }
		public List<string> DefaultSelectedFiles { get; set; }
		public List<string> DefaultSelectedTargetFolders { get; set; }
		protected string ConfigFilePath { get; set; }
	}
}
