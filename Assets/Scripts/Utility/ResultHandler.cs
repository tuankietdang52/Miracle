using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Utility
{
	public class ResultHandler<T>
	{
		public T Value { get; } = default;
		public bool Result { get; }
		public Exception Error { get; }

		/// <summary>
		/// Create new result with fail result
		/// </summary>
		public ResultHandler(Exception error)
		{
			Result = false;
			Error = error;
		}

		/// <summary>
		/// Create new result with success result
		/// </summary>
		/// <param name="value"></param>
		public ResultHandler(T value)
		{
			if (typeof(T).IsSubclassOf(typeof(Exception)) || typeof(T) == typeof(Exception))
			{
				throw new Exception("Value must not be exception");
			}

			Value = value;
			Result = true;
		}
	}
}
