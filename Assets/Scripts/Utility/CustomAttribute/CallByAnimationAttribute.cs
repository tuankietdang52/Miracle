using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Utility.CustomAttribute
{
	/// <summary>
	/// For note, not have any affect to method
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class CallByAnimationAttribute : Attribute
	{

	}
}
