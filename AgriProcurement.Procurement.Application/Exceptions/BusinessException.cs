namespace AgriProcurement.Procurement.Application.Exceptions;

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
}
