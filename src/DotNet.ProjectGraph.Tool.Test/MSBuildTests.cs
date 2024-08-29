using DotNet.ProjectGraph.Tool.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

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
        var project = msBuildService.ReadProject(@"../../../DotNet.ProjectGraph.Tool.Test.csproj");

        // then

    }
}