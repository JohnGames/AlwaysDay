using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityModManagerNet;
using System.Reflection;
using static UnityModManagerNet.UnityModManager;
using System.IO;
using System.Collections;
using UnityEngine.Video;
using System.Reflection.Emit;

namespace AlwaysDay
{
	public class Settings : UnityModManager.ModSettings, IDrawable
	{

		[Draw("Time",Precision = 3,Min = 0, Max = 1,Type = DrawType.Slider)] public float Time = 0.5f;

		public override void Save(UnityModManager.ModEntry modEntry)
		{
			Save(this, modEntry);
		}

		public void OnChange()
		{
		}
	}

	static class Main
	{
		public static Settings settings;

		// Send a response to the mod manager about the launch status, success or not.
		static bool Load(ModEntry modEntry)
		{
			settings = Settings.Load<Settings>(modEntry);
			modEntry.OnToggle = OnToggle;
			modEntry.OnUpdate = OnUpdate;
			modEntry.OnGUI = OnGUI;
			modEntry.OnSaveGUI = OnSaveGUI;

			if (modEntry.GameVersion != gameVersion)
			{
				UnityModManager.Logger.Log($"{modEntry.Info.DisplayName} expects {modEntry.GameVersion} but found {gameVersion}.");
			}
			return true; // If false the mod will show an error.
		}

		static void OnGUI(UnityModManager.ModEntry modEntry)
		{
			settings.Draw(modEntry);
		}

		static void OnSaveGUI(UnityModManager.ModEntry modEntry)
		{
			settings.Save(modEntry);
		}

		static bool OnToggle(UnityModManager.ModEntry modEntry, bool value /* active or inactive */)
		{
			modEntry.Enabled = value;
			return true;
		}

		static void OnUpdate(UnityModManager.ModEntry modEntry, float dt)
		{
			if (NotNullCheck)
			{
				DayNightManager.Instance.SetTime(settings.Time);
			}
		}

		static bool NotNullCheck
		{
			get
			{
				return DayNightManager.Instance != null;
			}
		}
	}


}

