using System;
using System.Reflection;

public class PropertyReader
{
    //simple struct to store the type and name of variables
    public struct Variable
    {
        public string name;
        public Type type;
        public Object objectReference;
    }

    //for instances of classes that inherit PropertyReader
    private Variable[] _fields_cache;
    private Variable[] _props_cache;

    public Variable[] GetFields()
    {
        if (_fields_cache == null)
            _fields_cache = GetFields(this.GetType());

        return _fields_cache;
    }

    public Variable[] GetProperties()
    {
        if (_props_cache == null)
            _props_cache = GetProperties(this.GetType());

        return _props_cache;
    }

    //getters and setters for instance values that inherit PropertyReader
    public object GetValue(string name)
    {
        return this.GetType().GetProperty(name).GetValue(this, null);
    }

    public void SetValue(string name, object value)
    {
        this.GetType().GetProperty(name).SetValue(this, value, null);
    }

    //static functions that return all values of a given type
    public static Variable[] GetFields(Type type)
    {
        var fieldValues = type.GetFields();
        var result = new Variable[fieldValues.Length];
        for (int i = 0; i < fieldValues.Length; i++)
        {
            result[i].name = fieldValues[i].Name;
            result[i].type = fieldValues[i].GetType();
            result[i].objectReference
                = fieldValues[i].GetValue(null);
        }

        return result;
    }

    public static Variable[] GetProperties(Type type)
    {
        var propertyValues = type.GetProperties();
        var result = new Variable[propertyValues.Length];
        for (int i = 0; i < propertyValues.Length; i++)
        {
            result[i].name = propertyValues[i].Name;
            result[i].type = propertyValues[i].GetType();
            result[i].objectReference
                = propertyValues[i].GetValue(null);
        }

        return result;
    }
}