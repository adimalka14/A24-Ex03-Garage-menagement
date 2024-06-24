using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using static Ex03.GarageLogic.VehicleFactory;

public class UniqDetailsForVehicle
{
    public static List<string> GetObjectDetails(Type i_ObjectType)
    {
        List<string> details = new List<string>();
        PropertyInfo[] properties = i_ObjectType.GetProperties();

        foreach (var property in properties)
        {
            if (property.DeclaringType != i_ObjectType)
            {
                continue;
            }

            string inputMessages = getUserInputMessages(property);
            StringBuilder propertyDetails = new StringBuilder();
            propertyDetails.Append(inputMessages);
            details.Add(propertyDetails.ToString());
        }

        return details;
    }

    private static string convertToSentenceCase(string i_Input)
    {
        StringBuilder result = new StringBuilder(i_Input.Length * 2);

        result.Append(char.ToUpper(i_Input[0]));
        for (int i = 1; i < i_Input.Length; i++)
        {
            if (char.IsUpper(i_Input[i]))
            {
                result.Append(' ');
            }

            result.Append(i_Input[i]);
        }

        return result.ToString();
    }

    private static string getUserInputMessages(PropertyInfo i_Property)
    {
        StringBuilder inputMessages = new StringBuilder($"Choose {convertToSentenceCase(i_Property.Name)} type:\n");

        if (i_Property.PropertyType == typeof(string))
        {
            inputMessages.Append("String");
        }
        else if (i_Property.PropertyType == typeof(int))
        {
            inputMessages.Append("Integer");
        }
        else if (i_Property.PropertyType == typeof(float))
        {
            inputMessages.Append("Float,{format : 0.00}");
        }
        else if (i_Property.PropertyType == typeof(bool))
        {
            inputMessages.Append("1 for True, 0 for False");
        }
        else if (i_Property.PropertyType.IsEnum)
        {
            Type enumType = i_Property.PropertyType;
            foreach (var enumValue in Enum.GetValues(enumType))
            {
                inputMessages.AppendLine($"{convertToSentenceCase(enumValue.ToString())} Enter {(int)enumValue}");
            }
        }

        return inputMessages.ToString();
    }

    public static string[] GetVehicleOptions()
    {
        return Enum.GetNames(typeof(eVehicleType));
    }
}