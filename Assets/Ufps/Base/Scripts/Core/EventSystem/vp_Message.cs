/////////////////////////////////////////////////////////////////////////////////
//
//	vp_Message.cs
//	? Opsive. All Rights Reserved.
//	https://twitter.com/Opsive
//	http://www.opsive.com
//
//	description:	standard event type. takes 0-1 generic arguments and optionally
//					a generic return value
//
/////////////////////////////////////////////////////////////////////////////////

#if (UNITY_IOS || UNITY_WII || UNITY_PS3 || UNITY_PS4 || UNITY_XBOXONE)
// (see the 'AOT platform' comment in vp_Event.cs for info on this)
#define AOT
#endif

using System;
using System.Reflection;
using System.Collections.Generic;


/// <summary>
/// Represents a simple method with no arguments and no return value.
/// The method name in the target script must have the prefix 'OnMessage_'.
/// Call 'Send' on this event to invoke the method. An unlimited number
/// of callback methods with the 'OnMessage_' prefix can be added.
/// </summary>
public class vp_Message : vp_Event			// non-generic version for 0 arguments
{

	public delegate void Sender();
	public Sender Send;

#if (!AOT)
	protected static void Empty() { }
#endif

	/// <summary>
	///
	/// </summary>
	public vp_Message(string name) : base(name)
	{
		InitFields();
	}


	/// <summary>
	/// initializes the standard fields of this event type and
	/// signature
	/// </summary>
	protected override void InitFields()
	{

		m_Fields = new FieldInfo[]{Type.GetField("Send")};

		StoreInvokerFieldNames();
		
		m_DelegateTypes = new Type[] { typeof(vp_Message.Sender) };
		Prefixes = new Dictionary<string, int>() { { "OnMessage_", 0 } };

#if (!AOT)
		m_DefaultMethods = new MethodInfo[] { Type.GetMethod("Empty") };
		Send = Empty;
#endif

	}


	/// <summary>
	/// registers an external method to this event
	/// </summary>
	public override void Register(object t, string m, int v)
	{
		Send += (vp_Message.Sender)vp_Message.Sender.CreateDelegate(m_DelegateTypes[v], t, m);
		Refresh();
	}


	/// <summary>
	/// unregisters an external method from this event
	/// </summary>
	public override void Unregister(object t)
	{
		RemoveExternalMethodFromField(t, m_Fields[0]);
		Refresh();
	}


}


/// <summary>
/// Represents a method with one generic argument and no return value.
/// The method name in the target script must have the prefix 'OnMessage_'.
/// Call 'Send' on this event with a single argument of any type to
/// invoke the method. An unlimited number of callback methods with the
/// 'OnMessage_' prefix can be added.
/// </summary>
public class vp_Message<V> : vp_Message			// generic version with 1 argument
{

#if (!AOT)
	protected static void Empty<T>(T value) { }
#endif

	public delegate void Sender<T>(T value);
	public new Sender<V> Send;


	/// <summary>
	///
	/// </summary>
	public vp_Message(string name) : base(name) { }


	/// <summary>
	/// initializes the standard fields of this event type and
	/// signature
	/// </summary>
	protected override void InitFields()
	{

		m_Fields = new FieldInfo[] { Type.GetField("Send") };

		StoreInvokerFieldNames();
		
#if (!AOT)
		m_DefaultMethods = new MethodInfo[] { GetStaticGenericMethod(Type, "Empty", m_ArgumentType, typeof(void)) };
#endif

		m_DelegateTypes = new Type[] { typeof(vp_Message<>.Sender<>) };
		Prefixes = new Dictionary<string, int>() { { "OnMessage_", 0 } };

#if (!AOT)
		Send = Empty;
#endif

		if (m_DefaultMethods != null && m_DefaultMethods[0] != null)
			SetFieldToLocalMethod(m_Fields[0], m_DefaultMethods[0], MakeGenericType(m_DelegateTypes[0]));

	}


	/// <summary>
	/// registers an external method to this event
	/// </summary>
	public override void Register(object t, string m, int v)
	{

		if (m == null)
			return;

		AddExternalMethodToField(t, m_Fields[v], m, MakeGenericType(m_DelegateTypes[v]));
		Refresh();

	}


	/// <summary>
	/// unregisters an external method from this event
	/// </summary>
	public override void Unregister(object t)
	{

		RemoveExternalMethodFromField(t, m_Fields[0]);
		Refresh();

	}


}


/// <summary>
/// Represents a method with one generic argument and a generic return value.
/// The method name in the target script must have the prefix 'OnMessage_'.
/// Call 'Send' on this event with a single argument of any type to invoke
/// the method. An unlimited number of callback methods with the 'OnMessage_'
/// prefix can be added, however only the last event added will have a return
/// value.
/// </summary>
public class vp_Message<V, VResult> : vp_Message			// generic version with 1 argument and a return value
{

#if (!AOT)
	protected static TResult Empty<T, TResult>(T value) { return default(TResult); }
#endif

	public delegate TResult Sender<T, TResult>(T value);
	public new Sender<V, VResult> Send;

	
	/// <summary>
	///
	/// </summary>
	public vp_Message(string name) : base(name) { }


	/// <summary>
	/// initializes the standard fields of this event type and
	/// signature
	/// </summary>
	protected override void InitFields()
	{

		m_Fields = new FieldInfo[] { Type.GetField("Send") };

		StoreInvokerFieldNames();

#if (!AOT)
		m_DefaultMethods = new MethodInfo[] { GetStaticGenericMethod(Type, "Empty", m_ArgumentType, m_ReturnType) };
#endif

		m_DelegateTypes = new Type[] { typeof(vp_Message<,>.Sender<,>) };
		Prefixes = new Dictionary<string, int>() { { "OnMessage_", 0 } };

		if ((m_DefaultMethods != null) && (m_DefaultMethods[0] != null))
			SetFieldToLocalMethod(m_Fields[0], m_DefaultMethods[0], MakeGenericType(m_DelegateTypes[0]));
		
	}


	/// <summary>
	/// registers an external method to this event
	/// </summary>
	public override void Register(object t, string m, int v)
	{

		if (m == null)
			return;

		AddExternalMethodToField(t, m_Fields[0], m, MakeGenericType(m_DelegateTypes[0]));
		Refresh();

	}


	/// <summary>
	/// unregisters an external method from this event
	/// </summary>
	public override void Unregister(object t)
	{
		RemoveExternalMethodFromField(t, m_Fields[0]);
		Refresh();
	}


}

