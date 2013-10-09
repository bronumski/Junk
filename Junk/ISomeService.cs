using System.Collections;

namespace Junk
{
    public interface ISomeService
    {
        int Convert(string foo);
        int Convert(string foo, string bar);

        IEnumerable GetAValue();
    }
}