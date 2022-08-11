using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;

using Skyline.DataMiner.Scripting;

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
			protocol.Log("QA" + protocol.QActionID + "|@@@@@@@@@@@@@@@|Start of QACTION!!!!!!!!!!!!!!!!", LogType.Information, LogLevel.NoLogging);

			using (var client = new HttpClient())
			{
				Uri loginUri = new Uri(string.Format("http://localhost:5002/api/Forwarder/pushworkloadnotification/{0}", protocol.RowKey()));

				StringContent loginBodyContent = new StringContent(String.Empty, Encoding.ASCII);

				var loginResponse = client.PostAsync(loginUri, loginBodyContent).Result;

				if (!loginResponse.IsSuccessStatusCode)
				{
					protocol.Log("QA" + protocol.QActionID + "|Run|The subscribe workload request failed " + loginResponse.IsSuccessStatusCode, LogType.Information, LogLevel.NoLogging);
					return;
				}

				var loginResponseData = loginResponse.Content.ReadAsStringAsync().Result;

				protocol.Log("QA" + protocol.QActionID + "|Run|Response from the subscribe to workload was " + loginResponseData, LogType.Information, LogLevel.NoLogging);
			}
		}
		catch (Exception ex)
		{
			protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
		}
	}
}