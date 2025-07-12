using System.Text;
using System.Text.Json;

namespace CSM.UseCases.CursorPagination;

public sealed record Cursor(long CreatedAt, Guid LastId)
{
    public static string Encode(long createdAt, Guid lastId)
    {
        var cursor = new Cursor(createdAt, lastId);

        string json = JsonSerializer.Serialize(cursor);
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
    }

    public static Cursor? Decode(string? encodedCursor)
    {
        if (string.IsNullOrWhiteSpace(encodedCursor))
        {
            return null;
        }

        try
        {
            string json = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCursor));
            return JsonSerializer.Deserialize<Cursor>(json);
        }
        catch
        {
            return null;
        }
    }
}