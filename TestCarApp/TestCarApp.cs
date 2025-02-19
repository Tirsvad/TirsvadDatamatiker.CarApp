namespace TestCarApp
{
    [TestClass]
    public sealed class TestCarApp
    {
        [TestMethod]
        public void TestHeader()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                CarApp.Program.Header("Test Header");

                string expected = "Test Header" + Environment.NewLine + "===========" + Environment.NewLine + Environment.NewLine;
                Assert.AreEqual(expected, sw.ToString());
            }
        }
    }
}
