using UnityEngine;
using QFramework;

namespace ProjectVampire
{
	public partial class GameObject : ViewController
	{
		void Start()
		{
			// QFramework 提供的日志系统
			"hello world".LogInfo();
		}
	}
}
