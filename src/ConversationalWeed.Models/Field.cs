namespace ConversationalWeed.Models
{
    public class Field
    {
        /// <summary>
        /// Field Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Type of weed card inside the field
        /// </summary>
        public FieldValue Value { get; set; }

        /// <summary>
        /// Type of protected card above the field
        /// </summary>
        public ProtectedFieldValue ProtectedValue { get; set; }

        /// <summary>
        /// Id of the last player that change the value of the field
        /// </summary>
        public ulong? ValueOwnerId { get; set; }

        /// <summary>
        /// Id of the last player that change the protected value of the field
        /// </summary>
        public ulong? ProtectedValueOwnerId { get; set; }

    }
}