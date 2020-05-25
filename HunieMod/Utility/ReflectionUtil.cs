using System;
using System.Collections.Generic;
using System.Reflection;

namespace HunieMod.Utility
{
    /// <summary>
    /// Utilities to access (public or private) fields/properties/methods of any object
    /// </summary>
    public static class ReflectionUtil
    {
        private static readonly BindingFlags allFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

        /// <summary>
        /// Gets the value from an object's field
        /// </summary>
        /// <param name="instance">The instance object</param>
        /// <param name="fieldName">The field's name which is to be fetched</param>
        /// <returns>The field's value or null when field is not found</returns>
        public static object GetField(object instance, string fieldName)
        {
            if (instance == null)
                return null;
            return GetField(instance.GetType(), instance, fieldName);
        }

        /// <summary>
        /// Gets the value from an object's field
        /// </summary>
        /// <param name="type">The instance type</param>
        /// <param name="instance">The instance object</param>
        /// <param name="fieldName">The field's name which is to be fetched</param>
        /// <returns>The field's value or null field is not found</returns>
        public static object GetField(Type type, object instance, string fieldName)
        {
            if (type != null && instance != null && !string.IsNullOrEmpty(fieldName))
            {
                FieldInfo field = type.GetField(fieldName, allFlags);
                if (field != null)
                    return field.GetValue(instance);
            }
            return null;
        }

        /// <summary>
        /// Sets the value of an object's field
        /// </summary>
        /// <param name="instance">The instance object</param>
        /// <param name="fieldName">The field's name which is to be set</param>
        /// <param name="fieldValue">The field's new value</param>
        public static void SetField(object instance, string fieldName, object fieldValue)
        {
            if (instance == null)
                return;
            SetField(instance.GetType(), instance, fieldName, fieldValue);
        }

        /// <summary>
        /// Sets the value of an object's field
        /// </summary>
        /// <param name="type">The instance type</param>
        /// <param name="instance">The instance object</param>
        /// <param name="fieldName">The field's name which is to be set</param>
        /// <param name="fieldValue">The field's new value</param>
        public static void SetField(Type type, object instance, string fieldName, object fieldValue)
        {
            if (type != null && instance != null && !string.IsNullOrEmpty(fieldName))
            {
                FieldInfo field = type.GetField(fieldName, allFlags);
                if (field != null)
                    field.SetValue(instance, fieldValue);
            }
        }

        /// <summary>
        /// Gets the value from an object's property
        /// </summary>
        /// <param name="type">The instance type</param>
        /// <param name="instance">The instance object</param>
        /// <param name="propertyName">The property name which is to be fetched</param>
        /// <returns>The property value from the object or null when property is not found</returns>
        public static object GetProperty(Type type, object instance, string propertyName)
        {
            if (type != null && instance != null)
            {
                PropertyInfo prop = type.GetProperty(propertyName, allFlags);
                if (prop != null)
                    return prop.GetValue(instance, null);
            }
            return null;
        }

        /// <summary>
        /// Sets the value of an object's property
        /// </summary>
        /// <param name="type">The instance type</param>
        /// <param name="instance">The instance object</param>
        /// <param name="propertyName">The property name which is to be set</param>
        /// <param name="propertyValue">The property's new value</param>
        public static void SetProperty(Type type, object instance, string propertyName, object propertyValue)
        {
            if (type != null && instance != null && !string.IsNullOrEmpty(propertyName))
            {
                PropertyInfo prop = type.GetProperty(propertyName, allFlags);
                if (prop != null)
                    prop.SetValue(instance, propertyValue, null);
            }
        }

        /// <summary>
        /// Invokes an object's method
        /// </summary>
        /// <param name="type">The instance type</param>
        /// <param name="instance">The instance object</param>
        /// <param name="methodName">Name of the method</param>
        /// <param name="parameters">Parameters to pass to the method</param>
        /// <returns>The method's return value (if any) or null when method is not found</returns>
        public static object Invoke(Type type, object instance, string methodName, params object[] parameters)
        {
            if (type != null && instance != null && !string.IsNullOrEmpty(methodName))
            {
                MethodInfo method = type.GetMethod(methodName, allFlags);
                if (method != null)
                    return method.Invoke(instance, parameters);
            }
            return null;
        }

        /// <summary>
        /// Invokes an object's method
        /// </summary>
        /// <param name="instance">The instance object</param>
        /// <param name="methodName">Name of the method</param>
        /// <param name="parameters">Parameters to pass to the method</param>
        /// <returns>The method's return value (if any) or null when method is not found</returns>
        public static object Invoke(object instance, string methodName, params object[] parameters)
        {
            if (instance == null)
                return null;
            return Invoke(instance.GetType(), instance, methodName, parameters);
        }

        /// <summary>
        /// Gets all the non-abstract subclasses of a Type
        /// </summary>
        /// <typeparam name="T">Parent type from which the subclasses inherit</typeparam>
        /// <param name="assembly">Assembly to query. If null, the assembly of the parent type will be used</param>
        /// <returns>Type array of all the subclasses found</returns>
        public static Type[] GetSubclassesOf<T>(Assembly assembly = null)
        {
            Type parent = typeof(T);
            if (!parent.IsClass)
                throw new ArgumentException("Parent type must be a class", nameof(T));

            assembly = assembly ?? Assembly.GetAssembly(parent);
            List<Type> list = new List<Type>();

            if (assembly != null)
            {
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (type.IsClass && !type.IsAbstract && type.IsSubclassOf(parent))
                    {
                        list.Add(type);
                    }
                }
            }

            return list.ToArray();
        }
    }
}
