namespace Resharper.ReactivePlugin.Tests.test.data.select_and_merge
{
    using System.Collections.Generic;
    using System.Linq;

    public class File00
    {
        public int Method1()
        {
            return 42;
        }

        public bool Method2()
        {
            return false;
        }

        public IEnumerable<object> Method3()
        {
            return Enumerable.Empty<object>();
        }

        public int Property1
        {
            get { return 42; }
        }

        public bool Property2
        {
            get { return false; }
        }

        public IEnumerable<object> Property3
        {
            get { return Enumerable.Empty<object>(); }
        }
    }
}
