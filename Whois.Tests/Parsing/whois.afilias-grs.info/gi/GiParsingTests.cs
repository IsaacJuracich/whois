using System;
using NUnit.Framework;
using Whois.Models;
using Whois.Parsers;

namespace Whois.Parsing.Whois.Afilias.Grs.Info.Gi
{
    [TestFixture]
    public class GiParsingTests : ParsingTests
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
            var sample = SampleReader.Read("whois.afilias-grs.info", "gi", "not_found.txt");
            var response = parser.Parse("whois.afilias-grs.info", "gi", sample);

            Assert.Greater(sample.Length, 0);
            Assert.AreEqual(WhoisResponseStatus.NotFound, response.Status);
        }

        [Test]
        public void Test_found()
        {
            var sample = SampleReader.Read("whois.afilias-grs.info", "gi", "found.txt");
            var response = parser.Parse("whois.afilias-grs.info", "gi", sample);

            Assert.Greater(sample.Length, 0);
            Assert.AreEqual(WhoisResponseStatus.Found, response.Status);

            Assert.AreEqual(42, response.FieldsParsed);
            Assert.AreEqual(0, response.ParsingErrors);
            Assert.AreEqual("generic/tld/Found001", response.TemplateName);

            Assert.AreEqual("sapphire.gi", response.DomainName);
            Assert.AreEqual("D68296-LRCC", response.RegistryDomainId);

            Assert.AreEqual("GibNet Registrar (R43-LRCC)", response.Registrar.Name);

            Assert.AreEqual(new DateTime(2008, 12, 20, 19, 25, 54), response.Updated);
            Assert.AreEqual(new DateTime(2004, 12, 20, 13, 34, 34), response.Registered);
            Assert.AreEqual(new DateTime(2009, 12, 20, 13, 34, 34), response.Expiration);
            Assert.AreEqual("FR-1103549674779", response.Registrant.RegistryId);
            Assert.AreEqual("Jimmy Imossi", response.Registrant.Name);
            Assert.AreEqual("Broadband Gibraltar Limited", response.Registrant.Organization);

            Assert.AreEqual(3, response.Registrant.Address.Count);
            Assert.AreEqual("Gibraltar", response.Registrant.Address[0]);
            Assert.AreEqual("NA", response.Registrant.Address[1]);
            Assert.AreEqual("GI", response.Registrant.Address[2]);

            Assert.AreEqual("+350.47200", response.Registrant.TelephoneNumber);
            Assert.AreEqual("jimossi@sapphire.gi", response.Registrant.Email);

            Assert.AreEqual("FR-1103549674779", response.AdminContact.RegistryId);
            Assert.AreEqual("Jimmy Imossi", response.AdminContact.Name);
            Assert.AreEqual("Broadband Gibraltar Limited", response.AdminContact.Organization);

            Assert.AreEqual(3, response.AdminContact.Address.Count);
            Assert.AreEqual("Gibraltar", response.AdminContact.Address[0]);
            Assert.AreEqual("NA", response.AdminContact.Address[1]);
            Assert.AreEqual("GI", response.AdminContact.Address[2]);

            Assert.AreEqual("+350.47200", response.AdminContact.TelephoneNumber);
            Assert.AreEqual("jimossi@sapphire.gi", response.AdminContact.Email);

            Assert.AreEqual("FR-1103549674779", response.BillingContact.RegistryId);
            Assert.AreEqual("Jimmy Imossi", response.BillingContact.Name);
            Assert.AreEqual("Broadband Gibraltar Limited", response.BillingContact.Organization);

            Assert.AreEqual(3, response.BillingContact.Address.Count);
            Assert.AreEqual("Gibraltar", response.BillingContact.Address[0]);
            Assert.AreEqual("NA", response.BillingContact.Address[1]);
            Assert.AreEqual("GI", response.BillingContact.Address[2]);

            Assert.AreEqual("+350.47200", response.BillingContact.TelephoneNumber);
            Assert.AreEqual("jimossi@sapphire.gi", response.BillingContact.Email);

            Assert.AreEqual("FR-10a223e2e4cf0", response.TechnicalContact.RegistryId);
            Assert.AreEqual("Tech Dept", response.TechnicalContact.Name);
            Assert.AreEqual("Broadband Gibraltar Ltd", response.TechnicalContact.Organization);

            Assert.AreEqual(3, response.TechnicalContact.Address.Count);
            Assert.AreEqual("N/A", response.TechnicalContact.Address[0]);
            Assert.AreEqual("N/A", response.TechnicalContact.Address[1]);
            Assert.AreEqual("GI", response.TechnicalContact.Address[2]);

            Assert.AreEqual("+350.47200", response.TechnicalContact.TelephoneNumber);
            Assert.AreEqual("tech@sapphire.gi", response.TechnicalContact.Email);


            Assert.AreEqual(2, response.NameServers.Count);
            Assert.AreEqual("ns1-a.sapphire.gi", response.NameServers[0]);
            Assert.AreEqual("ns2-a.sapphire.gi", response.NameServers[1]);

            Assert.AreEqual(1, response.DomainStatus.Count);
            Assert.AreEqual("OK", response.DomainStatus[0]);
        }
    }
}