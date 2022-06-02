using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Skyline.DataMiner.Scripting;

/// <summary>
/// DataMiner QAction Class: Test Signal Generator Table Sets.
/// </summary>
public static class QAction
{
	/// <summary>
	/// The QAction entry point.
	/// </summary>
	/// <param name="protocol">Link with SLProtocol process.</param>
	public static void Run(SLProtocol protocol)
	{
		try
		{
			var workloadId = protocol.RowKey();

			var triggerPid = protocol.GetTriggerParameter();

			var value = Convert.ToString(protocol.GetParameter(triggerPid));

			var formData = string.Empty;

			if (triggerPid == Parameter.Write.testsignalgeneratorscolor_3055)
			{
				formData = "{\"Color\":\"" + value + "\"}";
			}
			else if (triggerPid == Parameter.Write.testsignalgeneratorsfrequency_3056)
			{
				formData = "{\"Frequency\":\""+ value + "\"}";
			}
			else if (triggerPid == Parameter.Write.testsignalgeneratorsident_3057)
			{
				formData = "{\"Ident\":\"" + value + "\"}";
			}
			else if (triggerPid == Parameter.Write.testsignalgeneratorsidentoverlay_3058)
			{
				formData = "{\"IdentOverlay\":\"" + value + "\"}";
			}
			else if (triggerPid == Parameter.Write.testsignalgeneratorspattern_3059)
			{
				formData = "{\"Pattern\":\"" + value + "\"}";
			}
			else if (triggerPid == Parameter.Write.testsignalgeneratorssoundmode_3060)
			{
				formData = "{\"SoundMode\":\"" + value + "\"}";
			}
			else if (triggerPid == Parameter.Write.testsignalgeneratorssyncflash_3061)
			{
				formData = "{\"SyncFlash\":\"" + value + "\"}";
			}
			else if (triggerPid == Parameter.Write.testsignalgeneratorssyncperiod_3062)
			{
				formData = "{\"SyncPeriod\":\"" + value + "\"}";
			}
			else if (triggerPid == Parameter.Write.testsignalgeneratorstonelevel_3063)
			{
				formData = "{\"ToneLevel\":\"" + value + "\"}";
			}
			else if (triggerPid == Parameter.Write.testsignalgeneratorstodoverlay_3064)
			{
				formData = "{\"TodOverlay\":\"" + value + "\"}";
			}
			else if (triggerPid == Parameter.Write.testsignalgeneratorsinfooverlay_3065)
			{
				formData = "{\"InfoOverlay\":\"" + value + "\"}";
			}

			var command = new GenericCommand("TestSignalGenerator", "control", workloadId, formData, "AMPP-Control-Service-Key");

			protocol.SetParameter(Parameter.genericcommandcontrolbody_63, JsonConvert.SerializeObject(command));

			protocol.CheckTrigger(44);
		}
		catch (Exception ex)
		{
			protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
		}
	}

	public class GenericCommand
	{
		public GenericCommand(string application, string command, string workload, string formData, string reconKey)
		{
			Application = application;
			Command = command;
			Workload = workload;
			FormData = formData;
			ReconKey = reconKey;
		}

		[JsonProperty("application")]
		public string Application { get; set; }

		[JsonProperty("command")]
		public string Command { get; set; }

		[JsonProperty("workload")]
		public string Workload { get; set; }

		[JsonProperty("formData")]
		public string FormData { get; set; }

		[JsonProperty("reconKey")]
		public string ReconKey { get; set; }
	}
}