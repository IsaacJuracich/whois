using System;
using NUnit.Framework;
using Whois.Models;
using Whois.Parsers;

namespace Whois.Parsing.Org.Whois.Registry.Net.Za.OrgZa
{
    [TestFixture]
    public class OrgZaParsingTests : ParsingTests
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
            var sample = SampleReader.Read("org-whois.registry.net.za", "org.za", "not_found.txt");
            var response = parser.Parse("org-whois.registry.net.za", "org.za", sample);

            Assert.Greater(sample.Length, 0);
            Assert.AreEqual(WhoisResponseStatus.NotFound, response.Status);
            
            Assert.AreEqual(2, response.FieldsParsed);
            Assert.AreEqual(0, response.ParsingErrors);

            Assert.AreEqual("nosuchdomain.org.za", response.DomainName);
        }

        [Test]
        public void Test_found()
        {
            var sample = SampleReader.Read("org-whois.registry.net.za", "org.za", "found.txt");
            var response = parser.Parse("org-whois.registry.net.za", "org.za", sample);

            Assert.Greater(sample.Length, 0);
            Assert.AreEqual(WhoisResponseStatus.Found, response.Status);

            Assert.AreEqual(47, response.FieldsParsed);
            Assert.AreEqual(0, response.ParsingErrors);

            Assert.AreEqual("joburg.org.za", response.DomainName);
            Assert.AreEqual("dom_8VP-9999", response.RegistryDomainId);

            Assert.AreEqual("ZA Central Registry", response.Registrar.Name);
            Assert.AreEqual("org-whois2.registry.net.za", response.Registrar.WhoisServerUrl);

            Assert.AreEqual(new DateTime(2015, 2, 5, 8, 45, 51), response.Updated);
            Assert.AreEqual(new DateTime(1997, 10, 3, 10, 46, 34), response.Registered);
            Assert.AreEqual(new DateTime(2999, 12, 31, 21, 59, 59), response.Expiration);
            Assert.AreEqual("jobuRant", response.Registrant.RegistryId);
            Assert.AreEqual("City of Johannesburg Metropolitan Municipality", response.Registrant.Name);

            Assert.AreEqual(5, response.Registrant.Address.Count);
            Assert.AreEqual("P.O. Box 30757", response.Registrant.Address[0]);
            Assert.AreEqual("Braamfontein", response.Registrant.Address[1]);
            Assert.AreEqual("Gauteng", response.Registrant.Address[2]);
            Assert.AreEqual("2017", response.Registrant.Address[3]);
            Assert.AreEqual("ZA", response.Registrant.Address[4]);

            Assert.AreEqual("+27.110186314", response.Registrant.TelephoneNumber);
            Assert.AreEqual("+27.113819583", response.Registrant.FaxNumber);
            Assert.AreEqual("joelsonp@joburg.org.za", response.Registrant.Email);

            Assert.AreEqual("zacr-a0c0379446", response.AdminContact.RegistryId);
            Assert.AreEqual("Joelson Pholoha", response.AdminContact.Name);

            Assert.AreEqual(3, response.AdminContact.Address.Count);
            Assert.AreEqual("Private Bag X10013, Sandton, 2146", response.AdminContact.Address[0]);
            Assert.AreEqual("-", response.AdminContact.Address[1]);
            Assert.AreEqual("--", response.AdminContact.Address[2]);

            Assert.AreEqual("+27.110186314", response.AdminContact.TelephoneNumber);
            Assert.AreEqual("+27.113819583", response.AdminContact.FaxNumber);
            Assert.AreEqual("Joelsonp@Joburg.org.za", response.AdminContact.Email);

            Assert.AreEqual("zacr-07de5cca59", response.BillingContact.RegistryId);

            Assert.AreEqual(2, response.BillingContact.Address.Count);
            Assert.AreEqual("-", response.BillingContact.Address[0]);
            Assert.AreEqual("--", response.BillingContact.Address[1]);


            Assert.AreEqual("zacr-71fff5bce2", response.TechnicalContact.RegistryId);
            Assert.AreEqual("Eben Jacobs", response.TechnicalContact.Name);

            Assert.AreEqual(3, response.TechnicalContact.Address.Count);
            Assert.AreEqual("Accounts Payable, Vida Building, Kabelweg 57, 1014 BA Amsterdam", response.TechnicalContact.Address[0]);
            Assert.AreEqual("-", response.TechnicalContact.Address[1]);
            Assert.AreEqual("--", response.TechnicalContact.Address[2]);

            Assert.AreEqual("+27.110186314", response.TechnicalContact.TelephoneNumber);
            Assert.AreEqual("+27.113819583", response.TechnicalContact.FaxNumber);
            Assert.AreEqual("ebenj@joburg.org.za", response.TechnicalContact.Email);


            Assert.AreEqual(3, response.NameServers.Count);
            Assert.AreEqual("demeter.is.co.za", response.NameServers[0]);
            Assert.AreEqual("jupiter.is.co.za", response.NameServers[1]);
            Assert.AreEqual("titan.is.co.za", response.NameServers[2]);

            Assert.AreEqual(1, response.DomainStatus.Count);
            Assert.AreEqual("ok", response.DomainStatus[0]);
            AssertWriter.Write(response);
        }
    }
}