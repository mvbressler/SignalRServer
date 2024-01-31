using Bogus;
using Bogus.Bson;
using Bogus.DataSets;
using GSIMSignalRServerProject.Domain.Enums;
using GSIMSignalRServerProject.Domain.Models;

namespace GSIMSignalRServerProject.Infrastructure
{
    public static class ExecuteMethodGenerator
    {
        public static LenelCommandRequest DoGenerate(TestingTypeNamesEnum namesEnum)
        {
            var faker = new Faker();
            var result = new LenelCommandRequest
            {
                TypeName = ((int)namesEnum).ToString(),
                MethodName = "SendIn￾comingEvent"

            };
            switch (namesEnum)
            {
                case TestingTypeNamesEnum.Lnl_Reader:
                    result.Parameters.Add("ID", faker.Random.Int());
                    result.Parameters.Add("AlarmPanelID", faker.Random.Int());
                    result.Parameters.Add("HostName", faker.Internet.DomainName());
                    result.Parameters.Add("InputID", faker.Random.Int());
                    result.Parameters.Add("Name", faker.Commerce.Product());
                    result.Parameters.Add("PanelID", faker.Random.Int());
                    break;
                
                case TestingTypeNamesEnum.Lnl_AlarmPanel:
                    result.Parameters.Add("ID", faker.Random.Int());
                    result.Parameters.Add("PanelID", faker.Random.Int());
                    result.Parameters.Add("ControlType", faker.Random.Int());
                    result.Parameters.Add("Name", faker.Commerce.Product());
                    break;

                case TestingTypeNamesEnum.Lnl_AlarmInput:
                    result.Parameters.Add("ID", faker.Random.Int());
                    result.Parameters.Add("AlarmPanelID", faker.Random.Int());
                    result.Parameters.Add("HostName", faker.Internet.DomainName());
                    result.Parameters.Add("InputID", faker.Random.Int());
                    result.Parameters.Add("Name", faker.Commerce.Product());
                    result.Parameters.Add("PanelID", faker.Random.Int());
                    break;
                case TestingTypeNamesEnum.Lnl_IntrusionDoor:
                    result.Parameters.Add("ID", faker.Random.Int());
                    result.Parameters.Add("DeviceId", faker.Random.Int());
                    result.Parameters.Add("PanelId", faker.Random.Int());
                    result.Parameters.Add("HostName", faker.Internet.DomainName());
                    result.Parameters.Add("Name", faker.Commerce.Product());
                    break;
                case TestingTypeNamesEnum.Lnl_ReaderInput:
                    result.Parameters.Add("PanelId", faker.Random.Int());
                    result.Parameters.Add("ReaderId", faker.Random.Int());
                    result.Parameters.Add("HostName", faker.Internet.DomainName());
                    result.Parameters.Add("Name", faker.Commerce.Product());
                    break;
                case TestingTypeNamesEnum.Lnl_Panel:
                    result.Parameters.Add("ID", faker.Random.Int());
                    result.Parameters.Add("IsDayLightSaving", faker.Random.Bool());
                    result.Parameters.Add("IsOnline", faker.Random.Bool());
                    result.Parameters.Add("NAME", faker.Commerce.Product());
                    result.Parameters.Add("PANELTYPE", faker.Commerce.Categories);
                    result.Parameters.Add("ComputerName", faker.Internet.DomainName());
                    break;
                case TestingTypeNamesEnum.Lnl_EventType:
                    result.Parameters.Add("ID", faker.Random.Int());
                    result.Parameters.Add("Description", faker.Hacker.Phrase());
                    break;
                case TestingTypeNamesEnum.Lnl_EventSubtypeDefinition:
                    result.Parameters.Add("ID", faker.Random.Int());
                    result.Parameters.Add("TypeID", faker.Random.Int());
                    result.Parameters.Add("SubTypeID", faker.Random.Int());
                    result.Parameters.Add("Description", faker.Hacker.Phrase());
                    result.Parameters.Add("SupportParameters", faker.Random.Int());
                    result.Parameters.Add("Category", faker.Random.Int());
                    
                    break;

            }
            return result;
        }
    }
}
