using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using PostSys.Dal.Persistence;
using PostSys.Domain.Packages;
using PostSys.Domain.Postmen;
using PostSys.Domain.Postmen.Checkers;

namespace PostSys.Dal.Services;

/// <summary>Реализует <see cref="IPostmanParametersChecker"/>.</summary>
/// <param name="context">Контекст для доступа к данным.</param>
internal class PostmanParametersChecker(WriteDbContext context) : IPostmanParametersChecker
{
	#region Data

	private readonly WriteDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

	#endregion

	#region Methods

	/// <inheritdoc/>
	public async Task<bool> CheckUniqueEmailAsync(string email, CancellationToken cancellationToken)
	{
		return !await _context
			.Set<Postman>()
			.AnyAsync(x => x.Email.Value == email, cancellationToken);
	}

	/// <inheritdoc/>
	public async Task<bool> CheckExistenceOfPackageAsync(Guid packageId, CancellationToken cancellationToken)
	{
		return await _context
			.Set<Package>()
			.AnyAsync(x => x.Id == packageId && !x.IsDeleted, cancellationToken);
	}

	/// <inheritdoc/>
	public async Task<bool> CheckPackageIsNotInOperationAsync(Guid packageId, CancellationToken cancellationToken)
	{
		return !await _context
			.Set<Postman>()
			.AnyAsync(x => x.PostmanPackageId == packageId && !x.IsDeleted, cancellationToken);
	}

	#endregion
}