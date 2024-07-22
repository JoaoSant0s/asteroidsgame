using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using UnityEditor;

namespace Manifesto.Tools {
	public struct ProjectVersion {
		const string ProjectVersionFile = "ProjectSettings/version.txt";

		static readonly Regex VersionRegex = new Regex(@"^(?<major>\d+)\.(?<minor>\d+)\.(?<patch>\d+)$");

		static bool ParseValue(Match match, string group, out ushort value) {
			value = 0;

			var g = match.Groups[group];
			if (!g.Success) { return false; }

			return ushort.TryParse(g.Value, out value);
		}

		public static ProjectVersion? Parse(string str) {
			if (string.IsNullOrEmpty(str)) { return null; }

			var match = VersionRegex.Match(str);

			if (!match.Success) { return null; }

			ushort major;
			if (!ParseValue(match, "major", out major)) { return null; }

			ushort minor;
			if (!ParseValue(match, "minor", out minor)) { return null; }

			ushort patch;
			if (!ParseValue(match, "patch", out patch)) { return null; }

			return new ProjectVersion {
				major = major,
				minor = minor,
				patch = patch
			};
		}

		public static ProjectVersion? ReadFromFile() {
			return File.Exists(ProjectVersionFile) ? Parse(File.ReadAllText(ProjectVersionFile)) : null;
		}

		public static void WriteToFile(ProjectVersion version) {
			File.WriteAllText(ProjectVersionFile, version.ToString(), Encoding.UTF8);
		}

		public static void ApplyToSettings(ProjectVersion version) {
			PlayerSettings.bundleVersion = version.GetBuildVersion();
			PlayerSettings.iOS.buildNumber = version.GetIOSBuildNumber();
			PlayerSettings.Android.bundleVersionCode = version.GetAndroidVersionCode();
			AssetDatabase.SaveAssets();
		}
		
		ushort major;
		ushort minor;
		ushort patch;

		public void BumpMajor() {
			major++;
			minor = 0;
			patch = 0;
		}

		public void BumpMinor() {
			minor++;
			patch = 0;
		}

		public void BumpPatch() {
			patch++;
		}

		public override string ToString() {
			return string.Format("{0}.{1}.{2}", major, minor, patch);
		}

		string GetBuildVersion() {
			return string.Format("{0}.{1}.{2}", major, minor, patch);
		}

		string GetIOSBuildNumber() {
			return ToString();
		}

		int GetAndroidVersionCode() {
			const int AndroidVersionCodeSize = 30;
			const int MajorSize = 10;
			const int MinorSize = 10;
			const int PatchSize = AndroidVersionCodeSize - MinorSize - MajorSize;

			const int MajorOffset = MinorSize + PatchSize;
			const int MinorOffset = PatchSize;

			return (
				(major << MajorOffset) |
				(minor << MinorOffset) |
				patch
			);
		}
	}
}
