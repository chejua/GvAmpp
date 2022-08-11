using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;

using Skyline.DataMiner.Scripting;
using Skyline.Protocol.MessageProcessing;

/// <summary>
/// DataMiner QAction Class: Other On Workload GetState.
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
			var httpHelper = new HttpHelper(protocol, protocol.RowKey());
			httpHelper.SendHttpQuery();
		}
		catch (Exception ex)
		{
			protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
		}
	}
}