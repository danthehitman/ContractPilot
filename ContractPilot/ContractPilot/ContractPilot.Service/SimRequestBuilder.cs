using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using ContractPilot.Service.Enumeration;
using ContractPilot.Service.Model;

namespace ContractPilot.Service;

public class SimRequestBuilder
{
	private readonly Dictionary<string, SimVar> _variables;

	public SimRequestBuilder()
	{
		string json = File.ReadAllText(Path.Combine("Resources", "simvariables.json"), Encoding.UTF8);
		_variables = JsonSerializer.Deserialize<Dictionary<string, SimVar>>(json);
	}

	public List<SimRequest> GetRequestsForStruct<T>() where T : struct
	{
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		Type t = typeof(T);
		DataDefinition definition = (DataDefinition)Enum.Parse(typeof(DataDefinition), t.Name);
		IEnumerable<string> members = from f in t.GetFields()
			select f.Name;
		List<SimRequest> result = new List<SimRequest>();
		foreach (string member in members)
		{
			string[] parts = member.Split("__");
			string request = parts[0];
			parts[0] = parts[0].Replace("_", " ");
			string variableKey = parts[0];
			string sdkName = parts[0];
			if (parts.Length > 1)
			{
				variableKey = parts[0] + ":index";
				sdkName = parts[0];
				request += "_index";
			}
			sdkName += ((parts.Length == 1) ? string.Empty : (":" + parts[1]));
			if (_variables.TryGetValue(variableKey, out var value))
			{
				result.Add(new SimRequest
				{
					DataType = value.DataType,
					Definition = definition,
					NameUnitTuple = (sdkName, value.Unit),
					Request = (Request)Enum.Parse(typeof(Request), request)
				});
				continue;
			}
			throw new InvalidOperationException("simvariables.json is missing '" + sdkName + "'");
		}
		return result;
	}

	public SimRequest GetRequest(string name, DataDefinition definition)
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		string[] parts = name.Split("__");
		string request = parts[0];
		parts[0] = parts[0].Replace("_", " ");
		string variableKey = parts[0];
		string sdkName = parts[0];
		if (parts.Length > 1)
		{
			variableKey = parts[0] + ":index";
			sdkName = parts[0];
			request += "_index";
		}
		sdkName += ((parts.Length == 1) ? string.Empty : (":" + parts[1]));
		if (_variables.TryGetValue(variableKey, out var value))
		{
			SimRequest simRequest = new SimRequest();
			simRequest.DataType = value.DataType;
			simRequest.Definition = definition;
			simRequest.NameUnitTuple = (sdkName, value.Unit);
			simRequest.Request = (Request)Enum.Parse(typeof(Request), request);
			return simRequest;
		}
		throw new InvalidOperationException("simvariables.json is missing '" + sdkName + "'");
	}
}
