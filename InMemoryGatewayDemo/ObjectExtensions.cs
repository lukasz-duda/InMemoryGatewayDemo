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

            CopyProperties<T>(source, destination);

            return destination;
        }

        private static void CopyProperties<T>(object source, T destination)
        {
            var bindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
            PropertyInfo[] fields = source.GetType().GetProperties(bindingFlags);
            PropertyInfo[] destinationFields = typeof(T).GetProperties(bindingFlags);

            foreach (PropertyInfo sourceField in fields)
            {
                bool destinationContainsField = destinationFields
                    .Where(x => x.Name == sourceField.Name)
                    .Where(x => x.PropertyType == sourceField.PropertyType)
                    .Any();

                if (destinationContainsField)
                {
                    PropertyInfo destinationField = destinationFields
                        .Where(x => x.Name == sourceField.Name)
                        .Where(x => x.PropertyType == sourceField.PropertyType)
                        .Single();

                    destinationField.SetValue(destination, sourceField.GetValue(source, null), null);
                }
            }
        }
    }
}
