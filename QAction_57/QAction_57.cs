using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skyline.DataMiner.Scripting;
using Skyline.Protocol.Extensions;
using Skyline.Protocol.Models;

/// <summary>
/// DataMiner QAction Class: Parse Applications.
/// </summary>
public static class QAction
{
	/// <summary>
	/// The QAction entry point.
	/// </summary>
	/// <param name="protocol">Link with SLProtocol process.</param>
	public static void Run(SLProtocolExt protocol, object data)
	{
		try
		{
			var applications = JsonConvert.DeserializeObject<AmppApplication[]>(data as string);
			var rows = new List<QActionTableRow>();
			foreach (var application in applications)
			{
				rows.Add(
					new ApplicationtypesQActionRow
					{
						Applicationtypename_1001 = application.name,
						Needsaving_1002 = application.needsSaving,
						Supportedcommands_1003 = JsonConvert.SerializeObject(application.commands),
					});
			}
			protocol.applicationtypes.FillArray(rows);
			protocol.SetParameter(Parameter.debug_51, data);
		}
		catch (Exception ex)
		{
			protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
		}
	}
}