namespace EscrowManagement.Domain;

public enum EscrowStatus
{
    Unknown,
    Created,
    Funded,
    Payed,
    Accepted,
    Completed,
    InDispute,
}