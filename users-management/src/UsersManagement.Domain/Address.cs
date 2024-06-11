namespace UsersManagement.Domain;

public sealed record Address : IComparable<Address>
{
    private readonly string _address;

    private Address(string address)
    {
        if(!address.StartsWith("0x"))
        {
            throw new InvalidCastException($"Not a valid address {address}");
        }

        _address = address;
    }

    public int CompareTo(Address? other) =>
        other == null 
            ? 1 
            : string.Compare(_address, other._address, StringComparison.OrdinalIgnoreCase);

    public bool Equals(Address? other) =>
        other != null 
        && string.Equals(_address, other._address, StringComparison.OrdinalIgnoreCase);

    public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(_address);

    public override string ToString() => _address;

    public static implicit operator string(Address address) => address._address;
    public static implicit operator Address(string address) => new(address);
}