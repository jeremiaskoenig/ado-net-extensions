using System.Reflection;

namespace System.Data.Usability
{
    static class DataRowUsabilityExtension
    {
        /// <summary>
        /// Updates the properties defined by the DataFieldAttribute to be updated
        /// with the data from the first DataRow marked by the DataRowAttribute.
        /// </summary>
        public static void UpdateProperties<T>(this T obj) where T : class
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            DataRow dataRow = null;

            foreach (PropertyInfo property in properties)
            {
                if (!(property.PropertyType.Equals(typeof(DataRow))))
                    continue;

                object[] attributes = property.GetCustomAttributes(true);

                foreach (object attribute in attributes)
                {
                    if (attribute is DataRowAttribute)
                    {
                        dataRow = (DataRow)property.GetValue(obj);
                        break;
                    }
                }

                if (dataRow != null)
                    break;
            }

            if (dataRow == null)
                return;

            foreach (PropertyInfo property in properties)
            {
                object[] attributes = property.GetCustomAttributes(true);

                foreach (object attribute in attributes)
                {
                    if (attribute is DataFieldAttribute)
                    {
                        DataFieldAttribute dataProperty = attribute as DataFieldAttribute;
                        property.SetValue(obj, dataRow[dataProperty.Column]);
                    }
                }
            }
        }

        /// <summary>
        /// Updates the first DataRow marked by the DataRowAttribute with the data defined by the DataFieldAttribute.
        /// </summary>
        public static void UpdateDataRow<T>(this T obj) where T : class
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            
            DataRow dataRow = null;
            
            foreach (PropertyInfo property in properties)
            {
                if (!(property.PropertyType.Equals(typeof(DataRow))))
                    continue;

                object[] attributes = property.GetCustomAttributes(true);

                foreach (object attribute in attributes)
                {
                    if (attribute is DataRowAttribute)
                    {
                        dataRow = (DataRow)property.GetValue(obj);
                        break;
                    }
                }

                if (dataRow != null)
                    break;
            }

            if (dataRow == null)
                return;

            foreach (PropertyInfo property in properties)
            {
                object[] attributes = property.GetCustomAttributes(true);

                foreach (object attribute in attributes)
                {
                    if (attribute is DataFieldAttribute)
                    {
                        DataFieldAttribute dataProperty = attribute as DataFieldAttribute;
                        dataRow[dataProperty.Column] = property.GetValue(obj);
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// Marks the DataRow which is the database representation of the current class.
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class DataRowAttribute : Attribute { }

    /// <summary>
    /// Defines the database field in which the property will be stored.
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class DataFieldAttribute : Attribute
    {
        readonly string databaseColumn;
        
        public DataFieldAttribute(string column)
        {
            this.databaseColumn = column;
        }

        /// <summary>
        /// Name of the column in the DataRow
        /// </summary>
        public string Column
        {
            get { return databaseColumn; }
        }
    }
}
