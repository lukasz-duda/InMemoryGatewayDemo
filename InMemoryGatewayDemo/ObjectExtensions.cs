using System;
using System.Linq;
using System.Reflection;

namespace InMemoryGatewayDemo
{
    public static class ObjectExtensions
    {
        public static T Clone<T>(this object source)
        {
            T destination = Activator.CreateInstance<T>();

            Copy<T>(source, destination);

            return destination;
        }

        private static void Copy<T>(object source, T destination)
        {
            var bindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;

            PropertyInfo[] sourceProperties = source.GetType().GetProperties(bindingFlags);
            PropertyInfo[] destinationProperties = typeof(T).GetProperties(bindingFlags);

            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                PropertyInfo destinationField = destinationProperties.Single(x => x.Name == sourceProperty.Name);
                destinationField.SetValue(destination, sourceProperty.GetValue(source, null), null);
            }

            FieldInfo[] sourceFields = source.GetType().GetFields(bindingFlags);
            FieldInfo[] destinationFields = typeof(T).GetFields(bindingFlags);

            foreach (FieldInfo sourceField in sourceFields)
            {
                FieldInfo destinationField = destinationFields.Single(x => x.Name == sourceField.Name);
                destinationField.SetValue(destination, sourceField.GetValue(source));
            }
        }
    }
}
