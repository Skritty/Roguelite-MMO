using System;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
public class AcceptDefaultMemorySOAttribute : Attribute { }