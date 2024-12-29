using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Log
{
	public static class LoggerExtension
	{
		private static readonly string warning = "Warning";
		private static readonly string tag = "Error";

		public static Logger CreateLogger()
		{
			return new Logger(new LogHandler());
		}

		public static void LogWarning(this Logger logger, object message)
		{
			logger.LogWarning(warning, message);
		}
		
		public static void LogError(this Logger logger, object message)
		{
			logger.LogError(tag, message);
		}
	}
}
