using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.AccessControl;

namespace CharacterDataManager
{
	class FileAliasConfig
	{
		public FileAliasConfig( string configFilePath )
		{
			mAliases = new Dictionary<string, string>();
			ConfigFilePath = configFilePath;
			SetDefaultConfig();
			ReadSavedConfig();
		}
		protected void SetDefaultConfig()
		{
			mAliases.Add( "ACQ.DAT", "Tell Contact History" );
			mAliases.Add( "ADDON.DAT", "HUD Layout" );
			mAliases.Add( "COMMON.DAT", "Basic Settings" );
			mAliases.Add( "CONTROL0.DAT", "KB/M-Mode Settings" );
			mAliases.Add( "CONTROL1.DAT", "Gamepad-Mode Settings" );
			mAliases.Add( "GEARSET.DAT", "Gear Sets" );
			mAliases.Add( "GS.DAT", "Gold Saucer Settings" );
			mAliases.Add( "HOTBAR.DAT", "Hotbar Assignments" );
			mAliases.Add( "ITEMFDR.DAT", "Item Search Cache" );
			mAliases.Add( "ITEMODR.DAT", "Item Sort Order" );
			mAliases.Add( "KEYBIND.DAT", "Keybinds" );
			mAliases.Add( "LOGFLTR.DAT", "Chat Log Tab Filters" );
			mAliases.Add( "MACRO.DAT", "Character Macros" );
			mAliases.Add( "UISAVE.DAT", "Mail/Teleport History, CWLS Order, Waymarks, etc." );
		}

		protected void ReadSavedConfig()
		{
			if( File.Exists( ConfigFilePath ) )
			{
				List<string> lines = File.ReadLines( ConfigFilePath ).ToList();
				foreach( string line in lines )
				{
					string key = line.Split( '=' ).First().Trim();
					string value = line.Split( '=' ).Last().Trim();
					if( mAliases.ContainsKey( key ) )
					{
						mAliases[key] = value;
					}
					else
					{
						mAliases.Add( key, value  );
					}
				}
			}
		}

		public void SaveConfig()
		{
			if( Directory.Exists( Path.GetDirectoryName( ConfigFilePath ) ) )
			{
				string cfgString = "";
				foreach( KeyValuePair<string, string> entry in mAliases )
				{
					cfgString += entry.Key + " = " + entry.Value + "\r\n";
				}
				if( File.Exists( ConfigFilePath ) ) File.Copy( ConfigFilePath, ConfigFilePath + ".bak", true );
				File.WriteAllText( ConfigFilePath, cfgString );
			}
		}

		public string GetAlias( string fileName )
		{
			string alias = "";
			if( mAliases.TryGetValue( fileName, out alias ) ) return alias;
			else return fileName;
		}

		public void SetAlias( string fileName, string alias )
		{
			if( mAliases.ContainsKey( fileName ) )
			{
				mAliases[fileName] = alias;
			}
			else
			{
				mAliases.Add( fileName, alias );
			}
		}

		protected Dictionary<string, string> mAliases;
		protected string ConfigFilePath { get; set; }
	}
}
