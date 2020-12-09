using System.Collections.Generic;
namespace Lr2WindowsService
{
    public interface IConfigurationParser
    {
        List<Options> Parse();
    }
}
