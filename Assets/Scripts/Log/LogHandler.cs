using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Log
{
	public class LogHandler : ILogHandler
	{
		public void LogException(Exception exception, UnityEngine.Object context)
		{
			Debug.unityLogger.LogException(exception, context);
		}

		public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
		{
			Debug.unityLogger.logHandler.LogFormat(logType, context, format, args);
		}
	}
}
