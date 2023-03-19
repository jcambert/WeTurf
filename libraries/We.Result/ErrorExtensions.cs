using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace We.Results;

public static class ErrorExtensions
{
    public static string AsString(this IReadOnlyList<Error> errors)
        => errors.Select(error => error.Failure).JoinAsString("\n");

    private static string JoinAsString(this IEnumerable<string> source, string separator)
    => string.Join(separator, source);

}
