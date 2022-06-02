using Skyline.DataMiner.Scripting;
using Skyline.Protocol.MessageProcessing;
using System;

/// <summary>
/// DataMiner QAction Class: Process Message From AMPP.
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
			var messageReceiver = new MessageReceiver(protocol);
			var messageHandlerMappings = new MessageHandlerMappings(protocol);
			var messageHandler = messageHandlerMappings.GetMessageHandler(messageReceiver.Path, messageReceiver.Body);
			messageHandler.HandleMessage();
		}
		catch (Exception ex)
		{
			protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
		}
	}
}