using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Skyline.DataMiner.Scripting;

/// <summary>
/// DataMiner QAction Class: After Startup.
/// </summary>
public static class QAction
{
    /// <summary>
    /// The QAction entry point.
    /// </summary>
    /// <param name="protocol">Link with SLProtocol process.</param>
    public static void Run(SLProtocolExt protocol)
    {
		try
		{
			if (!protocol.IsEmpty(Parameter.apitoken_50))
			{
				protocol.SetParameter(Parameter.authorizationapikey_52, "Basic " + Convert.ToString(protocol.GetParameter(Parameter.apitoken_50)));
				protocol.SetParameter(Parameter.loginstatus_53, 0.0);
				protocol.CheckTrigger(2);
			}

			protocol.SetParameter(Parameter.messagequeue_61, string.Empty);
		}
		catch (Exception ex)
		{
			protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
		}
    }
}
