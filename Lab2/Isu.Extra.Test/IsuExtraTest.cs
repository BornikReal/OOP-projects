using Isu.Extra.CGTA;
using Isu.Extra.Services;
using Isu.Models.GroupNameParts;
using Xunit;

namespace Isu.Extra.Test;

public class IsuExtraTest
{
    private readonly SuperIsuServie superIsuServie;

    public IsuExtraTest()
    {
        superIsuServie = new SuperIsuServie();
        superIsuServie.AddNewMegafaculty("TINT", new List<GroupLetter> { new GroupLetter('M') });
        superIsuServie.AddNewMegafaculty("NonameMegafaculty", new List<GroupLetter> { new GroupLetter('Z') });
    }

    [Fact]
    public void 
}
