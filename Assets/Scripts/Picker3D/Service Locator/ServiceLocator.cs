using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Picker3D
{
	public static class ServiceLocator
	{
		private static readonly Dictionary<Type, object> serviceRegistery = new Dictionary<Type, object>();
		
		public static void Register<T>(T serviceInstance) 
		{
			Type serviceType = typeof(T);
			
			if (serviceRegistery.ContainsKey(serviceType)) 
			{
				Debug.LogError($"{serviceType.ToString()} is already registered.");
				return;
			}
			
			serviceRegistery.Add(serviceType, serviceInstance);
		}
		
		public static void UnRegister<T>() 
		{
			Type serviceType = typeof(T);
			
			if (!serviceRegistery.ContainsKey(serviceType)) 
			{
				Debug.LogError($"{serviceType.ToString()} is already not registered.");
				return;
			}
			
			serviceRegistery.Remove(serviceType);
		}
		
		public static T GetService<T>() 
		{
			Type serviceType = typeof(T);
			
			if (serviceRegistery.ContainsKey(serviceType))
			{
				return (T)serviceRegistery[serviceType];
			}
			
			throw new Exception($"{serviceType.ToString()} is unregistered.");
	 	}
	}
}
