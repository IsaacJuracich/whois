using NUnit.Framework;
using Whois.Models;
using Whois.Parsers;

namespace Whois.Parsing.Whois.Na.Nic.Com.Na.Na
{
    [TestFixture]
    public class NaParsingTests : ParsingTests
    {
        private WhoisParser parser;

        [SetUp]
        public void SetUp()
        {
            SerilogConfig.Init();

            parser = new WhoisParser();
        }

        [Test]
        public void Test_not_found()
        {
            var sample = SampleReader.Read("whois.na-nic.com.na", "na", "not_found.txt");
            var response = parser.Parse("whois.na-nic.com.na", "na", sample);

            Assert.Greater(sample.Length, 0);
            Assert.AreEqual(WhoisResponseStatus.NotFound, response.Status);
        }

        [Test]
        public void Test_found()
        {
            var sample = SampleReader.Read("whois.na-nic.com.na", "na", "found.txt");
            var response = parser.Parse("whois.na-nic.com.na", "na", sample);

            Assert.Greater(sample.Length, 0);
            Assert.AreEqual(WhoisResponseStatus.Found, response.Status);
        }
    }
}