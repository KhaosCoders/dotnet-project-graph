using DotNet.ProjectGraph.Tool.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNet.ProjectGraph.Tool.Test;

[TestClass]
public class MsBuildTests
{
    private IMSBuildService msBuildService;

    public MsBuildTests()
    {
        msBuildService = new MSBuildService();
    }

    [TestMethod]
    public void ReadCsProject()
    {
        // given

        // when
        var project = msBuildService.ReadProject(@"E:\FE_Parser\SWSConverter\SWSApps\SWSLib\SWSLib.csproj");

        // then

    }
}