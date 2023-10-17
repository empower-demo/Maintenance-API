namespace Maintenance_API_1
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Text;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Net.Apps.UserDefinableApis;
	using Skyline.DataMiner.Net.Apps.UserDefinableApis.Actions;

	/// <summary>
	/// Represents a DataMiner Automation script.
	/// </summary>
	public class Script
	{

		[AutomationEntryPoint(AutomationEntryPointType.Types.OnApiTrigger)]
		public ApiTriggerOutput OnApiTrigger(IEngine engine, ApiTriggerInput requestData)
		{
			// In order to use this snippet, the 'Skyline.DataMiner.Dev.Automation' Nuget package needs to be installed with version >= 10.2.12
			// Add custom code here
			String message = "";
            StatusCode statusCode = StatusCode.Ok;
            if (requestData.RawBody.Equals("Start", StringComparison.InvariantCultureIgnoreCase))
			{
				Start(engine);
				message = "Maintenance Started.. Switched to Satellite !";
            }
			else if (requestData.RawBody.Equals("Stop", StringComparison.InvariantCultureIgnoreCase))
			{
                Stop(engine);
                message = "Maintenance Stopped.. Switched to Main Connection !";
            }
			else {
				message = "Invalid Input";
				statusCode = StatusCode.BadRequest;
            }
            return new ApiTriggerOutput
			{
				ResponseBody = message,
				ResponseCode = (int)statusCode,
			};
		}
		public void Start(IEngine engine) {
			var ec2 = engine.FindElement("localhost");
			ec2.Mask("Started Maintenance !!");
		}

		public void Stop(IEngine engine) {
            var ec2 = engine.FindElement("localhost");
			ec2.Unmask();
        }
	}
}