using System;
using System.Text;
using System.Threading;
using System.Windows.Automation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;

namespace testNotepad
{
    [TestClass]
    public class UnitTest1
    {
        private Random rnd;
        private Application application;

        [TestInitialize]
        public void setUp()
        {
            rnd = new Random();
            application = Application.Launch(@"C:\Windows\system32\notepad.exe");
        }

        [TestMethod]
        public void TetsNotepadPastASCII()
        {
            Assert.IsNotNull(application);
            TextBox tb = GetWindow().Get<TextBox>(SearchCriteria.ByClassName("Edit"));
            tb.Text = GenerateASCIIString();
        }

        [TestCleanup]
        public void tearDown()
        {
            //waiting for 5 sec.
            Thread.Sleep(5000);
            application.Close();
        }

        /**
         * Generates random ASCII string 
         * with length from 5 to 25 characters
         * @return random ASCII string
         */
        private string GenerateASCIIString()
        {
            var length = rnd.Next(5, 26);
            StringBuilder sb = new StringBuilder(length);
            while (sb.Length < length)
            {
                char c = (char)(rnd.Next(32, 127));
                sb.Append(c);
            }
            return sb.ToString();
        }

        private Window GetWindow()
        {
            return application.GetWindow("Untitled - Notepad");
        }
    }
}
