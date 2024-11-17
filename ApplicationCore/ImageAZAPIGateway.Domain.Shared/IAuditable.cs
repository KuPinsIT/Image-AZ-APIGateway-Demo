namespace ImageAZAPIGateway.Domain.Shared
{
    public interface IAuditable : ICreationAuditable
    {
        DateTime? LastUpdated { get; set; }

        string? LastUpdatedBy { get; set; }
    }

    public interface ICreationAuditable
    {
        DateTime CreatedDate { get; set; }

        string? CreatedBy { get; set; }
    }
}
