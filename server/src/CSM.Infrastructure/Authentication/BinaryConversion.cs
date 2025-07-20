using CSM.Core.Features.Views;
using CSM.UseCases.Abstractions.Authentication;

namespace CSM.Infrastructure.Authentication;

internal sealed class BinaryConversion : IBinaryConversion
{
    public string DecimalToBinary(int input)
    {
        int length = Enum.GetValues<ActionType>().Length;
        var binary = Convert.ToString(input, 2).PadLeft(length, '0');
        
        return ReverseString(binary);
    }

    public int BinaryToDecimal(string binary)
    {
        return Convert.ToInt32(ReverseString(binary), 2);
    }
    
    private string ReverseString(string input)
    {
        return new string(input.Reverse().ToArray());
    }
}