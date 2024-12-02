using HYPJCW_HSZFT.Entities.Dependencies;
using HYPJCW_HSZFT.Logic.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace HYPJCW_HSZFT.Logic
{
    public class ExportLogic : IExportLogic
    {
        public void ExportToXml(Type[] typesToExport)
        {
            var root = new XElement("entities",
             new XAttribute("exportDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));

            foreach (var type in typesToExport)
            {
                if (!type.GetCustomAttributes(typeof(ToExportAttribute), false).Any())
                    continue; // Skip types without [ToExport] attribute

                var entity = new XElement("entity",
                    new XAttribute("hash", type.GetHashCode()),
                    new XAttribute("name", type.Name),
                    new XAttribute("namespace", type.Namespace??"Unspecified"));

                // Export properties
                var properties = new XElement("properties");

                foreach (var prop in type.GetProperties())
                {
                    // Check if the property is marked with [XmlAttribute]
                    var isAttribute = prop.GetCustomAttributes(typeof(XmlAttributeAttribute), false).Any();
                    var propertyElement = new XElement("property",
                        new XAttribute("type", prop.PropertyType.Name),
                        new XAttribute("name", prop.Name),
                        new XAttribute("isAttribute", isAttribute));

                    // Handle lists or collections
                    if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType) && prop.PropertyType != typeof(string))
                    {
                        var collectionType = prop.PropertyType.IsGenericType
                            ? prop.PropertyType.GetGenericArguments()[0].Name
                            : "Unknown";

                        propertyElement.Add(new XAttribute("isCollection", true));
                        propertyElement.Add(new XAttribute("collectionType", collectionType));
                    }
                    else
                    {
                        propertyElement.Add(new XAttribute("isCollection", false));
                    }

                    properties.Add(propertyElement);
                }

                entity.Add(properties);

                // Export methods
                var methods = new XElement("methods",
                    type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                        .Select(m => new XElement("method",
                            new XAttribute("returnType", m.ReturnType.Name),
                            new XAttribute("name", m.Name),
                            new XElement("parameters",
                                m.GetParameters().Select(param => new XElement("param",
                                    new XAttribute("type", param.ParameterType.Name),
                                    new XAttribute("name", param.Name??"Unspecified")))))));

                entity.Add(methods);
                root.Add(entity);
            }
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "ExportedEntities.xml");
            XDocument document = new XDocument(root);
            using (var writer = new StreamWriter(filePath))
            {
                document.Save(writer);
            }
        }


    }
}
