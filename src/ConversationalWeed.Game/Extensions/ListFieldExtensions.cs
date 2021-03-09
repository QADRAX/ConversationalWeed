using System.Collections.Generic;

namespace ConversationalWeed.Models
{
    public static class ListFieldExtensions
    {
        public static IList<Field> AddInitialFields(this IList<Field> fields, int numberOfFields)
        {
            for (int i = 1; i <= numberOfFields; i++)
            {
                fields.Add(new Field
                {
                    Id = i,
                    ProtectedValue = ProtectedFieldValue.Free,
                    Value = FieldValue.Empty,
                    ValueOwnerId = null,
                    ProtectedValueOwnerId = null,
                });
            }
            return fields;
        }
    }
}
