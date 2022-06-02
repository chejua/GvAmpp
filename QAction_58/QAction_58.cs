using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Skyline.Protocol.Extensions;
using Skyline.Protocol.Models;
using Skyline.DataMiner.Scripting;
using Newtonsoft.Json;
using System.Linq;
/// <summary>
/// DataMiner QAction Class: Parse Workloads.
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
			var amppWorkloads = JsonConvert.DeserializeObject<AmppWorkloadRoot>((string)data);
			var rows = amppWorkloads.workloads
				.Select(
					w => new WorkloadsQActionRow
					{
						Workloadid_2001 = w.workload.id,
						Workloadname_2002 = w.workload.name,
						Workloadapplicationname_2003 = w.workload.applicationName,
						Applicationpackagename_2004 = w.workload.packageName,
						Applicationpackageversion_2005 = w.workload.packageVersion,
						Workloadgetstate_2070 = 0
					})
				.ToArray();
			List<QActionTableRow> tableRows = new List<QActionTableRow>(rows);
			protocol.workloads.FillArray(tableRows);
			protocol.SetParameter(Parameter.debug_51, data);
		}
		catch (Exception ex)
		{
			protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
		}
	}
}