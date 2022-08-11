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
			var workloadIdGetState = protocol.GetColumnAsDictionary<string, int>(Parameter.Workloads.tablePid, Parameter.Workloads.Idx.workloadid_2001, Parameter.Workloads.Idx.workloadsrefreshstateonrestart_2006);

			var amppWorkloads = JsonConvert.DeserializeObject<AmppWorkloadRoot>(Convert.ToString(data));

			List<QActionTableRow> tableRows = new List<QActionTableRow>();

			for (int i = 0; i < amppWorkloads.workloads.Length; i++)
			{

				var workloadId = amppWorkloads.workloads[i].workload.id;

				int getStateConfigured;
				if (!workloadIdGetState.TryGetValue(workloadId, out getStateConfigured))
				{
					// New workload was added.
				}

				tableRows.Add(new WorkloadsQActionRow
				{
					Workloadid_2001 = workloadId,
					Workloadname_2002 = amppWorkloads.workloads[i].workload.name,
					Workloadapplicationname_2003 = amppWorkloads.workloads[i].workload.applicationName,
					Applicationpackagename_2004 = amppWorkloads.workloads[i].workload.packageName,
					Applicationpackageversion_2005 = amppWorkloads.workloads[i].workload.packageVersion,
					Workloadsrefreshstateonrestart_2006 = getStateConfigured,
				});
			}

			protocol.workloads.FillArray(tableRows);
			protocol.SetParameter(Parameter.debug_51, data);
		}
		catch (Exception ex)
		{
			protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
		}
	}
}