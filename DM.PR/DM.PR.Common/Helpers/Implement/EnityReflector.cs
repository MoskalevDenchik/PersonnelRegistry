using System.Collections.Generic;
using DM.PR.Common.Entities;
using System.Collections;
using System.Reflection;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DM.PR.Business.Test")]
namespace DM.PR.Common.Helpers.Implement
{
    internal class EnityReflector : IEnityReflector
    {
        public List<object> GetPropertyValueList<T>(T obj)
        {
            List<object> list = new List<object>();

            if (typeof(IEnumerable<IEntity>).IsAssignableFrom(typeof(T)))
            {
                foreach (var item in obj as IEnumerable)
                {
                    AddPropetyValues(item, list);
                }
            }
            else
            {
                AddPropetyValues(obj, list);
            }

            return list;
        }
        private void AddPropetyValues<T>(T obj, List<object> list)
        {
            if (obj == null)
            {
                list.Add(null);
                return;
            }

            PropertyInfo[] propetrties = obj.GetType().GetProperties();

            foreach (PropertyInfo item in propetrties)
            {
                Type itemType = item.PropertyType;

                if (typeof(IEnumerable<IEntity>).IsAssignableFrom(itemType))
                {
                    var entity = obj.GetType().GetProperty(item.Name).GetValue(obj);
                    foreach (var podElement in entity as IEnumerable)
                    {
                        AddPropetyValues(podElement, list);
                    }
                }
                else if (typeof(IEntity).IsAssignableFrom(itemType))
                {
                    var entity = obj.GetType().GetProperty(item.Name).GetValue(obj);
                    AddPropetyValues(entity, list);
                }
                else
                {
                    list.Add(item.GetValue(obj));
                }
            }
        }
    }
}
