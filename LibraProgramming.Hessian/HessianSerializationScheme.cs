﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace LibraProgramming.Hessian
{
    internal class HessianSerializationScheme
    {
        public Type ObjectType
        {
            get;
            private set;
        }

        public ISerializationElement Element
        {
            get;
        }

        private HessianSerializationScheme(Type objectType, ISerializationElement element)
        {
            ObjectType = objectType;
            Element = element;
        }

        public static HessianSerializationScheme CreateFromType(Type type)
        {
            var catalog = new Dictionary<Type, ISerializationElement>();
            var element = CreateSerializationElement(type, catalog);

            return new HessianSerializationScheme(type, element);
        }

        public void Serialize(HessianOutputWriter writer, object graph, HessianSerializationContext context)
        {
            Element.Serialize(writer, graph, context);
        }

        public object Deserialize(HessianInputReader reader, HessianSerializationContext context)
        {
            return Element.Deserialize(reader, context);
        }

        private static ISerializationElement CreateSerializationElement(Type type, IDictionary<Type, ISerializationElement> catalog)
        {
            var info = type.GetTypeInfo();

            if (IsSimpleType(info))
            {
                var resolver = HessianObjectSerializerResolver.Current;
                var serializer = resolver.GetSerializer(type);

                return new ValueElement(type, serializer);
            }
            
            return BuildSerializationObject(type, catalog);
        }

        private static ISerializationElement BuildSerializationObject(Type type, IDictionary<Type, ISerializationElement> catalog)
        {
            ISerializationElement existing;

            if (catalog.TryGetValue(type, out existing))
            {
                return existing;
            }

            var info = type.GetTypeInfo();
            var contract = info.GetCustomAttribute<DataContractAttribute>();

            if (null == contract)
            {
                throw new Exception();
            }

            var properties = new List<PropertyElement>();
            var element = new ObjectElement(type, properties);

            catalog.Add(type, element);

            foreach (var property in info.DeclaredProperties)
            {
                var attribute = property.GetCustomAttribute<DataMemberAttribute>();

                if (null == attribute)
                {
                    continue;
                }

                if (!property.CanRead || !property.CanWrite)
                {
                    continue;
                }

                var prop = new PropertyElement(property, CreateSerializationElement(property.PropertyType, catalog));

                properties.Add(prop);
            }

            properties.Sort(new ObjectPropertyComparer());

            return element;
        }

        private static bool IsSimpleType(TypeInfo typeinfo)
        {
            if (typeinfo.IsValueType || typeinfo.IsEnum || typeinfo.IsPrimitive)
            {
                return true;
            }

            var type = typeinfo.AsType();

            return typeof (Guid) == type || typeof (string) == type;
        }
    }
}