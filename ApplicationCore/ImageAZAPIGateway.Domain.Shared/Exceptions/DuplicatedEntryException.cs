namespace ImageAZAPIGateway.Domain.Shared.Exceptions
{
    public class DuplicatedEntryException : BusinessException
    {
        public DuplicatedEntryException(string entity) : base($"{entity} already exists")
        {
        }

        public DuplicatedEntryException(string entity, string details) : base($"{entity} already exists ({details})")
        {
        }
    }
}
