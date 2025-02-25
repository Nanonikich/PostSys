using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using PostSys.Dal.Persistence;
using PostSys.Domain.Clients;
using PostSys.Domain.Packages.Checkers;
using PostSys.Domain.Postmen;

namespace PostSys.Dal.Services;

/// <summary>Реализует <see cref="IPackageParametersChecker"/>.</summary>
/// <param name="context">Контекст для доступа к данным.</param>
internal class PackageParametersChecker(WriteDbContext context) : IPackageParametersChecker
{
	#region Data

	private readonly WriteDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

	#endregion

	#region Methods

	/// <inheritdoc/>
	public async Task<bool> CheckExistenceOfClientAsync(Guid clientId, CancellationToken cancellationToken)
	{
		return await _context
			.Set<Client>()
			.AnyAsync(x => x.Id == clientId && !x.IsDeleted, cancellationToken);
	}

	/// <inheritdoc/>
	public async Task<bool> CheckExistenceOfPostmanAsync(Guid postmanId, CancellationToken cancellationToken)
	{
		return await _context
			.Set<Postman>()
			.AnyAsync(x => x.Id == postmanId && !x.IsDeleted, cancellationToken);
	}

	#endregion
}