using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Blazorise.DataGrid;

namespace WebClient.Helpers
{
    public static class ReflectionHelper
    {
        public static string GetMemberName<T, U>(Expression<Func<T, U>> expression)
        {
            var memberInfo = GetMemberInfo(expression);

            return memberInfo.Name;
        }

        public static MemberInfo GetMemberInfo<T, U>(Expression<Func<T, U>> expression)
        {
            var member = expression.Body as MemberExpression;

            if (member is null)
            {
                throw new ArgumentException("Expression is not a member access", "expression");
            }

            return member.Member;
        }

        public static T FillObjectProperty<T>(this T target, IDictionary<string, object> dictionary) 
            where T : class
        {
            var objectType = typeof(T);

            foreach (var pair in dictionary)
            {
                var propertyInfo = objectType.GetProperty(pair.Key);

                if(propertyInfo != null)
                {
                    var propertyType = propertyInfo.PropertyType;

                    var propertyValue = Convert.ChangeType(pair.Value, Nullable.GetUnderlyingType(propertyType) ?? propertyType);

                    propertyInfo.SetValue(target, propertyValue, null);
                }
            }

            return target;
        }
    }

    public static class DataGridExtensions
    {
        public static void UpdateAnotherDataGridCell<TItem, TProperty>(this CellEditContext<TItem> context, Expression<Func<TItem, TProperty>> expression, string newValue)
        {
            context.CellValue = newValue;

            var propertyName = ReflectionHelper.GetMemberName(expression);

            context.UpdateCell(propertyName, context.Item);
        }
    }
}
