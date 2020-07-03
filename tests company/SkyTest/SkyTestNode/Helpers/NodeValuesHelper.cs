using SkyTestNode.Entity;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SkyTestNode.Helpers
{
    public static class NodeValuesHelper
    {
        //in this method I'll receive the Node and use Reflection to reflect the Properties in Runtime and get the values into levels
        public static List<int> extractNodeValues(Node nodeRoot)
        {
            List<int> outputArray = new List<int>();
            foreach (PropertyInfo property in nodeRoot.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.PropertyType == typeof(int))
                {
                    outputArray.Add(Convert.ToInt32(property.GetValue(nodeRoot)));
                }
                else
                {
                    if (property.PropertyType == typeof(Node))
                    {
                        var node2thLevel = (Node)property.GetValue(nodeRoot);
                        foreach (PropertyInfo property2 in node2thLevel.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                        {
                            if (property2.PropertyType == typeof(int))
                            {
                                outputArray.Add(Convert.ToInt32(property2.GetValue(node2thLevel)));
                            }
                            else
                            {
                                var node3thLevel = (Node)property2.GetValue(node2thLevel);
                                foreach (PropertyInfo property3 in node3thLevel.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                                {
                                    if (property3.PropertyType == typeof(int))
                                    {
                                        outputArray.Add(Convert.ToInt32(property3.GetValue(node3thLevel)));
                                    }
                                    else
                                    {
                                        var node4thLevel = (Node)property3.GetValue(node3thLevel);
                                        foreach (PropertyInfo property4 in node4thLevel.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                                        {
                                            if (property4.PropertyType == typeof(int))
                                            {
                                                outputArray.Add(Convert.ToInt32(property4.GetValue(node4thLevel)));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return outputArray;
        }

    }
}
