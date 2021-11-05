using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Circle.Helper.Extensions
{
    internal static class Util
    {
        internal static object GetPropertyValue(this object obj, string propertyName)
        {
            var columns = propertyName.Split('.');
            var propertyValue = obj;
            foreach (var item in columns)
            {

                var property = propertyValue.GetType().GetProperty(item).GetValue(propertyValue);
                propertyValue = property;
            }

            //if (property == null) throw new ArgumentNullException(String.Format("{0} geçersiz", propertyName));
            if (propertyValue == null)
                return "";
            else
                return propertyValue.ToString();// obj.GetValue(obj, null);
        }

        internal static object GetPropertyValueEx(this object obj, string propertyName, string nullDisplayText, string formatText)
        {

            /********************************************************************/
            Type type = null;
            var columns = propertyName.Split('.');
            var propertyValue = obj;
            foreach (var item in columns)
            {
                var property = propertyValue.GetType().GetProperty(item);
                propertyValue = property.GetValue(propertyValue);
                type = property.PropertyType;
            }

            var formattedModelValue = propertyValue;
            if (obj == null || (type == typeof(DateTime) && (DateTime)propertyValue == DateTime.MinValue))
            {
                if (!nullDisplayText.IsBlank())
                    formattedModelValue = nullDisplayText;
                else
                    formattedModelValue = "";
            }
            else if (!string.IsNullOrEmpty(formatText))
            {
                formattedModelValue = string.Format(System.Globalization.CultureInfo.CurrentCulture, formatText, formattedModelValue);
            }

            return formattedModelValue;
            /********************************************************************/
            /*
            var columns = propertyName.Split('.');
            var propertyValue = obj;
            foreach (var item in columns)
            {

                var property = propertyValue.GetType().GetProperty(item).GetValue(propertyValue);
                propertyValue = property;
            }
            
            //if (property == null) throw new ArgumentNullException(String.Format("{0} geçersiz", propertyName));
            if (propertyValue == null)
                return "";
            else
                return propertyValue.ToString();// obj.GetValue(obj, null);
                */
        }

        internal static string GetHtmlAttributes2(object htmlAttributes)
        {
            string ret = string.Empty;

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                foreach (var item in attributes)
                {
                    ret += " " + item.Key + "=" + "'" + item.Value + "'";
                }
            }

            return ret;
        }

        internal static string GetHtmlAttributes(IDictionary<string, object> htmlAttributes)
        {
            string ret = string.Empty;

            if (htmlAttributes != null)
            {

                foreach (var item in htmlAttributes)
                {
                    ret += " " + item.Key + "=" + "'" + item.Value + "'";
                }
            }

            return ret;
        }

        public static IDictionary<string, object> MergeHtmlAttributes(object htmlAttributesObject, object defaultHtmlAttributesObject)
        {
            var concatKeys = new string[] { "class" };

            var htmlAttributesDict = htmlAttributesObject as IDictionary<string, object>;
            var defaultHtmlAttributesDict = defaultHtmlAttributesObject as IDictionary<string, object>;

            RouteValueDictionary htmlAttributes = (RouteValueDictionary)((htmlAttributesDict != null)
                ? new RouteValueDictionary(htmlAttributesDict)
                : HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributesObject));
            RouteValueDictionary defaultHtmlAttributes = (RouteValueDictionary)((defaultHtmlAttributesDict != null)
                ? new RouteValueDictionary(defaultHtmlAttributesDict)
                : HtmlHelper.AnonymousObjectToHtmlAttributes(defaultHtmlAttributesObject));

            foreach (var item in htmlAttributes)
            {
                if (concatKeys.Contains(item.Key))
                {
                    defaultHtmlAttributes[item.Key] = (defaultHtmlAttributes[item.Key] != null)
                        ? string.Format("{0} {1}", defaultHtmlAttributes[item.Key], item.Value)
                        : item.Value;
                }
                else
                {
                    defaultHtmlAttributes[item.Key] = item.Value;
                }
            }

            return defaultHtmlAttributes;
        }

        internal static string GetDisplayName<T>(PropertyInfo p)
        {
            System.Type type = typeof(T);
            DisplayAttribute attr;
            attr = (DisplayAttribute)type.GetProperty(p.Name).GetCustomAttributes(typeof(DisplayAttribute), true).SingleOrDefault();

            if (attr == null)
            {
                MetadataTypeAttribute metadataType = (MetadataTypeAttribute)type.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
                if (metadataType != null)
                {
                    var property = metadataType.MetadataClassType.GetProperty(p.Name);
                    if (property != null)
                    {
                        attr = (DisplayAttribute)(property.GetCustomAttributes().Where(c => c.GetType().FullName.Contains("DisplayAttribute")).FirstOrDefault() as DisplayAttribute);
                    }
                }
            }

            if (attr != null)
            {
                return attr.GetName();
            }
            else
            {
                return p.Name;
            }
        }

        internal static PropertyInfo GetPropertyInfo<TSource>(Expression<Func<TSource, object>> propertyLambda)
        {
            System.Type type = typeof(TSource);
            LambdaExpression lambda = propertyLambda as LambdaExpression;
            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
            {
                throw new ArgumentException("method");
            }

            PropertyInfo propInfo = memberExpr.Member as PropertyInfo;
            /*
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format("Lambda ifadesindeki '{0}' alanı özellik değil.",propertyLambda.ToString()));
            }

            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
            {
                throw new ArgumentException(string.Format("Lambda ifadesindeki '{0}' özelliği {1} tipinde değil.", propertyLambda.ToString(), type));
            }*/

            return propInfo;
        }

        internal static PropertyInfo GetPropertyInfo<TSource, TEnum>(Expression<Func<TSource, TEnum>> propertyLambda)
        {
            System.Type type = typeof(TSource);
            LambdaExpression lambda = propertyLambda as LambdaExpression;
            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
            {
                throw new ArgumentException("method");
            }

            PropertyInfo propInfo = memberExpr.Member as PropertyInfo;
            /*
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format("Lambda ifadesindeki '{0}' alanı özellik değil.",propertyLambda.ToString()));
            }

            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
            {
                throw new ArgumentException(string.Format("Lambda ifadesindeki '{0}' özelliği {1} tipinde değil.", propertyLambda.ToString(), type));
            }*/

            return propInfo;
        }

        internal static string GetEnumDescription<TEnum>(TEnum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = (DisplayAttribute[])field.GetCustomAttributes(typeof(DisplayAttribute), false);
            return attributes.Length > 0 ? attributes[0].Name : value.ToString();
        }


        internal static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            Type modelType = modelMetadata.ModelType;

            Type underlyingType = Nullable.GetUnderlyingType(modelType);
            if (underlyingType != null)
            {
                modelType = underlyingType;
            }
            return modelType;
        }

        internal static Type GetNonNullableModelType(Type modelType)
        {
            Type underlyingType = Nullable.GetUnderlyingType(modelType);
            if (underlyingType != null)
            {
                modelType = underlyingType;
            }
            return modelType;
        }

        internal static bool IsNullable<T>(T obj)
        {
            if (obj == null) return true; // obvious
            Type type = typeof(T);
            if (!type.IsValueType) return true; // ref-type
            if (Nullable.GetUnderlyingType(type) != null) return true; // Nullable<T>
            return false; // value-type
        }

        internal static bool IsNullable(Type modelType)
        {
            bool result = false;
            if (Nullable.GetUnderlyingType(modelType) != null) { result = true; }
            return result;
        }


    }
}
