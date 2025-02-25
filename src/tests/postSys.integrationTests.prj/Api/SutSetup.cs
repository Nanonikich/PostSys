using PostSys.Dal.Persistence;
using PostSys.IntegrationTests.Common.Docker.Database;

namespace PostSys.IntegrationTests.Api;

/// <summary>Определяет инициализацию SUT.</summary>
[SetUpFixture]
internal class SutSetup : PostgreSqlContainerSetup<Program, WriteDbContext>;