namespace CSM.UseCases.Abstractions.Authentication;

/// <summary>
/// To define service to work with binary authentication
/// </summary>
public interface IBinaryConversion
{
    /// <summary>
    /// Convert integer to string binary
    /// It will add "0" prefix at beginning for FE to compare
    /// </summary>
    public string DecimalToBinary(int input);
    
    /// <summary>
    /// Convert string binary to decimal
    /// Start from right to left
    /// </summary>
    public int BinaryToDecimal(string binary);
}