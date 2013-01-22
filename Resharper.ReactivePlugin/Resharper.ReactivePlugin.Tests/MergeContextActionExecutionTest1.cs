namespace Resharper.ReactivePlugin.Tests
{
    using JetBrains.ReSharper.Feature.Services.Bulbs;
    using JetBrains.ReSharper.Feature.Services.CSharp.Bulbs;
    using JetBrains.ReSharper.Intentions.CSharp.Test;
    using JetBrains.ReSharper.Intentions.Extensibility;
    using NUnit.Framework;

    [TestFixture]
    public class MergeContextActionExecutionTest1 : CSharpContextActionExecuteTestBase
    {
        protected override string ExtraPath
        {
            get { return "ClassName"; }
        }

        protected override IContextAction CreateContextAction(ICSharpContextActionDataProvider dataProvider)
        {
            // todo: create an instance of your context action here
            // e.g. new ClassNameContextAction(dataProvider)
            return null;
        }

        [TestCase("execution01")]
        [Test]
        public void Test(string testSrc)
        {
            DoOneTest(testSrc);
        }
    }
}
