namespace OrganizationCore.Paasword
{
    public interface IPasswordGenerationInterface
    {
        string GeneratePasswordAsync(int length = 12);
    }
}
