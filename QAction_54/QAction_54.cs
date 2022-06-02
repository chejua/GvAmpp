using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

using Skyline.DataMiner.Scripting;
using Skyline.Protocol.Extensions;

/// <summary>
/// DataMiner QAction Class: Parse Token.
/// </summary>
public static class QAction
{
	/// <summary>
	/// The QAction entry point.
	/// </summary>
	/// <param name="protocol">Link with SLProtocol process.</param>
	public static void Run(SLProtocol protocol, object data)
	{
		try
		{
			JObject response = (JObject) JsonConvert.DeserializeObject((string)data);

			if (response.GetFieldAsString("error") != null)
			{
				protocol.SetParameter(Parameter.loginstatus_53, (int) GeneralStatusEnumeration.ERROR);
				protocol.SetParameter(Parameter.debug_51, response.GetFieldAsString("error"));
				protocol.SetParameter(Parameter.token_55, string.Empty);
				return;
			}
			protocol.SetParameter(Parameter.loginstatus_53, (int) GeneralStatusEnumeration.OK);
			protocol.SetParameter(Parameter.token_55, "Bearer " + response.GetFieldAsString("access_token"));
			protocol.SetParameter(
				Parameter.tokenexpiration_56,
				Convert.ToInt32(response.GetFieldAsString("expires_in")));
			protocol.CheckTrigger(3);
		}
		catch (Exception ex)
		{
			protocol.Log(string.Format(">>>QA{0} Exception {1} with content {2}", protocol.QActionID, ex.Message, data),
				   LogType.Error,
				   LogLevel.LogEverything);
		}
	}
}