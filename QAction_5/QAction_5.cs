using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using Skyline.DataMiner.Scripting;
using Skyline.Protocol.Extensions;
using Skyline.Protocol.MessageProcessing;

/// <summary>
/// DataMiner QAction Class: Send Enabled Workloads Get State After Restart.
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
			// Get workloads
			var workloadIdGetState = protocol.GetColumnAsDictionary<string, int>(Parameter.Workloads.tablePid, Parameter.Workloads.Idx.workloadid_2001, Parameter.Workloads.Idx.workloadsrefreshstateonrestart_2006);

			foreach (var workload in workloadIdGetState)
			{
				if (!workload.Value.Equals(1))
				{
					continue;
				}

				var httpHelper = new HttpHelper(protocol, workload.Key);
				httpHelper.SendHttpQuery();
				Thread.Sleep(1000);
			}

			// Setting flag to one so it only does this once after getting the workloads
			protocol.SetParameter(Parameter.getstateflagcompleted_4, 1);
		}
		catch (Exception ex)
		{
			protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
		}
	}
}