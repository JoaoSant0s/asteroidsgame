using System.Linq;

using UnityEngine;
using UnityEditor;

namespace Manifesto.Tools {
	static class ToolsMenu {
		#region ProjectVersion
		[MenuItem("Project Version/Bump Major", false, 1)]
		static void BumpMajorVersion() {
			var version = ProjectVersion.ReadFromFile() ?? new ProjectVersion();

			version.BumpMajor();

			ProjectVersion.ApplyToSettings(version);
			ProjectVersion.WriteToFile(version);

			Debug.LogFormat("Bumped version to {0}", version);
		}

		[MenuItem("Project Version/Bump Minor", false, 2)]
		static void BumpMinorVersion() {
			var version = ProjectVersion.ReadFromFile() ?? new ProjectVersion();

			version.BumpMinor();

			ProjectVersion.ApplyToSettings(version);
			ProjectVersion.WriteToFile(version);

			Debug.LogFormat("Bumped version to {0}", version);
		}

		[MenuItem("Project Version/Bump Patch", false, 3)]
		static void BumpPatchVersion() {
			var version = ProjectVersion.ReadFromFile() ?? new ProjectVersion();

			version.BumpPatch();

			ProjectVersion.ApplyToSettings(version);
			ProjectVersion.WriteToFile(version);

			Debug.LogFormat("Bumped version to {0}", version);
		}

		[MenuItem("Project Version/Apply to Settings", false, 14)]
		static void ApplyVersion() {
			var version = ProjectVersion.ReadFromFile() ?? new ProjectVersion();

			ProjectVersion.ApplyToSettings(version);
		}
		#endregion	
	}
}
