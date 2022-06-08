namespace Skyline.Protocol
{
	using Newtonsoft.Json.Linq;
	using System;
	using System.Reflection;
	using System.Linq;
	using System.Collections.Generic;
	using Skyline.Protocol.Models;

	namespace Extensions
	{
		#region Structs
		#endregion

		#region Classes
		public static class MyExtensions
		{
			public static string GetFieldAsString(this JObject json, string fieldName)
			{
				if (json.ContainsKey(fieldName))
					return json[fieldName].ToString();
				return null;
			}

			public static int GetTokenHashCode(this JToken token)
			{ return string.Join("-", token.Children().Select(c => c.Path).OrderBy(c => c).ToArray()).GetHashCode(); }
		}

		public static class NotificationMessageBuilder
		{
			#region Fields
			private static Dictionary<int, Type> KnownTypes = new Dictionary<int, Type>
			{
				{
					NotificationMessageBuilder.GetTypeHashCode<SignalGeneratorSectionAPayload>(),
					typeof(SignalGeneratorSectionAPayload)
				},
				{
					NotificationMessageBuilder.GetTypeHashCode<SignalGeneratorSectionBPayload>(),
					typeof(SignalGeneratorSectionBPayload)
				},
				{
					NotificationMessageBuilder.GetTypeHashCode<PingPayLoad>(),
					typeof(PingPayLoad)
				},
				{
					NotificationMessageBuilder.GetTypeHashCode<MultiviewerSectionAPayload>(),
					typeof(MultiviewerSectionAPayload)
				},

			};
			#endregion

			#region Public methods
			public static int GetTypeHashCode<T>()
			{
				return string.Join(
					"-",
					typeof(T).GetMembers()
						.Where(m => m.MemberType == MemberTypes.Property)
						.Select(m => m.Name)
						.OrderBy(m => m)
						.ToArray())
					.GetHashCode();
			}

			public static bool TryGetTypeFromHashCode(int hashcode, out Type type)
			{
				type = typeof(object);
				return KnownTypes.TryGetValue(hashcode, out type);
			}

			public static bool TryGetObjectFromToken(JToken token, out object result)
			{
				result = null;
				if (token == null)
				{
					return false;
				}
				try
				{
					if (NotificationMessageBuilder.TryGetTypeFromHashCode(token.GetTokenHashCode(), out Type type))
					{
						result = token.ToObject(type);
						return true;
					}
				}
				catch (Exception)
				{
				}
				return false;
			}
			#endregion
		}
		#endregion

		#region Enumerations
		public enum GeneralStatusEnumeration
		{
			NA,
			OK,
			ERROR
		}

;
		#endregion
	}

	namespace Models
	{
		public class AmppApplication
		{
			#region Public properties
			public AmppCommand[] commands { get; set; }

			public string name { get; set; }

			public bool needsSaving { get; set; }
			#endregion
		}

		public class AmppCommand
		{
			#region Public properties
			public string icon { get; set; }

			public string markdown { get; set; }

			public string name { get; set; }

			public string schema { get; set; }

			public string version { get; set; }
			#endregion
		}

		public class AmppWorkload
		{
			#region Public properties
			public string eTag { get; set; }

			public Workload workload { get; set; }
			#endregion
		}

		public class AmppWorkloadRoot
		{
			#region Public properties
			public AmppWorkload[] workloads { get; set; }
			#endregion
		}

		public class Billingproperties
		{
			#region Public properties
			public string ExecutionType { get; set; }

			public object SubscriptionGroup { get; set; }
			#endregion
		}

		public class Desiredstate
		{
			#region Public properties
			public Startproperties startProperties { get; set; }

			public string state { get; set; }
			#endregion
		}

		#region Notifications
		public class NotificationObject
		{
			public JToken payload { get; set; }

			public string workload { get; set; }
		}

		public class PingPayLoad
		{
			#region Public properties
			public string Application { get; set; }

			public string Success { get; set; }

			public string Time { get; set; }

			public string Workload { get; set; }
			#endregion
		}

		public class Properties
		{
			#region Public properties
			public string apikey { get; set; }

			public string assetmanage { get; set; }

			public string awsaccesskeyid { get; set; }

			public string awss3region { get; set; }

			public string awssecretkey { get; set; }

			public string framecachehistoryduration { get; set; }

			public string framecacheport { get; set; }

			public string historyduration { get; set; }

			public string id { get; set; }

			public string platformapikey { get; set; }

			public string port { get; set; }

			public object receivinginterfaces { get; set; }

			public string s3apikey { get; set; }

			public object s3apiKey { get; set; }

			public string s3bandwidththrottling { get; set; }

			public string s3readratelimit { get; set; }

			public string s3region { get; set; }

			public string s3secretkey { get; set; }

			public string sdiinput { get; set; }

			public string usegpu { get; set; }
			#endregion
		}

		public class SignalGeneratorSectionAPayload
		{
			#region Public properties
			public string FrameRate { get; set; }

			public string Resolution { get; set; }

			public string ScanMode { get; set; }
			#endregion
		}

		public class SignalGeneratorSectionBPayload
		{
			#region Public properties
			public string Color { get; set; }

			public string Frequency { get; set; }

			public string Ident { get; set; }

			public string IdentOverlay { get; set; }

			public string InfoOverlay { get; set; }

			public string Pattern { get; set; }

			public string SoundMode { get; set; }

			public string SyncFlash { get; set; }

			public string SyncPeriod { get; set; }

			public string TodOverlay { get; set; }

			public string ToneLevel { get; set; }
			#endregion
		}

		public class MultiviewerSectionAPayload
		{
			public string AudioLevelMode { get; set; }
			public int AudioRmsWindowMs { get; set; }
		}


		#endregion

		public class Startproperties
		{
		}

		public class State
		{
			#region Public properties
			public string applicationVersion { get; set; }

			public object errorReason { get; set; }

			public object errorReasonCode { get; set; }

			public bool isWorkloadAssigned { get; set; }

			public string nodeId { get; set; }

			public string nodeName { get; set; }

			public string packageVersion { get; set; }

			public string state { get; set; }
			#endregion
		}

		public class Tags
		{
		}

		public class Workload
		{
			#region Public properties
			public string applicationName { get; set; }

			public string applicationVersion { get; set; }

			public string applicationVersionType { get; set; }

			public Billingproperties billingProperties { get; set; }

			public string configurationPath { get; set; }

			public Desiredstate desiredState { get; set; }

			public string fabricId { get; set; }

			public string id { get; set; }

			public string name { get; set; }

			public object[] packageDependencies { get; set; }

			public string packageName { get; set; }

			public string[] packagePlacementConstraints { get; set; }

			public string[] packageSupportedOS { get; set; }

			public string packageVersion { get; set; }

			public string packageVersionType { get; set; }

			public string parentId { get; set; }

			public string[] placementConstraints { get; set; }

			public string productCode { get; set; }

			public Properties properties { get; set; }

			public int startupGroup { get; set; }

			public State state { get; set; }

			public State[] states { get; set; }

			public Tags tags { get; set; }

			public int vru { get; set; }
			#endregion
		}
	}

	namespace MessageProcessing
	{
		using Newtonsoft.Json;
		using Skyline.DataMiner.Net.Messages.Advanced;
		using Skyline.DataMiner.Scripting;
		using Skyline.Protocol.Models;
		using System;
		using System.Collections.Generic;
		using System.Linq;
		using System.Text;
		using SLNetMessages = Skyline.DataMiner.Net.Messages;
		using Skyline.Protocol.Extensions;

		public class AutomationScript
		{
			#region Fields
			private string[] scriptlaunchData;
			#endregion

			#region Public methods
			public static void RunScript(
				SLProtocol protocol,
				Dictionary<string, string> dicScriptParameters,
				string sScriptName)
			{
				var info = new ScriptInfo();
				FillScriptParameters(dicScriptParameters, info);
				var script = new AutomationScript();
				script.ConfigAutomationScript(info);
				script.ExecuteScript(protocol, sScriptName);
			}

			public void ConfigAutomationScript(ScriptInfo info)
			{
				var data = new List<string>
				{
					"CHECKSETS:" + Convert.ToString(info.CheckSets).ToUpper(),
					"DEFER:" + Convert.ToString(!info.WaitUntilEnd).ToUpper(),
				};

				if (info.UserCookie != string.Empty)
				{
					data.Add("USER:" + info.UserCookie);
				}

				foreach (var paramPair in info.ParametersByNameList)
				{
					data.Add("PARAMETERBYNAME:" + paramPair.Key + ":" + paramPair.Value);
				}

				foreach (var protocolPair in info.ProtocolByNameList)
				{
					var splitdmaelemId = protocolPair.Value.Split('/');
					data.Add("PROTOCOLBYNAME:" + protocolPair.Key + ":" + splitdmaelemId[0] + ":" + splitdmaelemId[1]);
				}

				scriptlaunchData = data.ToArray();
				/*
		*                  Elements of the form:
		*                         PROTOCOL:protid:dmaid:eid  Defines a protocol mapping
		*                         MEMORY:id:filename                Defines a memory location mapping
		*                         PARAMETER:id:value                Predefined Execute Result
		*                         EXTRA:id:value                          Extra magic parameters (sent to us from e.g. correlation)
		*                         DEFER:True/False
		*                                boolean indicating if the Execute method can return from the moment
		*                                the script has been started, or needs to wait until the execution of
		*                               the script has completed
		*                                      TRUE   = don't wait for script to terminate
		*                                      FALSE  = wait for script to terminate
		*                         DYNAMIC:protid:dmaid/eid:dmaid/eid:dmaid/eid...
		*                                same as PROTOCOL:protid:dmaid:eid, with as difference that a series of
		*                                elements is given. The first element of this list which matches the
		*                                correct protocol/version will be used
		*                         USER:cookie
		*                                user executing the script (e.g. systemdisplay user/scheduler/correlation)
		*                         CHECKSETS:true/false
		*                                overrides the "check sets" setting from the script
		*                                TRUE = after setting a parameter, wait for the set to succeed. The "Set Parameter"
		*                                       script action will fail if the associated read parameter does not have the
		*                                         correct value after ~10s
		*                                FALSE = after a "Set Parameter", assume success and continue immediately.
		*                         OPTIONS:xxx
		*                                Where xxx is a bitwise combination of the values from AutomationFlags.h
		*/
			}

			public void ExecuteScript(SLProtocol protocol, string scriptName)
			{
				if (scriptlaunchData != null)
				{
					var scriptMessage = new SLNetMessages.ExecuteScriptMessage(scriptName)
					{
						Options = new SLNetMessages.SA { Sa = scriptlaunchData, },
					};

					protocol.SLNet.SendSingleResponseMessage(scriptMessage);
				}
			}

			public static string[] GetAvailableScripts(SLProtocol protocol, string folder)
			{
				var msg = new GetAutomationInfoMessage(21, string.Empty);
				var response = protocol.SLNet.SendSingleResponseMessage(msg) as GetAutomationInfoResponseMessage;
				if (response != null)
				{
					var folderInfo = response.psaRet.Psa
						.FirstOrDefault(
							p => p.Sa.Length > 0 && p.Sa[0].Equals(folder, StringComparison.InvariantCultureIgnoreCase));
					if (folderInfo != null)
					{
						return folderInfo.Sa.Skip(1).ToArray();
					}
				}

				return new string[0];
			}
			#endregion

			#region Private methods
			private static void FillScriptParameters(Dictionary<string, string> dicScriptParameters, ScriptInfo info)
			{
				foreach (var key in dicScriptParameters.Keys)
				{
					info.ParametersByNameList.Add(key, dicScriptParameters[key]);
				}
			}
			#endregion

			#region Nested Classes
			public class ScriptInfo
			{
				#region Public properties
				public bool CheckSets { get; set; }

				public Dictionary<string, string> ParametersByNameList { get; }

				public Dictionary<string, string> ProtocolByNameList { get; }

				public string UserCookie { get; set; }

				public bool WaitUntilEnd { get; set; }
				#endregion

				#region Constructors
				public ScriptInfo()
				{
					CheckSets = false;
					UserCookie = string.Empty;
					WaitUntilEnd = false;
					ParametersByNameList = new Dictionary<string, string>();
					ProtocolByNameList = new Dictionary<string, string>();
				}
				#endregion
			}
			#endregion
		}

		public class HandleNotificationMessage : IMessageHandler
		{
			#region Fields
			private readonly string message;
			private readonly SLProtocolExt protocol;
			private readonly MessageQueue queue;
			private NotificationObject rootObject;
			#endregion

			#region Constructors
			public HandleNotificationMessage(SLProtocolExt protocol, string message)
			{
				this.protocol = protocol;
				this.message = message;
				queue = new MessageQueue(protocol);
			}
			#endregion

			#region Public methods
			public void HandleMessage()
			{
				// Deserialize the message
				//protocol.Log(
				//	string.Format(
				//		">>>QA{0} HandleMessage processing {1}:{2}",
				//		protocol.QActionID,
				//		System.Environment.NewLine,
				//		message),
				//	LogType.Allways,
				//	LogLevel.LogEverything);
				rootObject = JsonConvert.DeserializeObject<NotificationObject>(message);

				// Validate if the contents are valid
				ValidateMessage();
				var messageResponseSender = new MessageResponseSender(protocol);
				queue.AddMessage(rootObject);
				var body = new HttpResponseBody { Result = "OK", Description = "Notification received", };
				messageResponseSender.SendResponse(body);
				queue.ScheduleProcess();
			}
			#endregion

			#region Private methods
			private void ValidateMessage()
			{
				// Validate correctly or thrown an exception
			}
			#endregion
		}

		public class HttpResponseBody
		{
			#region Public properties
			public string Description { get; set; }

			public string Result { get; set; }
			#endregion
		}

		public class MessageHandlerMappings
		{
			#region Fields
			private readonly SLProtocolExt protocol;
			#endregion

			#region Constructors
			public MessageHandlerMappings(SLProtocolExt protocol) { this.protocol = protocol; }
			#endregion

			#region Public methods
			public IMessageHandler GetMessageHandler(string path, string message)
			{
				var factoryDictionary = new Dictionary<string, Func<IMessageHandler>>
				{ { "/notification", () => new HandleNotificationMessage(protocol, message) }, };

				if (factoryDictionary.ContainsKey(path))
				{
					return factoryDictionary[path].Invoke();
				}

				throw new NotSupportedException("Not Supported request path: " + path);
			}
			#endregion
		}

		public class MessageQueue
		{
			private const int ProcessGamePlanQueue = 100;
			#region Fields
			private readonly SLProtocolExt protocol;
			private List<NotificationObject> queue;
			#endregion

			#region Constructors
			public MessageQueue(SLProtocolExt protocol) { this.protocol = protocol; }
			#endregion

			#region Public methods
			public void AddMessage(NotificationObject notification)
			{
				ReadQueue();
				queue.Add(notification);
				WriteQueue();
			}

			public void Process()
			{
				ReadQueue();
				if (queue.Count == 0)
				{
					return;
				}

				var notification = queue[0];
				queue.RemoveAt(0);
				ExecuteMessage(notification);
				if (queue.Count > 0)
				{
					ScheduleProcess();
				}

				WriteQueue();
			}

			public void ScheduleProcess() { protocol.CheckTrigger(ProcessGamePlanQueue); }
			#endregion

			#region Private methods
			private void ExecuteMessage(NotificationObject notification)
			{
				protocol.Log(string.Format(">>>QA{0} The workload {1} has sent this notification:\n\r{2}",
		protocol.QActionID,
		notification.workload,
		JsonConvert.SerializeObject(notification, Formatting.None)),
								 LogType.Allways,
								 LogLevel.LogEverything);

				if (NotificationMessageBuilder.TryGetObjectFromToken(notification.payload, out object result))
				{
					if (result is SignalGeneratorSectionAPayload)
					{
						ExecuteActionBaseOnWorkLoadType(notification.workload, result);
					}
					else if (result is SignalGeneratorSectionBPayload)
					{
						if (protocol.testsignalgenerators.Exists(notification.workload))
						{
							var bPayload = result as SignalGeneratorSectionBPayload;

							protocol.testsignalgenerators[notification.workload] = new TestsignalgeneratorsQActionRow
							{
								Testsignalgeneratorscolor_3005 = bPayload.Color,
								Testsignalgeneratorsfrequency_3006 = bPayload.Frequency,
								Testsignalgeneratorsident_3007 = bPayload.Ident,
								Testsignalgeneratorsidentoverlay_3008 = bPayload.IdentOverlay,
								Testsignalgeneratorsinfooverlay_3015 = bPayload.InfoOverlay,
								Testsignalgeneratorspattern_3009 = bPayload.Pattern,
								Testsignalgeneratorssoundmode_3010 = bPayload.SoundMode,
								Testsignalgeneratorssyncflash_3011 = bPayload.SyncFlash,
								Testsignalgeneratorssyncperiod_3012 = bPayload.SyncPeriod,
								Testsignalgeneratorstonelevel_3013 = bPayload.ToneLevel,
								Testsignalgeneratorstodoverlay_3014 = bPayload.TodOverlay,
							};
						}
					}

					protocol.Log(string.Format(">>>QA{0} ExecuteMessage succesfully deserialized the object:\n\r {1}", protocol.QActionID, JsonConvert.SerializeObject(result, Formatting.Indented)),
					 LogType.Allways,
					 LogLevel.LogEverything);
				}
				else
				{
					protocol.Log(string.Format(">>>QA{0} ExecuteMessage couldn't define the object:\n\r {1}", protocol.QActionID, JsonConvert.SerializeObject(notification, Formatting.Indented)),
					 LogType.Allways,
					 LogLevel.LogEverything);
				}
			}

			private void ExecuteActionBaseOnWorkLoadType(string id, object result)
			{
				var workLoadRow =  new WorkloadsQActionRow((object[])protocol.GetRow(Parameter.Workloads.tablePid, id));

				if (workLoadRow.Workloadapplicationname_2003 is "Test Signal Generator")
					MapTestSignalGenerator(id, (SignalGeneratorSectionAPayload) result);
				else
					throw new NotSupportedException("Workload of type " + workLoadRow.Workloadapplicationname_2003 +
					                                " not yet supported");
			}

			private void MapTestSignalGenerator(string id, SignalGeneratorSectionAPayload sectionA)
			{
				var workloadname = protocol.GetParameterIndexByKey(Parameter.Workloads.tablePid, id, 2) as string;
				TestsignalgeneratorsQActionRow row = new TestsignalgeneratorsQActionRow
				{
					Signalgeneratorsinstance_3001 = id,
					Testsignageneratorsworkloadname_3016 = workloadname,
					Framerate_3002 = sectionA.FrameRate,
					Testsignalgeneratorsresolution_3003 = sectionA.Resolution,
					Testsignalgeneratorsscanmode_3004 = sectionA.ScanMode,
				};
				protocol.testsignalgenerators.AddRow(row);
			}

			private void ReadQueue()
			{
				var rawQueue = Convert.ToString(protocol.GetParameter(Parameter.messagequeue_61));
				try
				{
					queue = JsonConvert.DeserializeObject<List<NotificationObject>>(rawQueue);
					queue = queue ?? new List<NotificationObject>();
				}
				catch (Exception e)
				{
					protocol.Log(
						"QA" + protocol.QActionID + "|MessageQueue.ReadQueue|Error: " + e.Message,
						LogType.Error,
						LogLevel.NoLogging);
					protocol.Log(
						"QA" + protocol.QActionID + "|MessageQueue.ReadQueue|Queue content: " + rawQueue,
						LogType.Error,
						LogLevel.NoLogging);
					queue = new List<NotificationObject>();
					throw;
				}
			}

			private void WriteQueue()
			{ protocol.SetParameter(Parameter.messagequeue_61, JsonConvert.SerializeObject(queue, Formatting.None)); }
			#endregion
		}

		public class MessageReceiver
		{
			#region Fields
			private string encodedCredentials;
			private string headers;
			private readonly SLProtocolExt protocol;
			#endregion

			#region Public properties
			public string Body { get; private set; }

			public string Path { get; private set; }
			#endregion

			#region Constructors
			public MessageReceiver(SLProtocolExt protocol)
			{
				this.protocol = protocol;
				ProcessLastReceivedMessage();
			}
			#endregion

			#region Private methods
			private void ProcessHeaders()
			{
				encodedCredentials = string.Empty;
				var lines = headers.Split(new[] { Environment.NewLine, }, StringSplitOptions.None);

				foreach (var line in lines)
				{
					if (line.StartsWith("POST") ||
						line.StartsWith("PUT") ||
						line.StartsWith("DELETE") ||
						line.StartsWith("GET"))
					{
						Path = line.Split(' ')[1];
					}

					if (line.StartsWith("token: "))
					{
						encodedCredentials = line.Replace("token: ", string.Empty);
					}
				}
			}

			private void ProcessLastReceivedMessage()
			{
				var rawMessage = Convert.ToString(protocol.GetParameter(Parameter.messagefromampp_40));
				var separator = new[] { Environment.NewLine + Environment.NewLine, };

				var messageParts = rawMessage.Split(separator, StringSplitOptions.None);
				headers = messageParts[0];
				Body = messageParts[1];
				ProcessHeaders();
			}
			#endregion
		}

		public class MessageResponseSender
		{
			private const int SendResponseTrigger = 42;
			#region Fields
			private readonly SLProtocolExt protocol;
			#endregion

			#region Constructors
			public MessageResponseSender(SLProtocolExt protocol) { this.protocol = protocol; }
			#endregion

			#region Public methods
			public void SendErrorResponse(Exception e)
			{
				var body = new HttpResponseBody();
				body.Result = "Internal Error";
				body.Description = e.Message;
				var serializedResponse = JsonConvert.SerializeObject(body);
				var bytes = Encoding.ASCII.GetBytes(serializedResponse);
				var httpMessage = "HTTP/1.1 500" +
					Environment.NewLine +
					"Content-Type: application/json" +
					Environment.NewLine +
					"Content-Length: " +
					bytes.Length +
					Environment.NewLine +
					Environment.NewLine +
					serializedResponse;
				protocol.SetParameter(Parameter.responsetoampp_41, httpMessage);
				protocol.CheckTrigger(SendResponseTrigger);
			}

			public void SendResponse(HttpResponseBody body)
			{
				var serializedResponse = JsonConvert.SerializeObject(body);
				var bytes = Encoding.ASCII.GetBytes(serializedResponse);
				var httpMessage = "HTTP/1.1 200 OK" +
					Environment.NewLine +
					"Content-Type: application/json" +
					Environment.NewLine +
					"Content-Length: " +
					bytes.Length +
					Environment.NewLine +
					Environment.NewLine +
					serializedResponse;
				protocol.SetParameter(Parameter.responsetoampp_41, httpMessage);
				protocol.CheckTrigger(SendResponseTrigger);
			}
			#endregion
		}

		public interface IMessageHandler
		{
			#region Public methods
			void HandleMessage();
			#endregion
		}
	}
}