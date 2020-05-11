using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CharacterDataManager
{
	class Config
	{
		public Config()
		{
			//	Get the path to the settings folder.
			ConfigFolderPath = Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ) + "\\PunishedPineapple\\CharacterDataManager\\";

			//	Create the children.
			ProgramSettings = new ProgramConfig( ConfigFolderPath + "Options.cfg" );
			FileAliasSettings = new FileAliasConfig( ConfigFolderPath + "FileAliases.cfg" );

			//	The alias file lives one directory up since we may want to share it with other programs.
			CharacterAliasSettings = new CharacterAliasConfig( Directory.GetParent( ConfigFolderPath ).Parent.FullName + "\\CharacterAliases.cfg" );
		}

		public void SaveConfig()
		{
			//	Create the directories that hold the config files if they don't exist.
			Directory.CreateDirectory( ConfigFolderPath );

			//	Ask our children to save themselves.  The children are expected to back up old files on their own if applicable.
			ProgramSettings.SaveConfig();
			FileAliasSettings.SaveConfig();
			CharacterAliasSettings.SaveConfig();
		}

		public ProgramConfig ProgramSettings { get; protected set; }
		public FileAliasConfig FileAliasSettings { get; protected set; }
		public CharacterAliasConfig CharacterAliasSettings { get; protected set; }
		public string ConfigFolderPath { get; protected set; }
	}
}
