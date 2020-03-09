using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Automatik
{
    internal class MemberInfo
    {
        public string Name { get; set; }

        public Type MemberType { get; set; }

        public IEnumerable<FindByAttribute> FindBy { get; set; }

        public IEnumerable<WaitAttribute> Wait { get; set; }

        public Action<object> SetValue { get; set; }
        public Func<object> GetValue { get; set; }


        public static IEnumerable<MemberInfo> FindOn(object obj)
        {
            if (obj == null)
                return new MemberInfo[] {};

            var objType = obj.GetType();

            var fields =
                objType
                    .GetFields()
                    .Where(f => f.IsDefined(typeof(FindByAttribute)))
                    .Select(f =>
                        new MemberInfo
                        {
                            Name = f.Name,
                            MemberType = f.FieldType,
                            FindBy = f.GetCustomAttributes<FindByAttribute>(),
                            Wait = f.GetCustomAttributes<WaitAttribute>(),
                            SetValue = (Action<object>)((object val) => f.SetValue(obj, val)),
                            GetValue = (Func<object>)(() => f.GetValue(obj))
                        });

            var properties =
                objType
                    .GetProperties()
                    .Where(p => p.IsDefined(typeof(FindByAttribute)))
                    .Select(p =>
                        new MemberInfo
                        {
                            Name = p.Name,
                            MemberType = p.PropertyType,
                            FindBy = p.GetCustomAttributes<FindByAttribute>(),
                            Wait = p.GetCustomAttributes<WaitAttribute>(),
                            SetValue = (Action<object>)((object val) => p.SetValue(obj, val)),
                            GetValue = (Func<object>)(() => p.GetValue(obj))
                        }
                    );

            return fields.Union(properties);
        }
   }
}